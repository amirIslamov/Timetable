namespace API.Auth.Dto.Profile;

public class RequestPasswordResetRequest
{
    public string Email { get; set; }
    public string ReturnUrl { get; set; }
}