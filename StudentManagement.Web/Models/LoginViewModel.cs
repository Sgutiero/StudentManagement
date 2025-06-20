using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        public string Document { get; set; } = "";
    }
}
