﻿using Model.Timetable;

namespace Timetable.Timetable.Dto.Student
{
    public class CreateStudentRequest
    {
        public long UserId { get; set; }
        public long GroupId { get; set; }
        
        public ParentContacts MotherContacts { get; set; }
        public ParentContacts FatherContacts { get; set; }
    }
}