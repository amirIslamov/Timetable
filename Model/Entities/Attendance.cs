namespace Model.Entities;

public class Attendance
{
    public long Id { get; set; }
    public AttendanceStatus Status { get; set; }
}

public enum AttendanceStatus
{
    Attended,
    Skipped,
    Late
}