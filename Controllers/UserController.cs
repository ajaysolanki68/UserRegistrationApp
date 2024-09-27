using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UserRegistrationApp.Models;
using UserRegistrationApp.Services;

namespace UserRegistrationApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: /api/user
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            var requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
            Console.WriteLine($"Incoming request: {requestBody}");

            if (userDto == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid user data.");
            }

            await _userService.InsertUserAsync(userDto);
            return CreatedAtAction(nameof(GetUserById), new { id = userDto.Id }, userDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
        // GET: /api/user/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // PUT: /api/user/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto userDto)
        {
            if (id != userDto.Id || !ModelState.IsValid)
            {
                return BadRequest("Invalid user data.");
            }

            await _userService.UpdateUserAsync(userDto);
            return NoContent();
        }

        // DELETE: /api/user/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userService.DeleteUserAsync(id);
            return NoContent();
        }

        // GET: /api/user/states
        [HttpGet("states")]
        public IActionResult GetStates()
        {
            var states = new List<State>
            {
                new State { Id = 1, StateName = "Gujarat" },
                new State { Id = 2, StateName = "Maharashtra" }
            };
            return Ok(states.Select(s => new { id = s.Id, name = s.StateName }));
        }
        // GET: /api/user/cities/{state}
        [HttpGet("cities/{state}")]
        public IActionResult GetCities(string state)
        {
            var cities = state switch
            {
                "Gujarat" => new List<City>
        {
            new City { Id = 1, CityName = "Surat" },
            new City { Id = 2, CityName = "Bardoli" },
            new City { Id = 3, CityName = "Baroda" }
        },
                "Maharashtra" => new List<City>
        {
            new City { Id = 4, CityName = "Mumbai" },
            new City { Id = 5, CityName = "Pune" }
        },
                _ => new List<City>()
            };

            return Ok(cities.Select(c => new { id = c.Id, name = c.CityName }));
        }
    }
}
