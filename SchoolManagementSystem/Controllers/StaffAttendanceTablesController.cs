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
    public class StaffAttendanceTableController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: StaffAttendanceTable
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var StaffAttendanceTable = db.StaffAttendanceTable.Include(s => s.StaffTable);
            return View(StaffAttendanceTable.ToList());
        }
        public ActionResult teacherAttendance()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var StaffAttendanceTable = db.StaffAttendanceTable.Include(s => s.StaffTable);
            return View(StaffAttendanceTable.ToList());
        }

        // GET: StaffAttendanceTable/Details/5
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
            StaffAttendanceTable staffAttendanceTable = db.StaffAttendanceTable.Find(id);
            if (staffAttendanceTable == null)
            {
                return HttpNotFound();
            }
            return View(staffAttendanceTable);
        }

        // GET: StaffAttendanceTable/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.StaffID = new SelectList(db.StaffTable.Where(s=>s.IsActive == true), "StaffID", "Name");
            return View(new StaffAttendanceTable());
        }

        // POST: StaffAttendanceTable/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StaffAttendanceID,StaffID,AttendDate,ComingTime,ClosingTime")] StaffAttendanceTable staffAttendanceTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                db.StaffAttendanceTable.Add(staffAttendanceTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StaffID = new SelectList(db.StaffTable, "StaffID", "Name", staffAttendanceTable.StaffID);
            return View(staffAttendanceTable);
        }

        // GET: StaffAttendanceTable/Edit/5
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
            StaffAttendanceTable staffAttendanceTable = db.StaffAttendanceTable.Find(id);
            if (staffAttendanceTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.StaffID = new SelectList(db.StaffTable, "StaffID", "Name", staffAttendanceTable.StaffID);
            return View(staffAttendanceTable);
        }

        // POST: StaffAttendanceTable/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StaffAttendanceID,StaffID,AttendDate,ComingTime,ClosingTime")] StaffAttendanceTable staffAttendanceTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                db.Entry(staffAttendanceTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StaffID = new SelectList(db.StaffTable, "StaffID", "Name", staffAttendanceTable.StaffID);
            return View(staffAttendanceTable);
        }

        // GET: StaffAttendanceTable/Delete/5
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
            StaffAttendanceTable staffAttendanceTable = db.StaffAttendanceTable.Find(id);
            if (staffAttendanceTable == null)
            {
                return HttpNotFound();
            }
            return View(staffAttendanceTable);
        }

        // POST: StaffAttendanceTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            StaffAttendanceTable staffAttendanceTable = db.StaffAttendanceTable.Find(id);
            db.StaffAttendanceTable.Remove(staffAttendanceTable);
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
