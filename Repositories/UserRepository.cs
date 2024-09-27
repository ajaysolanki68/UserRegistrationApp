using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UserRegistrationApp.Data;
using UserRegistrationApp.Models;

namespace UserRegistrationApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RegistrationDbContext _context;

        public UserRepository(RegistrationDbContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(int id)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_DeleteUser @Id", new SqlParameter("@Id",id));
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _context.Users.FromSqlRaw("EXEC sp_GetAllUsers").ToListAsync();

            return users.Select(user => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address,
                StateId = user.StateId,
                CityId = user.CityId
            });
        }
        public async Task<UserDto> GetByIdAsync(int id)
        {
            try
            {
                var sql = "SELECT * FROM tblUserRegistration WHERE Id = @Id";
                var user = await _context.Users
                    .FromSqlRaw(sql, new SqlParameter("@Id", id)).AsNoTracking().FirstOrDefaultAsync();

                if (user == null) return null;

                return new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Phone = user.Phone,
                    Address = user.Address,
                    StateId = user.StateId,
                    CityId = user.CityId
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching user data", ex);
            }

        }

        public async Task InsertAsync(UserDto userDto)
        {
            await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_InsertUserRegistration @Name, @Email, @Phone, @Address, @StateId, @CityId",
            new SqlParameter("@Name", userDto.Name),
            new SqlParameter("@Email", userDto.Email),
            new SqlParameter("@Phone", userDto.Phone),
            new SqlParameter("@Address", userDto.Address ?? (object)DBNull.Value),
            new SqlParameter("@StateId", userDto.StateId),
            new SqlParameter("@CityId", userDto.CityId)
            );
        }

        public async Task UpdateAsync(UserDto userDto)
        {
           try
           {
                await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_UpdateUserRegistration @Id, @Name, @Email, @Phone, @Address, @StateId, @CityId",

                    new SqlParameter("@Id", userDto.Id),
                    new SqlParameter("@Name", userDto.Name),
                    new SqlParameter("@Email", userDto.Email),
                    new SqlParameter("@Phone", userDto.Phone),
                    new SqlParameter("@Address", userDto.Address ?? (object)DBNull.Value),
                    new SqlParameter("@StateId", userDto.StateId),
                    new SqlParameter("@CityId", userDto.CityId)
                );
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the user.", ex);
            }
        }
    }
}
