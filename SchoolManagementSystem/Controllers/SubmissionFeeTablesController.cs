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
    public class SubmissionFeeTableController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: SubmissionFeeTable
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var SubmissionFeeTable = db.SubmissionFeeTable.Include(s => s.ProgrameTable).Include(s => s.StudentTable).Include(s => s.UserTable).Include(s => s.ClassTable).OrderByDescending(s=>s.SubmissionFeeID);
            return View(SubmissionFeeTable.ToList());
        }

        public ActionResult GetByPromotID(string sid)
        {
            int promoteid = Convert.ToInt32(sid);
            var promoterecord = db.StudentPromotTable.Find(promoteid);
            return Json(new { StudentID = promoterecord.StudentID, ClassID = promoterecord.ClassID, ProgrameID = promoterecord.ProgrameSessionTable.ProgrameID }, JsonRequestBehavior.AllowGet);

        }


        // GET: SubmissionFeeTable/Details/5
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
            SubmissionFeeTable submissionFeeTable = db.SubmissionFeeTable.Find(id);
            if (submissionFeeTable == null)
            {
                return HttpNotFound();
            }
            return View(submissionFeeTable);
        }

        // GET: SubmissionFeeTable/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.ProgrameID = new SelectList(db.ProgrameTable, "ProgrameID", "Name");
            ViewBag.StudentID = new SelectList(db.StudentTable, "StudentID", "Name");
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName");
            ViewBag.ClassID = new SelectList(db.ClassTable, "ClassID", "Name");
            return View(new SubmissionFeeTable());
        }

        // POST: SubmissionFeeTable/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubmissionFeeTable submissionFeeTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            submissionFeeTable.UserID = userid;
            if (ModelState.IsValid)
            {
                db.SubmissionFeeTable.Add(submissionFeeTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProgrameID = new SelectList(db.ProgrameTable, "ProgrameID", "Name", submissionFeeTable.ProgrameID);
            ViewBag.StudentID = new SelectList(db.StudentTable, "StudentID", "Name", submissionFeeTable.StudentID);
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName", submissionFeeTable.UserID);
            ViewBag.ClassID = new SelectList(db.ClassTable, "ClassID", "Name", submissionFeeTable.ClassID);
            return View(submissionFeeTable);
        }

        // GET: SubmissionFeeTable/Edit/5
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
            SubmissionFeeTable submissionFeeTable = db.SubmissionFeeTable.Find(id);
            if (submissionFeeTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProgrameID = new SelectList(db.ProgrameTable, "ProgrameID", "Name", submissionFeeTable.ProgrameID);
            ViewBag.StudentID = new SelectList(db.StudentTable, "StudentID", "Name", submissionFeeTable.StudentID);
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName", submissionFeeTable.UserID);
            ViewBag.ClassID = new SelectList(db.ClassTable, "ClassID", "Name", submissionFeeTable.ClassID);
            return View(submissionFeeTable);
        }

        // POST: SubmissionFeeTable/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubmissionFeeTable submissionFeeTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            submissionFeeTable.UserID = userid;
            if (ModelState.IsValid)
            {
                db.Entry(submissionFeeTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProgrameID = new SelectList(db.ProgrameTable, "ProgrameID", "Name", submissionFeeTable.ProgrameID);
            ViewBag.StudentID = new SelectList(db.StudentTable, "StudentID", "Name", submissionFeeTable.StudentID);
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName", submissionFeeTable.UserID);
            ViewBag.ClassID = new SelectList(db.ClassTable, "ClassID", "Name", submissionFeeTable.ClassID);
            return View(submissionFeeTable);
        }

        // GET: SubmissionFeeTable/Delete/5
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
            SubmissionFeeTable submissionFeeTable = db.SubmissionFeeTable.Find(id);
            if (submissionFeeTable == null)
            {
                return HttpNotFound();
            }
            return View(submissionFeeTable);
        }

        // POST: SubmissionFeeTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            SubmissionFeeTable submissionFeeTable = db.SubmissionFeeTable.Find(id);
            db.SubmissionFeeTable.Remove(submissionFeeTable);
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
