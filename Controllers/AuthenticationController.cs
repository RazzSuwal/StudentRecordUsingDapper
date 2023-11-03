using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentRecordUsingDapper.Models;
using StudentRecordUsingDapper.Services;

namespace StudentRecordUsingDapper.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserServices _userService;

        public AuthenticationController(IUserServices userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                var authenticatedUser = _userService.AuthenticateUser(user.UserName, user.Password);
                if (authenticatedUser != null)
                {
                    // Authentication successful
                    // Implement your logic here, e.g., set authentication cookie and redirect to dashboard
                    // Session Start hunxa with cookie
                    return RedirectToAction("Index", "Dashboard");
                }
            }

            // Authentication failed
            ModelState.AddModelError(string.Empty, "Invalid credentials");
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Sign out the user
            //Session ENd

            // Redirect to the home page after logout
            return RedirectToAction("Index", "Home");
        }







    }

}