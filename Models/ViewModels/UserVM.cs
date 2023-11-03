using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentRecordUsingDapper.Models.ViewModels
{
    public class UserVM
    {
        public User User { get; set; }
        public List<User> UsersList{get; set;}
    }
}