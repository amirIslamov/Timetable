using Model.Entities;

namespace API.Timetable.Dto.Teacher;

public class GetTeacherResponse
{
    public long Id { get; set; }
    public string Email { get; set; }
    public FullName FullName { get; set; }
    public string Chair { get; set; }
    public Address Address { get; set; }
    public string PhoneNumber { get; set; }

    public static GetTeacherResponse FromTeacher(Model.Entities.Teacher teacher)
    {
        return new GetTeacherResponse
        {
            Id = teacher.Id,
            Email = teacher.User.Email,
            Address = teacher.User.Address,
            FullName = teacher.User.FullName,
            PhoneNumber = teacher.User.PhoneNumber,
            Chair = teacher.Chair
        };
    }
}