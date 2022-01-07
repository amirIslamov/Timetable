namespace API.Timetable.Dto.Classroom;

public class GetClassroomResponse
{
    public long Id { get; set; }
    public int ClassroomNum { get; set; }
    public string Pavilion { get; set; }

    public static GetClassroomResponse FromClassroom(Model.Entities.Classroom classroom)
        => new GetClassroomResponse()
        {
            Id = classroom.Id,
            Pavilion = classroom.Pavilion,
            ClassroomNum = classroom.ClassroomNumber
        };
}