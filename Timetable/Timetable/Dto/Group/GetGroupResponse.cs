namespace Timetable.Timetable.Dto.Group
{
    public class GetGroupResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int AdmissionYear { get; set; }
        public long CuratorId { get; set; }

        public static GetGroupResponse FromGroup(Model.Group group)
            => new GetGroupResponse()
            {
                Id = group.Id,
                Name = group.Name,
                AdmissionYear = group.AdmissionYear,
                CuratorId = group.CuratorId,
                ShortName = group.ShortName
            };
    }
}