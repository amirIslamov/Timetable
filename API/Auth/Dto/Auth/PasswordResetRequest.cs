using System.ComponentModel.DataAnnotations;

namespace API.Auth.Dto.Profile
{
    public class ResetPasswordRequest
    {
        [Required]
        public long UserId { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        [Compare(nameof(NewPassword))]
        public string NewPasswordConfirmation { get; set; }
    }
}