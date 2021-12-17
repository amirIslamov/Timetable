using Model.Entities;

namespace API.Timetable.Dto.Student;

public class ListStudentsResponse
{
    public long Id { get; set; }
    public string Email { get; set; }
    public FullName FullName { get; set; }
    public long GroupId { get; set; }

    public static ListStudentsResponse FromStudent(Model.Entities.Student student)
    {
        return new ListStudentsResponse
        {
            Id = student.Id,
            Email = student.User.Email,
            FullName = student.User.FullName,
            GroupId = student.GroupId
        };
    }
}