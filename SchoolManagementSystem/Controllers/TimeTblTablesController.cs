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
    public class TimeTblTableController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: TimeTblTable
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
 
            var TimeTblTable = db.TimeTblTable.Include(t => t.ClassSubjectTable).Include(t => t.StaffTable).Include(t => t.UserTable).OrderByDescending(e=>e.TimeTableID);
            return View(TimeTblTable.ToList());
        }

        // GET: TimeTblTable/Details/5
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
            TimeTblTable timeTblTable = db.TimeTblTable.Find(id);
            if (timeTblTable == null)
            {
                return HttpNotFound();
            }
            return View(timeTblTable);
        }

        // GET: TimeTblTable/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
    
            ViewBag.ClassSubjectID = new SelectList(db.ClassSubjectTable.Where(s => s.IsActive == true), "ClassSubjectID", "Name");
            ViewBag.StaffID = new SelectList(db.StaffTable.Where(s=>s.IsActive == true), "StaffID", "Name");
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName");
            ViewBag.GroupeId = new SelectList(db.GroupeTable, "GroupeId", "Name");
            return View(new TimeTblTable());
        }

        // POST: TimeTblTable/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TimeTblTable timeTblTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            timeTblTable.UserID = userid;
            if (ModelState.IsValid)
            {
                db.TimeTblTable.Add(timeTblTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassSubjectID = new SelectList(db.ClassSubjectTable.Where(s => s.IsActive == true), "ClassSubjectID", "Name", timeTblTable.ClassSubjectID);
            ViewBag.StaffID = new SelectList(db.StaffTable.Where(s => s.IsActive == true), "StaffID", "Name", timeTblTable.StaffID);
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName", timeTblTable.UserID);
            ViewBag.GroupeId = new SelectList(db.GroupeTable, "GroupeId", "Name", timeTblTable.GroupeId);
            return View(timeTblTable);
        }

        // GET: TimeTblTable/Edit/5
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
            TimeTblTable timeTblTable = db.TimeTblTable.Find(id);
            if (timeTblTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassSubjectID = new SelectList(db.ClassSubjectTable.Where(s => s.IsActive == true), "ClassSubjectID", "Name", timeTblTable.ClassSubjectID);
            ViewBag.StaffID = new SelectList(db.StaffTable.Where(s => s.IsActive == true), "StaffID", "Name", timeTblTable.StaffID);
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName", timeTblTable.UserID);
            ViewBag.GroupeId = new SelectList(db.GroupeTable, "GroupeId", "Name", timeTblTable.GroupeId);
            return View(timeTblTable);
        }

        // POST: TimeTblTable/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TimeTblTable timeTblTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            timeTblTable.UserID = userid;
            if (ModelState.IsValid)
            {
                db.Entry(timeTblTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassSubjectID = new SelectList(db.ClassSubjectTable.Where(s => s.IsActive == true), "ClassSubjectID", "Name", timeTblTable.ClassSubjectID);
            ViewBag.StaffID = new SelectList(db.StaffTable.Where(s => s.IsActive == true), "StaffID", "Name", timeTblTable.StaffID);
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName", timeTblTable.UserID);
            ViewBag.GroupeId = new SelectList(db.GroupeTable, "GroupeId", "Name", timeTblTable.GroupeId);
            return View(timeTblTable);
        }

        // GET: TimeTblTable/Delete/5
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
            TimeTblTable timeTblTable = db.TimeTblTable.Find(id);
            if (timeTblTable == null)
            {
                return HttpNotFound();
            }
            return View(timeTblTable);
        }

        // POST: TimeTblTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            TimeTblTable timeTblTable = db.TimeTblTable.Find(id);
            db.TimeTblTable.Remove(timeTblTable);
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
