
using SchoolManagementSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class StudentCertificateReportController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();
        // GET: StudentCertificateReport
        public ActionResult LeavingC(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Message = "Ready to Print";
           var student = db.StudentPromotTable.Where(std => std.StudentID == id && std.IsActive == true).FirstOrDefault();
            if (student == null)
            {
                ViewBag.Message = "Already Printed!";
                student = db.StudentPromotTable.Where(std => std.StudentID == id).OrderByDescending(e=>e.StudentPromotID).FirstOrDefault();
                return View(student);
            }
            return View(student);
        }

        [HttpPost]
        public ActionResult PrintLeavingC(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var student = db.StudentPromotTable.Where(std => std.StudentID == id && std.IsActive == true).FirstOrDefault();
            if (student == null)
            {
                ViewBag.Message = "Already Print! Please Contact to Adminstration Department.";
                student = db.StudentPromotTable.Where(std => std.StudentID == id).OrderByDescending(e => e.StudentPromotID).FirstOrDefault();
                return View("LeavingC", student);
            }
            student.IsActive = false;
            db.Entry(student).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            ViewBag.Message = "Print Successfully";
            return View("LeavingC", student);
        }

        public ActionResult CharacterC(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Message = "Ready to Print";
            var student = db.StudentPromotTable.Where(std => std.StudentID == id && std.IsActive == true).FirstOrDefault();
            if (student == null)
            {
                ViewBag.Message = "Already Printed!";
                student = db.StudentPromotTable.Where(std => std.StudentID == id).OrderByDescending(e => e.StudentPromotID).FirstOrDefault();
                return View(student);
            }
            return View(student);
        }

        public ActionResult ProvisionalC(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Message = "Ready to Print";
            var student = db.StudentPromotTable.Where(std => std.StudentID == id && std.IsActive == true).FirstOrDefault();
            if (student == null)
            {
                ViewBag.Message = "Already Printed!";
                student = db.StudentPromotTable.Where(std => std.StudentID == id).OrderByDescending(e => e.StudentPromotID).FirstOrDefault();
                return View(student);
            }
            return View(student);
        }
    }
}