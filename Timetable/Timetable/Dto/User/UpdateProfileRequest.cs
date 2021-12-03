using Timetable.Auth.Model;

namespace Timetable.Timetable.Dto.User
{
    public class UpdateProfileRequest
    {
        public string PhoneNumber { get; set; }
        public FullName FullName { get; set; }
        public Address Address { get; set; }
    }
}