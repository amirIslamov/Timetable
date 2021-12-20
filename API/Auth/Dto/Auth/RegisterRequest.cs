using Model.Entities;
using Model.Profile.Roles;

namespace API.Auth.Dto.Auth;

public class RegisterRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string PasswordConfirmation { get; set; }
    public FullName FullName { get; set; }
    public Address Address { get; set; }
    public List<Role> RequestedRoles { get; set; }
    public string PhoneNumber { get; set; }
}