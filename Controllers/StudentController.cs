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
    public class StudentController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IStudentServices _studentServices;

        public StudentController(IConfiguration configuration, IStudentServices studentServices)
        {
            _configuration = configuration;
            _studentServices = studentServices;
        }

        public IActionResult Index()
        {
            StudentVM model = new StudentVM();
            model.StudentsList = _studentServices.GetAllStudentsList().ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult CreateUpdate(Student student)
        {
            // var model = _studentServices.InsertStudentRecord(student);
            // return View(model);
            StudentVM model = new StudentVM();

            string url = Request.Headers["Referer"].ToString();
            string result = string.Empty;

            if(student.StudentID>0){
                result = _studentServices.UpdateStudentById(student);
            }
            else{
                result = _studentServices.InsertStudentRecord(student);
            }
            if(result == "Save Successfully" || result == "Student updated successfully"){

                TempData["SuccessMsg"] = "Save Successful";
                return Redirect(url);
            }
            TempData["ErrorMsg"] = result;
            return Redirect(url);
        }

        [HttpPost]
        public IActionResult DeleteStudent(int StudentID){
            string url = Request.Headers["Referer"].ToString();
            string result = _studentServices.DeleteStudentRecord(StudentID);
            if(result == "Student deleted successfully"){
                return Json(new {message="Student deleted successfully"});
            }
            return Json(new {message="Error"});
        }
    }
}