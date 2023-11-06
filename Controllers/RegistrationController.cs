using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentRecordUsingDapper.Models;
using StudentRecordUsingDapper.Models.ViewModels;
using StudentRecordUsingDapper.Services;

namespace StudentRecordUsingDapper.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IUserServices _userService;
        private readonly IConfiguration _configuration;

        public RegistrationController(IUserServices userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterUser(User user)
        {
            UserVM model = new UserVM();

            string url = Request.Headers["Referer"].ToString();
            string result = _userService.RegisterUser(user);


            if (result == "Register Successfully")
            {

                // TempData["SuccessMsg"] = "Register Successfully";
                return RedirectToAction("Login", "Login");
            }
            TempData["ErrorMsg"] = result;
            return Redirect(url);
        }


    }
}

