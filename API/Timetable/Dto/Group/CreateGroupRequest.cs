namespace API.Timetable.Dto.Group;

public class CreateGroupRequest
{
    public string Name { get; set; }
    public string ShortName { get; set; }
    public int AdmissionYear { get; set; }
    public long CuratorId { get; set; }
}