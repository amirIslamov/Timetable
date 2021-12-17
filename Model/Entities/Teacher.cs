namespace Model.Entities;

public class Teacher
{
    public long Id { get; set; }

    public string Chair { get; set; }
    public TimetableUser User { get; set; }

    public List<TeacherLoad> TeacherLoads { get; set; }
}