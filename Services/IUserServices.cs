using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentRecordUsingDapper.Models;

namespace StudentRecordUsingDapper.Services
{
    public interface IUserServices
    {
        public string RegisterUser(User user);
        User AuthenticateUser(string userName, string password);
    }

}