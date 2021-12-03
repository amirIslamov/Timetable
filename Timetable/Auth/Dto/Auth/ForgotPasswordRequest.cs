using System.ComponentModel.DataAnnotations;

namespace Timetable.Auth.Dto.Profile
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