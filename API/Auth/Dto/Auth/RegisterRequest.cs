using Model.Entities;

namespace API.Auth.Dto.Auth;

public class RegisterRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string PasswordConfirmation { get; set; }
    public FullName FullName { get; set; }
    public Address Address { get; set; }
    public string PhoneNumber { get; set; }
}