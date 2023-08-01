using SchoolManagementSystem.Migrations;

namespace SchoolManagementSystem.Models
{
    public class TeacherAssignmentViewModel
    {
        public List<Teachers> Teachers { get; set; }
        public List<Departments> Departments { get; set; }
        public List<StudentResults> Results { get; set; }
    }
}
