
using SchoolManagementSystem;
using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class AttendanceReportsController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();
        // GET: AttendanceReports
        public ActionResult StudentAttendance(int? id)
        {

            if (id == 0)
            {
                int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
                id = db.StudentTable.Where(e => e.UserID == userid).FirstOrDefault().StudentID;
            }

            //var classid = db.StudentPromotTables.Where(p => p.StudentID == id && p.IsActive == true).FirstOrDefault().ClassID;
            var studentAttendance = db.AbsenceTable.Where(a => a.StudentID == id /* && a.ClassID == classid*/).OrderByDescending(a=>a.AbsenceID);
            return View(studentAttendance);
        }

        public ActionResult AllStudent()
        {
            var studentattandance = db.AbsenceTable.OrderByDescending(a => a.AbsenceDate);
            return View(studentattandance);
        }

        public ActionResult Staff(int? id)
        {

            if (id == 0)
            {
                int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
                var staff = db.StaffTable.Where(e => e.UserID == userid).FirstOrDefault();
                id = staff != null ? staff.StaffID : 0;
            }
            List<StaffAttendanceReport> staffAttendancelist = new List<StaffAttendanceReport>();
            var staffattendance = db.StaffAttendanceTable.Where(a => a.StaffAttendanceID == id).OrderByDescending(a => a.StaffAttendanceID);
            foreach (var item in staffattendance)
            {
                       var attend = new StaffAttendanceReport();
                attend.Name = item.StaffTable.Name;
                attend.Designation = item.StaffTable.DesignationTable.Title;
                attend.AttendDate = item.AttendDate;
                attend.ComingTime = item.ComingTime;
                attend.ClosingTime = item.ClosingTime;
                attend.DutyHour = ((TimeSpan)item.ClosingTime - (TimeSpan)item.ComingTime);

     

            staffAttendancelist.Add(attend);
            }


            return View(staffAttendancelist);
        }

        public ActionResult AllStaff()
        {

            List<StaffAttendanceReport> staffAttendancelist = new List<StaffAttendanceReport>();
            var staffattendance = db.StaffAttendanceTable.OrderByDescending(a => a.StaffAttendanceID);
            foreach (var item in staffattendance)
            {
                var attend = new StaffAttendanceReport();
                attend.Name = item.StaffTable.Name;
                attend.Designation = item.StaffTable.DesignationTable.Title;
                attend.AttendDate = item.AttendDate;
                attend.ComingTime = item.ComingTime;
                attend.ClosingTime = item.ClosingTime;
                attend.DutyHour = ((TimeSpan)item.ClosingTime - (TimeSpan)item.ComingTime);
                staffAttendancelist.Add(attend);
            }
            return View(staffAttendancelist);
        }
    }
}