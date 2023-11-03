using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentRecordUsingDapper.Models;

namespace StudentRecordUsingDapper.Services
{
    public interface IStudentServices
    {
        public List<Student> GetAllStudentsList();
        public string InsertStudentRecord(Student student);
        public string UpdateStudentById(Student student);
        public string DeleteStudentRecord(int StudentId);
    }
}