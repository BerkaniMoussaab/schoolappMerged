using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManagementSystem.Models
{
    public class StaffAttendanceReport
    {
        public int StaffAttendanceID { get; set; }

        public string Designation { get; set; }

        public string Name { get; set; }
        public System.DateTime AttendDate { get; set; }

        public Nullable<System.TimeSpan> ComingTime { get; set; }
   
        public Nullable<System.TimeSpan> ClosingTime { get; set; }

        public TimeSpan DutyHour { get; set; }
    }
}