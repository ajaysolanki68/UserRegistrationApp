using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistrationApp.Models;
using UserRegistrationApp.Services;

namespace UserRegistrationApp.Controllers
{
    public class UserRegistrationController : Controller
    {
        private readonly IUserService _userService;

        public UserRegistrationController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var users = _userService.GetAllUsersAsync().Result; 
            return View(users);
        }
        // POST: /User/Add
        [HttpPost]
        public async Task<IActionResult> Add(UserDto user)
        {
            if (ModelState.IsValid)
            {
                 await _userService.InsertUserAsync(user); 
                return  RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: /User/Edit/{id}
        [HttpGet]
        public async Task< IActionResult> Edit(int id)
        {
            var user = await _userService.GetUserByIdAsync(id); 
            return Json(user);
        }

        // POST: /User/Edit
        [HttpPost]
        public async Task  <IActionResult> Edit(UserDto user)
        {
            if (ModelState.IsValid)
            {
                await _userService.UpdateUserAsync(user); 
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // DELETE: /User/Delete/{id}
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteUserAsync(id);
            return Json(new { success = true });
        }
    }
}
