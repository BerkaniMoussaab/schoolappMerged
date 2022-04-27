
using SchoolManagementSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class ExamReportsController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();
        // GET: ExamReports
        public ActionResult PrintDMC()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.ExamID = new SelectList(db.ExamTable, "ExamID", "Title");
            return View(new List<ExamMarksTable>());
        }


       [HttpPost()]
        public ActionResult PrintDMC(int? promoteid, int? examid)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            int usertypeid = Convert.ToInt32(Convert.ToString(Session["UserTypeID"]));
        
        ViewBag.ExamID = new SelectList(db.ExamTable, "ExamID", "Title");
           
                
            var promoterecord = db.StudentPromotTable.Find(promoteid);
            if (usertypeid == 4)
               

                if (promoterecord != null)
            {

                var listmarks = db.ExamMarksTable.Where(e => e.ClassSubjectTable.ClassID == promoterecord.ClassID && e.ExamID == examid && e.StudentID == promoterecord.StudentID);
                if (listmarks != null)
                {

                }
                return View(listmarks);
            }
            
            return View(new List<ExamMarksTable>());
        }
    }
}