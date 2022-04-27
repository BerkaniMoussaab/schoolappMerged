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
    public class ExamTimeController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: ExamTime
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var ExamTime = db.ExamTime.Include(e => e.ClassSubjectTable).Include(e => e.ClassTable).Include(e => e.ExamTable).Include(e => e.UserTable);
            return View(ExamTime.ToList());
        }

        // GET: ExamTime/Details/5
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
            ExamTime examTime = db.ExamTime.Find(id);
            if (examTime == null)
            {
                return HttpNotFound();
            }
            return View(examTime);
        }

        // GET: ExamTime/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.ClassSubjectID = new SelectList(db.ClassSubjectTable, "ClassSubjectID", "Name");
            ViewBag.ClassID = new SelectList(db.ClassTable, "ClassID", "Name");
            ViewBag.ExamID = new SelectList(db.ExamTable, "ExamID", "Title");
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName");
            return View();
        }

        // POST: ExamTime/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( ExamTime examTime)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
                {
                    return RedirectToAction("Login", "Home");
                }
                int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
                examTime.UserID = userid;
                db.ExamTime.Add(examTime);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassSubjectID = new SelectList(db.ClassSubjectTable, "ClassSubjectID", "Name", examTime.ClassSubjectID);
            ViewBag.ClassID = new SelectList(db.ClassTable, "ClassID", "Name", examTime.ClassID);
            ViewBag.ExamID = new SelectList(db.ExamTable, "ExamID", "Title", examTime.ExamID);
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName", examTime.UserID);
            return View(examTime);
        }
        public JsonResult GetSubjects(int ClassID)
        {
           
            db.Configuration.ProxyCreationEnabled = false;
            List<ClassSubjectTable> classSubjects = db.ClassSubjectTable.Where(x => x.ClassID == ClassID).ToList();
            return Json(classSubjects, JsonRequestBehavior.AllowGet);

        }

        // GET: ExamTime/Edit/5
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
            ExamTime examTime = db.ExamTime.Find(id);
            if (examTime == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassSubjectID = new SelectList(db.ClassSubjectTable, "ClassSubjectID", "Name", examTime.ClassSubjectID);
            ViewBag.ClassID = new SelectList(db.ClassTable, "ClassID", "Name", examTime.ClassID);
            ViewBag.ExamID = new SelectList(db.ExamTable, "ExamID", "Title", examTime.ExamID);
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName", examTime.UserID);
            return View(examTime);
        }

        // POST: ExamTime/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExamTimeId,Date,StartTime,EndTime,ClassID,ExamID,ClassSubjectID,Name,UserID")] ExamTime examTime)
        {
            if (ModelState.IsValid)
            {
                db.Entry(examTime).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassSubjectID = new SelectList(db.ClassSubjectTable, "ClassSubjectID", "Name", examTime.ClassSubjectID);
            ViewBag.ClassID = new SelectList(db.ClassTable, "ClassID", "Name", examTime.ClassID);
            ViewBag.ExamID = new SelectList(db.ExamTable, "ExamID", "Title", examTime.ExamID);
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName", examTime.UserID);
            return View(examTime);
        }

        // GET: ExamTime/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamTime examTime = db.ExamTime.Find(id);
            if (examTime == null)
            {
                return HttpNotFound();
            }
            return View(examTime);
        }

        // POST: ExamTime/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExamTime examTime = db.ExamTime.Find(id);
            db.ExamTime.Remove(examTime);
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
