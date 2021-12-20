using Model.Entities;

namespace API.Timetable.Dto.Load;

public class GetLoadResponse
{
    public static GetLoadResponse FromLoad(TeacherLoad load)
    {
        return new GetLoadResponse()
        {
            Id = load.Id,
            DisciplineId = load.DisciplineId,
            TeacherId = load.TeacherId,
            Type = load.Type,
            TotalHours = load.TotalHours
        };
    }

    public int TotalHours { get; set; }

    public TeacherLoad.ClassType Type { get; set; }

    public long TeacherId { get; set; }

    public long DisciplineId { get; set; }

    public long Id { get; set; }
}