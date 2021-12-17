using Model.Entities;

namespace API.Timetable.Dto.Teacher;

public class ListTeachersResponse
{
    public long Id { get; set; }
    public string Email { get; set; }
    public FullName FullName { get; set; }
    public string Chair { get; set; }

    public static ListTeachersResponse FromTeacher(Model.Entities.Teacher teacher)
    {
        return new ListTeachersResponse
        {
            Id = teacher.Id,
            Email = teacher.User.Email,
            FullName = teacher.User.FullName,
            Chair = teacher.Chair
        };
    }
}