using System.ComponentModel.DataAnnotations;

namespace SecretSharing.Dtos
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
            ErrorMessage = "Passord Must have Atleast 1 Lower 1 Upper and 1 Special Character")]
        public string Password { get; set; }
    }
}
