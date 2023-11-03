
namespace StudentRecordUsingDapper.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Response { get; set; }
    }
}