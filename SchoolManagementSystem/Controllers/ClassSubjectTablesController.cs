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
    public class ClassSubjectTablesController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: ClassSubjectTable
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var ClassSubjectTable = db.ClassSubjectTable.Include(c => c.ClassTable).Include(c => c.SubjectTable);
            return View(ClassSubjectTable.ToList());
        }

        // GET: ClassSubjectTable/Details/5
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
            ClassSubjectTable classSubjectTable = db.ClassSubjectTable.Find(id);
            if (classSubjectTable == null)
            {
                return HttpNotFound();
            }
            return View(classSubjectTable);
        }

        // GET: ClassSubjectTable/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.ClassID = new SelectList(db.ClassTable.Where(c => c.IsActive == true), "ClassID", "Name");
            ViewBag.SubjectID = new SelectList(db.SubjectTable, "SubjectID", "Name");
            return View(new ClassSubjectTable());
        }

        // POST: ClassSubjectTable/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClassSubjectTable classSubjectTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }


            if (ModelState.IsValid)
            {
                var classname = db.ClassTable.Where(c => c.ClassID == classSubjectTable.ClassID).SingleOrDefault();
                if (classname != null)
                {
                    if (!classSubjectTable.Name.Contains(classname.Name))
                    {
                        classSubjectTable.Name = classSubjectTable.Name + "-" + classname.Name;
                    }
                }
                db.ClassSubjectTable.Add(classSubjectTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.ClassTable.Where(c => c.IsActive == true), "ClassID", "Name", classSubjectTable.ClassID);
            ViewBag.SubjectID = new SelectList(db.SubjectTable, "SubjectID", "Name", classSubjectTable.SubjectID);
            return View(classSubjectTable);
        }

        // GET: ClassSubjectTable/Edit/5
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
            ClassSubjectTable classSubjectTable = db.ClassSubjectTable.Find(id);
            if (classSubjectTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.ClassTable.Where(c => c.IsActive == true), "ClassID", "Name", classSubjectTable.ClassID);
            ViewBag.SubjectID = new SelectList(db.SubjectTable, "SubjectID", "Name", classSubjectTable.SubjectID);
            return View(classSubjectTable);
        }

        // POST: ClassSubjectTable/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClassSubjectTable classSubjectTable)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }


            if (ModelState.IsValid)
            {
                var classname = db.ClassTable.Where(c => c.ClassID == classSubjectTable.ClassID).SingleOrDefault();
                if (classname != null)
                {
                    if (!classSubjectTable.Name.Contains(classname.Name))
                    {
                        classSubjectTable.Name = classSubjectTable.Name + "-" + classname.Name;
                    }
                }
                db.Entry(classSubjectTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.ClassTable.Where(c => c.IsActive == true), "ClassID", "Name", classSubjectTable.ClassID);
            ViewBag.SubjectID = new SelectList(db.SubjectTable, "SubjectID", "Name", classSubjectTable.SubjectID);
            return View(classSubjectTable);
        }

        // GET: ClassSubjectTable/Delete/5
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
            ClassSubjectTable classSubjectTable = db.ClassSubjectTable.Find(id);
            if (classSubjectTable == null)
            {
                return HttpNotFound();
            }
            return View(classSubjectTable);
        }

        // POST: ClassSubjectTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ClassSubjectTable classSubjectTable = db.ClassSubjectTable.Find(id);
            db.ClassSubjectTable.Remove(classSubjectTable);
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
