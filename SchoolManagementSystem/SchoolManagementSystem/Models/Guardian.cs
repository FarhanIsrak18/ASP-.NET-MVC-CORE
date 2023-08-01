using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Guardian
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GuardianId { get; set; }
        public string GuardianName { get; set; }
        public string PhoneNumber { get; set; }
        public string Relation { get; set; }
        public string StudentName { get; set; }
        public int StudentId { get; set; }
    }
}
