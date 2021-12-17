namespace Model.Auth;

public class PasswordHasherOptions
{
    public const string PasswordHasher = "PasswordHasher";

    public int WorkFactor { get; set; } = 12;
}