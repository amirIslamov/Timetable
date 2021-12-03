﻿using System.Collections.Generic;
using Model.Timetable;

namespace Timetable.Timetable.Model
{
    public class Group
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int AdmissionYear { get; set; }
        
        public long CuratorId { get; set; }
        public Teacher Curator { get; set; }
        
        public List<Student> Students { get; set; }
        public List<ClassGroup> ClassGroups { get; set; }
    }
}