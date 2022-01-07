namespace API.Timetable.Dto.Classroom;

public class ListClassroomsResponse
{
    public long Id { get; set; }
    public int ClassroomNum { get; set; }
    public string Pavilion { get; set; }

    public static ListClassroomsResponse FromClassroom(Model.Entities.Classroom classroom)
        => new ListClassroomsResponse()
        {
            Id = classroom.Id,
            Pavilion = classroom.Pavilion,
            ClassroomNum = classroom.ClassroomNumber
        };
}