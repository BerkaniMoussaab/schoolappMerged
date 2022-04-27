
using SchoolManagementSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class EmployeeCertificateController : Controller
    {
        // GET: EmployeeCertificate
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();
        public ActionResult ExperienceC(int? id)
        {
            var employee = db.StaffTable.Where(s => s.StaffID == id).FirstOrDefault();
            ViewBag.FromDate = employee.RegistrationDate.ToString("yyyy/MM/dd");
            if (employee.StaffAttendanceTable != null)
            {
                ViewBag.ToDate = employee.StaffAttendanceTable.OrderByDescending(s => s.AttendDate).FirstOrDefault().AttendDate;
            }
            else
            {
                ViewBag.ToDate = DateTime.Now.ToString("yyyy/MM/dd");
            }
            return View(employee);
        }
    }
}