using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SprintApp.Core.Dtos
{
    public class RegistrationDto
    {
        [Required(ErrorMessage ="First Name is Required"), MinLength(3), DisplayName("First Name")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is Required"), MinLength(3), DisplayName("Last Name")]
        public string? LastName { get; set; }
        [Required(ErrorMessage ="Email is Required"), DisplayName("Email"), EmailAddress]
        public string? EmailId { get; set; }
        [Required(ErrorMessage = "Password is Required"), DisplayName("Password"), PasswordPropertyText]
        public string Password { get; set; } = string.Empty;
    }
}
