using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StudentRecordUsingDapper.Models;
using StudentRecordUsingDapper.Models.ViewModels;
using StudentRecordUsingDapper.Services;

namespace StudentRecordUsingDapper.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserServices _userService;
        private readonly IConfiguration _configuration;

        public LoginController(IUserServices userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login loginViewModel)
        {
            string url = Request.Headers["Referer"].ToString();
            User user = _userService.AuthenticateUser(loginViewModel.Username, loginViewModel.Password);

            if (user != null)
            {
                // Authentication successful, store user data in session
                // HttpContext.Session.SetString("Id", user.Id.ToString());
                HttpContext.Session.SetString("Username", user.UserName);

                // TempData["SuccessMsg"] = "Login Successful";
                return RedirectToAction("Index", "Home");
            }

            TempData["ErrorMsg"] = "Invalid username or password.";
            return Redirect(url);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            // Clear session data
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Login"); // Redirect to home page or any other page after logout
        }
    }
}
