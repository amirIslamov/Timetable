namespace API.Auth.Dto.Profile;

public class ResetPasswordRequest
{
    public long UserId { get; set; }
    public string Code { get; set; }
    public string NewPassword { get; set; }
}