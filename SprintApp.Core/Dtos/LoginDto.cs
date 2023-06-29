using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SprintApp.Core.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email is Required"), DisplayName("Email"), EmailAddress]
        public string EmailId { get; set; }= string.Empty;
        [Required(ErrorMessage = "Password is Required"), DisplayName("Password"), PasswordPropertyText]
        public string Password { get; set; } = string.Empty;
    }
}
