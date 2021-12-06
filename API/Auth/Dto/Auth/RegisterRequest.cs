using System.ComponentModel.DataAnnotations;
using Model.Profile;

namespace API.Auth.Dto.Auth
{
    public class RegisterRequest
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        
        [StringLength(maximumLength: 16, MinimumLength = 8)]
        [Required]
        public string Password { get; set; }
        
        [StringLength(maximumLength: 16, MinimumLength = 8)]
        [Required]
        [Compare("Password")]
        public string PasswordConfirmation { get; set; }
        
        [Required]
        public FullName FullName { get; set; }
        
        public Address Address { get; set; }
        
        [Phone]
        public string PhoneNumber { get; set; }
    }
}