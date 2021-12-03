using System.Collections.Generic;
using System.Linq;
using Model.Profile.Roles;
using Repositories.Util;
using Timetable.Auth.Model;

namespace Timetable.Timetable.Dto.User
{
    public class ListUsersResponse
    {
        public List<ShortUser> Users { get; set; }
        public Paging Paging { get; set; }

        public class ShortUser
        {
            public long Id { get; set; }
            public string Email { get; set; }
            public FullName FullName { get; set; }
            public List<Role> Roles { get; set; }

            public static ShortUser FromUser(TimetableUser user) => new ShortUser()
            {
                Id = user.Id,
                Email = user.Email,
                Roles = user.RoleSet.Roles.ToList(),
                FullName = user.FullName
            };
        }

        public static ListUsersResponse FromUsersAndPaging(IEnumerable<TimetableUser> users, Paging paging)
            => new ListUsersResponse()
            {
                Paging = paging,
                Users = users.Select(ShortUser.FromUser).ToList()
            };
    }
}