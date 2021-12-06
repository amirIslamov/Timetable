using Model.Profile.Roles;

namespace Model.Profile
{
    public class TimetableUser
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PasswordHash { get; set; }
        
        public FullName FullName { get; set; }
        public Address Address { get; set; }

        public RoleSet RoleSet { get; set; } = new RoleSet();
    }
    
    public class FullName
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }

        public FullName(string firstName, string lastName, string patronymic)
        {
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
        }
    }

    public class Address
    {
        public string City { get; set; }
        public string HouseNumber { get; set; }
        public string ZipCode { get; set; }

        public Address(string city, string houseNumber, string zipCode)
        {
            City = city;
            HouseNumber = houseNumber;
            ZipCode = zipCode;
        }

        public Address() { }
    }
}