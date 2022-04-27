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
    public class StaffAbsenceController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: StaffAbsence
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var StaffAbsence = db.StaffAbsence.Include(s => s.StaffTable);
            return View(StaffAbsence.ToList());
        }

        // GET: StaffAbsence/Details/5
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
            StaffAbsence staffAbsence = db.StaffAbsence.Find(id);
            if (staffAbsence == null)
            {
                return HttpNotFound();
            }
            return View(staffAbsence);
        }

        // GET: StaffAbsence/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.StaffID = new SelectList(db.StaffTable, "StaffID", "Name");
            return View();
        }

        // POST: StaffAbsence/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StaffAbsenceID,StaffID,AbsenceStartDate,AbsenceEndDate,Justification,Status")] StaffAbsence staffAbsence)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.StaffAbsence.Add(staffAbsence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StaffID = new SelectList(db.StaffTable, "StaffID", "Name", staffAbsence.StaffID);
            return View(staffAbsence);
        }

        // GET: StaffAbsence/Edit/5
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
            StaffAbsence staffAbsence = db.StaffAbsence.Find(id);
            if (staffAbsence == null)
            {
                return HttpNotFound();
            }
            ViewBag.StaffID = new SelectList(db.StaffTable, "StaffID", "Name", staffAbsence.StaffID);
            return View(staffAbsence);
        }

        // POST: StaffAbsence/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StaffAbsenceID,StaffID,AbsenceStartDate,AbsenceEndDate,Justification,Status")] StaffAbsence staffAbsence)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Entry(staffAbsence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StaffID = new SelectList(db.StaffTable, "StaffID", "Name", staffAbsence.StaffID);
            return View(staffAbsence);
        }

        // GET: StaffAbsence/Delete/5
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
            StaffAbsence staffAbsence = db.StaffAbsence.Find(id);
            if (staffAbsence == null)
            {
                return HttpNotFound();
            }
            return View(staffAbsence);
        }

        // POST: StaffAbsence/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            StaffAbsence staffAbsence = db.StaffAbsence.Find(id);
            db.StaffAbsence.Remove(staffAbsence);
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
