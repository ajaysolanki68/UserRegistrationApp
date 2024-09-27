using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistrationApp.Models;
using UserRegistrationApp.Repositories;

namespace UserRegistrationApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }


        public async Task UpdateUserAsync(UserDto userDto)
        {
            await _userRepository.UpdateAsync(userDto);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task  InsertUserAsync(UserDto userDto)
        {
            await _userRepository.InsertAsync(userDto);
        }
    }
}
