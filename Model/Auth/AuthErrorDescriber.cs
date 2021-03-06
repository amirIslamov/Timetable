using Model.Validation.Abstractions;

namespace Model.Auth;

public class AuthErrorDescriber
{
    public ValidationError DuplicateEmail(string entityCode)
    {
        return new(
            nameof(DuplicateEmail),
            $"User with email {entityCode} is already exists");
    }

    public ValidationError UserEmailNull()
    {
        return new(
            nameof(UserEmailNull),
            "User email must be provided");
    }

    public IValidationError IncorrectPasswordLength(int optionsMinSize, int optionsMaxSize)
    {
        return new ValidationError(
            nameof(IncorrectPasswordLength),
            $"Password length should be in range of {optionsMinSize} to {optionsMaxSize}");
    }

    public IValidationError PasswordContainsDisallowedCharacters()
    {
        return new ValidationError(
            nameof(PasswordContainsDisallowedCharacters),
            "Password contains disallowed characters");
    }

    public IValidationError PasswordDoesNotContainNonAlphanumericCharacters()
    {
        return new ValidationError(
            nameof(PasswordDoesNotContainNonAlphanumericCharacters),
            "Password should contain at least one non alphanumeric character");
    }


    public IValidationError PasswordDoesNotContainDigits()
    {
        return new ValidationError(
            nameof(PasswordDoesNotContainDigits),
            "Password should contain at least one digit");
    }

    public IValidationError InvalidRequestedRoles()
    {
        return new ValidationError(
            nameof(InvalidRequestedRoles),
            "User should request at least one role");    }
}