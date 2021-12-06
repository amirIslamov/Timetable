using System.ComponentModel.DataAnnotations;

namespace API.Auth.Dto.Profile
{
    public class RequestPasswordResetRequest
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        [Url]
        public string ReturnUrl { get; set; }
    }
}