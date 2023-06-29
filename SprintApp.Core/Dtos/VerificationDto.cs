using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SprintApp.Core.Dtos
{
    public class VerificationDto
    {
        [Required(ErrorMessage = "Email is Required"), DisplayName("Email"), EmailAddress]
        public string? EmailId { get; set; }
        [Required(ErrorMessage = "Token Required"), DisplayName("Verification Token")]
        public string? VerificationToken { get; set; }
    }
}
