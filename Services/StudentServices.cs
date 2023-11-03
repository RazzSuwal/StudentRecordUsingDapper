using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentRecordUsingDapper.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Dapper;

namespace StudentRecordUsingDapper.Services
{
    public class StudentServices : IStudentServices
    {
        private IConfiguration _configuration;

        public StudentServices(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("DefaultConnection");
            providerName = "System.Data.SqlClient";
        }

        public string ConnectionString{get;}
        public string providerName{get;}
        public IDbConnection connection{
            get{return new SqlConnection(ConnectionString);}
        }
        public string DeleteStudentRecord(int StudentId)
        {
             string result ="";
            try
            {
                using (IDbConnection dbConnection=connection)
                {
                    dbConnection.Open();
                    var std = dbConnection.Query<Student>("DeleteStudentRecord",
                    new{StudentID = StudentId},
                    commandType:CommandType.StoredProcedure);

                    if (std != null && std.FirstOrDefault().Response == "Student deleted successfully")
                    {
                        result = "Student deleted successfully";
                    }
                    dbConnection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                return errorMessage;
            }
        }

        public List<Student> GetAllStudentsList()
        {
            List<Student> students = new List<Student>();
            try
            {
                using (IDbConnection dbConnection=connection)
                {
                    dbConnection.Open();
                    students = dbConnection.Query<Student>("GetAllStudentsList", commandType:CommandType.StoredProcedure).ToList();
                    dbConnection.Close();
                    return students;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                return students;
            }
        }

        public string InsertStudentRecord(Student student)
        {
            string result ="";
            try
            {
                using (IDbConnection dbConnection=connection)
                {
                    dbConnection.Open();
                    var std = dbConnection.Query<Student>("InsertStudentRecord",
                    new{FullName = student.FullName, Email = student.Email, PhoneNumber = student.PhoneNumber},
                    commandType:CommandType.StoredProcedure);

                    if (std != null && std.FirstOrDefault().Response == "Saved Successfully")
                    {
                        result = "Save Successfully";
                    }
                    dbConnection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                return errorMessage;
            }

        }

        public string UpdateStudentById(Student student)
        {
            string result ="";
            try
            {
                using (IDbConnection dbConnection=connection)
                {
                    dbConnection.Open();
                    var std = dbConnection.Query<Student>("UpdateStudentByID",
                    new{FullName = student.FullName, Email = student.Email, PhoneNumber = student.PhoneNumber, StudentID = student.StudentID},
                    commandType:CommandType.StoredProcedure);

                    if (std != null && std.FirstOrDefault().Response == "Student updated successfully")
                    {
                        result = "Student updated successfully";
                    }
                    dbConnection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                return errorMessage;
            }
        }
    }
}