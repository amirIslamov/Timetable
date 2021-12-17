namespace Model.Auth;

public class PasswordValidationOptions
{
    public const string PasswordValidation = "PasswordValidation";

    public string AllowedCharacters { get; set; }
    public int MinSize { get; set; }
    public int MaxSize { get; set; }
    public bool RequireDigit { get; set; }
    public bool RequireNonAlphanumeric { get; set; }
}