using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SprintApp.Core.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email is Required"), DisplayName("Email"), EmailAddress]
        public string? EmailId { get; set; }
        [Required(ErrorMessage = "Password is Required"), DisplayName("Password"), PasswordPropertyText]
        public string? Password { get; set; }
        [Required(ErrorMessage ="Token Required"), DisplayName("Verification Token")]
        public string? VerificationToken { get; set; }
    }
}
