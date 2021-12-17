namespace API.Timetable.Dto.Group;

public class ListGroupsResponse
{
    public long Id { get; set; }
    public string ShortName { get; set; }

    public static ListGroupsResponse FromGroup(Model.Entities.Group group)
    {
        return new ListGroupsResponse
        {
            Id = group.Id,
            ShortName = group.ShortName
        };
    }
}