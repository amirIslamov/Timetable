using System.Collections.Generic;
using System.Linq;
using Model.Profile;
using Model.Profile.Roles;

namespace API.Timetable.Dto.User
{
    public class GetProfileResponse
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public FullName FullName { get; set; }
        public Address Address { get; set; }
        public List<Role> Roles { get; set; }

        public static GetProfileResponse FromUser(TimetableUser user)
            => new GetProfileResponse()
            {
                Id = user.Id,
                Email = user.Email,
                Address = user.Address,
                Roles = user.RoleSet.Roles.ToList(),
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber
            };
    }
}