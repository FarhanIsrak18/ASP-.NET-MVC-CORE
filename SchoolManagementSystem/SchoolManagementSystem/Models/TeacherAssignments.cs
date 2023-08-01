using SchoolManagementSystem.Migrations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class TeacherAssignments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeacherAssignmentId { get; set; }

        [Required]
        public int TeacherId { get; set;}

        [Required]
        public int DepartmentId { get; set;}

        [ForeignKey("TeacherId")]
        public Teachers Teachers { get; set; }

        [ForeignKey("DepartmentId")]
        public Departments Departments { get; set; }
    }
}
