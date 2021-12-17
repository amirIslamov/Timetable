using Model.Validation.Abstractions;

namespace Model.Validation;

public class TimetableErrorDescriber
{
    public ValidationError DuplicateSubjectCode(string entityCode)
    {
        return new(
            nameof(DuplicateSubjectCode),
            $"Subject with code {entityCode} is already exists");
    }

    public ValidationError SubjectNameNull()
    {
        return new(
            nameof(SubjectNameNull),
            "Subject name must be provided");
    }

    public ValidationError SubjectCodeNull()
    {
        return new(
            nameof(SubjectCodeNull),
            "Subject code must be provided");
    }

    public IValidationError GroupNameNull()
    {
        return new ValidationError(
            nameof(GroupNameNull),
            "Group name must be provided");
    }

    public IValidationError GroupShortNameNull()
    {
        return new ValidationError(
            nameof(GroupShortNameNull),
            "Group short name must be provided");
    }

    public IValidationError DuplicateGroupShortName(string groupShortName)
    {
        return new ValidationError(
            nameof(DuplicateGroupShortName),
            $"Group with short name {groupShortName} is already exists");
    }

    public IValidationError InvalidCuratorId(long groupCuratorId)
    {
        return new ValidationError(
            nameof(InvalidCuratorId),
            $"Invalid curator id {groupCuratorId}: such teacher does not exist");
    }


    public IValidationError InvalidAdmissionYear(int groupAdmissionYear)
    {
        return new ValidationError(
            nameof(InvalidAdmissionYear),
            $"Group's admission year {groupAdmissionYear} doesn't fall into valid range");
    }

    public IValidationError InvalidSubjectId(long disciplineSubjectId)
    {
        return new ValidationError(
            nameof(InvalidSubjectId),
            $"Invalid subject id {disciplineSubjectId}: such subject does not exist");
    }

    public IValidationError InvalidGroupId(long disciplineSubjectId)
    {
        return new ValidationError(
            nameof(InvalidGroupId),
            $"Invalid group id {disciplineSubjectId}: such group does not exist");
    }

    public IValidationError InvalidSemesterNumber(long disciplineSemesterNumber)
    {
        return new ValidationError(
            nameof(InvalidSemesterNumber),
            $"Discipline's semester number {disciplineSemesterNumber} doesn't fall into valid range");
    }

    public IValidationError InvalidClassroomWorkHours(int disciplineClassroomWorkHours)
    {
        return new ValidationError(
            nameof(InvalidClassroomWorkHours),
            $"Discipline's classroom work hours {disciplineClassroomWorkHours} don't fall into valid range");
    }

    public IValidationError InvalidIndependentWorkHours(int disciplineIndependentWorkHours)
    {
        return new ValidationError(
            nameof(InvalidIndependentWorkHours),
            $"Discipline's independent work hours {disciplineIndependentWorkHours} don't fall into valid range");
    }

    public IValidationError InvalidClassroomWorkHoursTruncation(int disciplineClassroomWorkHours)
    {
        return new ValidationError(
            nameof(InvalidClassroomWorkHoursTruncation),
            $"Invalid classroom work hours truncation: changing work hours to {disciplineClassroomWorkHours} will not be enough to cover the existing loads");
    }

    public IValidationError InvalidDisciplineId(long teacherLoadDisciplineId)
    {
        return new ValidationError(
            nameof(InvalidDisciplineId),
            $"Invalid discipline id {teacherLoadDisciplineId}: such discipline does not exist");
    }

    public IValidationError InvalidTeacherId(long teacherLoadDisciplineId)
    {
        return new ValidationError(
            nameof(InvalidTeacherId),
            $"Invalid teacher id {teacherLoadDisciplineId}: such teacher does not exist");
    }

    public IValidationError InvalidLoadWorkHours(int teacherLoadTotalHours)
    {
        return new ValidationError(
            nameof(InvalidLoadWorkHours),
            $"Load's work hours {teacherLoadTotalHours} don't fall into valid range");
    }

    public IValidationError NotEnoughWorkHours(int teacherLoadTotalHours)
    {
        return new ValidationError(
            nameof(NotEnoughWorkHours),
            $"Cannot create load with total work hours of {teacherLoadTotalHours}: corresponding discipline doesn't have enough work hours");
    }

    public IValidationError EntryAtTheSameTimeExists(long entryTeacherLoadId)
    {
        return new ValidationError(
            nameof(EntryAtTheSameTimeExists),
            "Cannot create entry at provided time: another entry exists");
    }

    public IValidationError InvalidTeacherLoadId(long entryTeacherLoadId)
    {
        return new ValidationError(
            nameof(InvalidTeacherId),
            $"Invalid teacher load id {entryTeacherLoadId}: such teacher load does not exist");
    }
}