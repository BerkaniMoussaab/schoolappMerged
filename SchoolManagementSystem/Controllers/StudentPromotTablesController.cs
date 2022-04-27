using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using SchoolManagementSystem;

namespace SchoolManagementSystem.Controllers
{
    public class StudentPromotTableController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: StudentPromotTable
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var StudentPromotTable = db.StudentPromotTable.Include(s => s.ClassTable).Include(s => s.ProgrameSessionTable).Include(s => s.SectionTable).Include(s => s.StudentTable).OrderByDescending(s=>s.StudentPromotID);
            return View(StudentPromotTable.ToList());
        }
        public ActionResult GetByStudentID(string sid)
        {
            int studentid = Convert.ToInt32(sid);
            var promoterecord = db.StudentTable.Find(studentid);
            return Json(new { StudentID = promoterecord.StudentID, ClassID = promoterecord.ClassID}, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetPromotClsList(string sid)
        {
         
            int studentid = Convert.ToInt32(sid);
            var student = db.StudentTable.Find(studentid);
            int promoteid = 0;
            try
            {
             promoteid = db.StudentPromotTable.Where(p => p.StudentID == studentid).Max(m=>m.StudentPromotID);
            }
            catch 
            {
                promoteid = 0;
            }
            List<ClassTable> classTable = new List<ClassTable>();
            if (promoteid > 0)
            {
                var promotetable = db.StudentPromotTable.Find(promoteid);
                foreach (var item in db.ClassTable.Where(cls => cls.ClassID > promotetable.ClassID))
                {
                    classTable.Add(new ClassTable { ClassID = item.ClassID, Name = item.Name });
                }
            }
            else
            {
                foreach (var cls in db.ClassTable.Where(cls => cls.ClassID > student.ClassID))
                {
                    classTable.Add(new ClassTable { ClassID = cls.ClassID, Name = cls.Name });
                }
            }
            return Json(new { data = classTable }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAnnulFee(string sid)
        {
            int progsessid = Convert.ToInt32(sid);
            var ps = db.ProgrameSessionTable.Find(progsessid);
            var annulfee = db.AnnualTable.Where(a => a.AnnualID == ps.ProgrameID).SingleOrDefault();
            double? fee = annulfee.Fees;
            return Json(new { fees = fee }, JsonRequestBehavior.AllowGet);
        }



        // GET: StudentPromotTable/Details/5
        public ActionResult Details(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentPromotTable studentPromotTable = db.StudentPromotTable.Find(id);
            if (studentPromotTable == null)
            {
                return HttpNotFound();
            }
            return View(studentPromotTable);
        }

        // GET: StudentPromotTable/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.ClassID = new SelectList(db.ClassTable, "ClassID", "Name");
            ViewBag.ProgrameSessionID = new SelectList(db.ProgrameSessionTable, "ProgrameSessionID", "Details");
            ViewBag.SectionID = new SelectList(db.SectionTable, "SectionID", "SectionName");
            ViewBag.StudentID = new SelectList(db.StudentTable, "StudentID", "Name");
            ViewBag.AnnualFee = new SelectList(db.StudentTable, "StudentID", "Name");
            return View(new StudentPromotTable());
        }

        // POST: StudentPromotTable/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentPromotTable studentPromotTable)
        {   
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            StudentTable student = db.StudentTable.Find(studentPromotTable.StudentID);
            student.ClassID = studentPromotTable.ClassID;
            if (ModelState.IsValid)
            {
                db.StudentPromotTable.Add(studentPromotTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.ClassTable, "ClassID", "Name", studentPromotTable.ClassID);
            ViewBag.ProgrameSessionID = new SelectList(db.ProgrameSessionTable, "ProgrameSessionID", "Details", studentPromotTable.ProgrameSessionID);
            ViewBag.SectionID = new SelectList(db.SectionTable, "SectionID", "SectionName", studentPromotTable.SectionID);
            ViewBag.StudentID = new SelectList(db.StudentTable, "StudentID", "Name", studentPromotTable.StudentID);
            return View(studentPromotTable);
        }

        // GET: StudentPromotTable/Edit/5
        public ActionResult Edit(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentPromotTable studentPromotTable = db.StudentPromotTable.Find(id);
            if (studentPromotTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.ClassTable, "ClassID", "Name", studentPromotTable.ClassID);
            ViewBag.ProgrameSessionID = new SelectList(db.ProgrameSessionTable, "ProgrameSessionID", "Details", studentPromotTable.ProgrameSessionID);
            ViewBag.SectionID = new SelectList(db.SectionTable, "SectionID", "SectionName", studentPromotTable.SectionID);
            ViewBag.StudentID = new SelectList(db.StudentTable, "StudentID", "Name", studentPromotTable.StudentID);
            return View(studentPromotTable);
        }

        // POST: StudentPromotTable/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentPromotTable studentPromotTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            StudentTable student = db.StudentTable.Find(studentPromotTable.StudentID);
            student.ClassID = studentPromotTable.ClassID;
            if (ModelState.IsValid)
            {
                db.Entry(studentPromotTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.ClassTable, "ClassID", "Name", studentPromotTable.ClassID);
            ViewBag.ProgrameSessionID = new SelectList(db.ProgrameSessionTable, "ProgrameSessionID", "Details", studentPromotTable.ProgrameSessionID);
            ViewBag.SectionID = new SelectList(db.SectionTable, "SectionID", "SectionName", studentPromotTable.SectionID);
            ViewBag.StudentID = new SelectList(db.StudentTable, "StudentID", "Name", studentPromotTable.StudentID);
            return View(studentPromotTable);
        }

        // GET: StudentPromotTable/Delete/5
        public ActionResult Delete(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentPromotTable studentPromotTable = db.StudentPromotTable.Find(id);
            if (studentPromotTable == null)
            {
                return HttpNotFound();
            }
            return View(studentPromotTable);
        }

        // POST: StudentPromotTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentPromotTable studentPromotTable = db.StudentPromotTable.Find(id);
            db.StudentPromotTable.Remove(studentPromotTable);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
