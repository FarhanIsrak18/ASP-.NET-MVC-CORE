using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class StudentResults
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }

        [Required]
        public string StudentName { get; set; }

        [Required]
        public int Bangla { get; set; }

        [Required]
        public int English { get; set; }

        [Required]
        public int Math { get; set; }

        [Required]
        public int Science { get; set; }

        [Required]
        public int Average { get; set; }

        [Required]
        public int Status { get; set; }


    }
}
