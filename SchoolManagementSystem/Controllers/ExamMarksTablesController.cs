using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace SchoolManagementSystem.Controllers
{
    public class ExamMarksTableController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: ExamMarksTable
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var ExamMarksTable = db.ExamMarksTable.Include(e => e.ExamTable).Include(e => e.UserTable).Include(e => e.StudentTable).Include(e => e.ClassSubjectTable);
            return View(ExamMarksTable.ToList());
        }

        // GET: ExamMarksTable/Details/5
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
            ExamMarksTable examMarksTable = db.ExamMarksTable.Find(id);
            if (examMarksTable == null)
            {
                return HttpNotFound();
            }
            return View(examMarksTable);
        }

        // GET: ExamMarksTable/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.ExamID = new SelectList(db.ExamTable, "ExamID", "Title");
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName");
            ViewBag.StudentID = new SelectList(db.StudentTable, "StudentID", "Name");
            ViewBag.ClassSubjectID = new SelectList(db.ClassSubjectTable, "ClassSubjectID", "Name");
            ViewBag.ExamTimeID = new SelectList(db.ExamTime, "ExamTimeId", "Name");
            return View();
        }

        // POST: ExamMarksTable/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MarksID,ExamID,ClassSubjectID,StudentID,UserID,TotalMarks,ObtainMarks,ExamTimeID")] ExamMarksTable examMarksTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.ExamMarksTable.Add(examMarksTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExamID = new SelectList(db.ExamTable, "ExamID", "Title", examMarksTable.ExamID);
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName", examMarksTable.UserID);
            ViewBag.StudentID = new SelectList(db.StudentTable, "StudentID", "Name", examMarksTable.StudentID);
            ViewBag.ClassSubjectID = new SelectList(db.ClassSubjectTable, "ClassSubjectID", "Name", examMarksTable.ClassSubjectID);
        
            return View(examMarksTable);
        }

        // GET: ExamMarksTable/Edit/5
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
            ExamMarksTable examMarksTable = db.ExamMarksTable.Find(id);
            if (examMarksTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExamID = new SelectList(db.ExamTable, "ExamID", "Title", examMarksTable.ExamID);
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName", examMarksTable.UserID);
            ViewBag.StudentID = new SelectList(db.StudentTable, "StudentID", "Name", examMarksTable.StudentID);
            ViewBag.ClassSubjectID = new SelectList(db.ClassSubjectTable, "ClassSubjectID", "Name", examMarksTable.ClassSubjectID);
 
            return View(examMarksTable);
        }
        public ActionResult GetByPromotID(string sid)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            int promoteid = Convert.ToInt32(sid);
            var promoterecord = db.StudentPromotTable.Find(promoteid);
            List<StudentTable> stdlist = new List<StudentTable>();
            stdlist.Add(new StudentTable { StudentID = promoterecord.StudentID, Name = promoterecord.StudentTable.Name });
            List<ClassSubjectTable> listsubjects = new List<ClassSubjectTable>();
            var classsubjects = db.ClassSubjectTable.Where(cls => cls.ClassID == promoterecord.ClassID && cls.IsActive == true);
            foreach (var subj in classsubjects)
            {
                listsubjects.Add(new ClassSubjectTable
                {
                    ClassSubjectID = subj.ClassSubjectID,
                    Name = subj.Name
                });
            }
            return Json(new { std = stdlist, subjects = listsubjects }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetTotalMarks(string sid)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            int classsubjectid = Convert.ToInt32(sid);
            var totalmarks = db.ClassSubjectTable.Find(classsubjectid).SubjectTable.TotalMarks;
            return Json(new { data = totalmarks }, JsonRequestBehavior.AllowGet);

        }
        // POST: ExamMarksTable/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MarksID,ExamID,ClassSubjectID,StudentID,UserID,TotalMarks,ObtainMarks,ExamTimeID")] ExamMarksTable examMarksTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Entry(examMarksTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExamID = new SelectList(db.ExamTable, "ExamID", "Title", examMarksTable.ExamID);
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName", examMarksTable.UserID);
            ViewBag.StudentID = new SelectList(db.StudentTable, "StudentID", "Name", examMarksTable.StudentID);
            ViewBag.ClassSubjectID = new SelectList(db.ClassSubjectTable, "ClassSubjectID", "Name", examMarksTable.ClassSubjectID);
       
            return View(examMarksTable);
        }

        // GET: ExamMarksTable/Delete/5
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
            ExamMarksTable examMarksTable = db.ExamMarksTable.Find(id);
            if (examMarksTable == null)
            {
                return HttpNotFound();
            }
            return View(examMarksTable);
        }

        // POST: ExamMarksTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ExamMarksTable examMarksTable = db.ExamMarksTable.Find(id);
            db.ExamMarksTable.Remove(examMarksTable);
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
