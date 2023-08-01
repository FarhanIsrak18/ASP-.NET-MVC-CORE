using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class EditViewModel
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        public string oldPassword { get; set; }
        public string confirmPassword { get; set; }
        public string Role { get; set; }
    }
}
