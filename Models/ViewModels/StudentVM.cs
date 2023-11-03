using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentRecordUsingDapper.Models.ViewModels
{
    public class StudentVM
    {
        public Student Student { get; set; }
        public List<Student> StudentsList{get; set;}
    }
}