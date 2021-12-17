using Model.Entities;
using Model.Profile.Roles;

namespace API.Timetable.Dto.User;

public class ListUsersResponse
{
    public long Id { get; set; }
    public string Email { get; set; }
    public FullName FullName { get; set; }
    public List<Role> Roles { get; set; }

    public static ListUsersResponse FromUser(TimetableUser user)
    {
        return new ListUsersResponse
        {
            Id = user.Id,
            Email = user.Email,
            Roles = user.RoleSet.Roles.ToList(),
            FullName = user.FullName
        };
    }
}