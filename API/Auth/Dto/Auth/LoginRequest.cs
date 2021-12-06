using System.ComponentModel.DataAnnotations;

namespace API.Auth.Dto.Auth
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(16, MinimumLength = 8)]
        [Required]
        public string Password { get; set; }
    }
}