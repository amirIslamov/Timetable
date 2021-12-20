namespace Model.Entities;

public class Student
{
    public long Id { get; set; }
    public ParentContacts MotherContacts { get; set; }
    public ParentContacts FatherContacts { get; set; }

    public long GroupId { get; set; }
    public Group Group { get; set; }

    public long UserId { get; set; }
    public TimetableUser User { get; set; }
}

public class ParentContacts
{
    public ParentContacts(FullName fullName, string phoneNumber)
    {
        FullName = fullName;
        PhoneNumber = phoneNumber;
    }

    public ParentContacts()
    {
    }

    public FullName FullName { get; set; }
    public string PhoneNumber { get; set; }
}