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
    public class AnnualTablesController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: AnnualTables
        public ActionResult Index()
        {
            var annualTable = db.AnnualTable.Include(a => a.ProgrameTable).Include(a => a.UserTable);
            return View(annualTable.ToList());
        }

        // GET: AnnualTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnnualTable annualTable = db.AnnualTable.Find(id);
            if (annualTable == null)
            {
                return HttpNotFound();
            }
            return View(annualTable);
        }

        // GET: AnnualTables/Create
        public ActionResult Create()
        {
            ViewBag.ProgrameID = new SelectList(db.ProgrameTable, "ProgrameID", "Name");
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName");
            return View();
        }

        // POST: AnnualTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AnnualID,UserID,ProgrameID,Title,Description,Fees,IsActive")] AnnualTable annualTable)
        {
            if (ModelState.IsValid)
            {
                db.AnnualTable.Add(annualTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProgrameID = new SelectList(db.ProgrameTable, "ProgrameID", "Name", annualTable.ProgrameID);
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName", annualTable.UserID);
            return View(annualTable);
        }

        // GET: AnnualTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnnualTable annualTable = db.AnnualTable.Find(id);
            if (annualTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProgrameID = new SelectList(db.ProgrameTable, "ProgrameID", "Name", annualTable.ProgrameID);
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName", annualTable.UserID);
            return View(annualTable);
        }

        // POST: AnnualTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AnnualID,UserID,ProgrameID,Title,Description,Fees,IsActive")] AnnualTable annualTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(annualTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProgrameID = new SelectList(db.ProgrameTable, "ProgrameID", "Name", annualTable.ProgrameID);
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName", annualTable.UserID);
            return View(annualTable);
        }

        // GET: AnnualTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnnualTable annualTable = db.AnnualTable.Find(id);
            if (annualTable == null)
            {
                return HttpNotFound();
            }
            return View(annualTable);
        }

        // POST: AnnualTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AnnualTable annualTable = db.AnnualTable.Find(id);
            db.AnnualTable.Remove(annualTable);
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
