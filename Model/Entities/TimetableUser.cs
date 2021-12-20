using Model.Profile.Roles;

namespace Model.Entities;

public class TimetableUser
{
    public long Id { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string PhoneNumber { get; set; }
    public string ConcurrencyStamp { get; set; }
    public string PasswordHash { get; set; }
    public FullName FullName { get; set; }
    public Address Address { get; set; }

    public RoleSet RoleSet { get; set; } = new();
    public RoleSet RequestedRoles { get; set; } = new();
}

public class FullName
{
    public FullName(string firstName, string lastName, string patronymic)
    {
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
}

public class Address
{
    public Address(string city, string houseNumber, string zipCode)
    {
        City = city;
        HouseNumber = houseNumber;
        ZipCode = zipCode;
    }

    public Address()
    {
    }

    public string City { get; set; }
    public string HouseNumber { get; set; }
    public string ZipCode { get; set; }
}