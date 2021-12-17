using Model.Entities;

namespace API.Timetable.Dto.User;

public class UpdateProfileRequest
{
    public string PhoneNumber { get; set; }
    public FullName FullName { get; set; }
    public Address Address { get; set; }
}