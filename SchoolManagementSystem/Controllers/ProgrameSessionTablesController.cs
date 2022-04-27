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
    public class ProgrameSessionTableController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: ProgrameSessionTable
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var programeSessionTable = db.ProgrameSessionTable.Include(p => p.ProgrameTable).Include(p => p.SessionTable).Include(p => p.UserTable);
            return View(programeSessionTable.ToList());
        }

        // GET: ProgrameSessionTable/Details/5
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
            ProgrameSessionTable programeSessionTable = db.ProgrameSessionTable.Find(id);
            if (programeSessionTable == null)
            {
                return HttpNotFound();
            }
            return View(programeSessionTable);
        }

        // GET: ProgrameSessionTable/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.ProgrameID = new SelectList(db.ProgrameTable, "ProgrameID", "Name");
            ViewBag.SessionID = new SelectList(db.SessionTable, "SessionID", "Name");
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName");
            return View(new ProgrameSessionTable());
        }

        // POST: ProgrameSessionTable/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProgrameSessionTable programeSessionTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            programeSessionTable.UserID = userid;

            if (ModelState.IsValid)
            {
                var sessionname = db.SessionTable.Where(s => s.SessionID == programeSessionTable.SessionID).SingleOrDefault();
                var programename = db.ProgrameTable.Where(s => s.ProgrameID == programeSessionTable.ProgrameID).SingleOrDefault();
                if (sessionname != null)
                {
                    if (!programeSessionTable.Details.Contains(sessionname.Name))
                    {
                        var details = "(" + sessionname.Name + "-" + (programename != null ? programename.Name : "") + ") " + programeSessionTable.Details;
                        programeSessionTable.Details = details;
                    }
                }

                db.ProgrameSessionTable.Add(programeSessionTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProgrameID = new SelectList(db.ProgrameTable, "ProgrameID", "Name", programeSessionTable.ProgrameID);
            ViewBag.SessionID = new SelectList(db.SessionTable, "SessionID", "Name", programeSessionTable.SessionID);
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName", programeSessionTable.UserID);
            return View(programeSessionTable);
        }

        // GET: ProgrameSessionTable/Edit/5
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
            ProgrameSessionTable programeSessionTable = db.ProgrameSessionTable.Find(id);
            if (programeSessionTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProgrameID = new SelectList(db.ProgrameTable, "ProgrameID", "Name", programeSessionTable.ProgrameID);
            ViewBag.SessionID = new SelectList(db.SessionTable, "SessionID", "Name", programeSessionTable.SessionID);
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName", programeSessionTable.UserID);
            return View(programeSessionTable);
        }

        // POST: ProgrameSessionTable/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( ProgrameSessionTable programeSessionTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            programeSessionTable.UserID = userid;
            if (ModelState.IsValid)
            {
                var sessionname = db.SessionTable.Where(s=>s.SessionID == programeSessionTable.SessionID).SingleOrDefault();
                var programename = db.ProgrameTable.Where(s => s.ProgrameID == programeSessionTable.ProgrameID).SingleOrDefault();
                if (sessionname != null)
                {
                    if (!programeSessionTable.Details.Contains(sessionname.Name))
                    {
                        var details = "(" + sessionname.Name + "-" + (programename != null ? programename.Name : "") + ") " + programeSessionTable.Details;
                        programeSessionTable.Details = details;
                    }
                }
                db.Entry(programeSessionTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProgrameID = new SelectList(db.ProgrameTable, "ProgrameID", "Name", programeSessionTable.ProgrameID);
            ViewBag.SessionID = new SelectList(db.SessionTable, "SessionID", "Name", programeSessionTable.SessionID);
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName", programeSessionTable.UserID);
            return View(programeSessionTable);
        }

        // GET: ProgrameSessionTable/Delete/5
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
            ProgrameSessionTable programeSessionTable = db.ProgrameSessionTable.Find(id);
            if (programeSessionTable == null)
            {
                return HttpNotFound();
            }
            return View(programeSessionTable);
        }

        // POST: ProgrameSessionTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ProgrameSessionTable programeSessionTable = db.ProgrameSessionTable.Find(id);
            db.ProgrameSessionTable.Remove(programeSessionTable);
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
