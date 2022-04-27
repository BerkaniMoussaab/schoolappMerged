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
    public class HomeWorkTableController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: HomeWorkTable
        public ActionResult Index()
        {
            var HomeWorkTable = db.HomeWorkTable.Include(h => h.ClassTable).Include(h => h.HomeWorkTypeTable).Include(h => h.GroupeTable);
            return View(HomeWorkTable.ToList());
        }

        // GET: HomeWorkTable/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeWorkTable homeWorkTable = db.HomeWorkTable.Find(id);
            if (homeWorkTable == null)
            {
                return HttpNotFound();
            }
            return View(homeWorkTable);
        }

        // GET: HomeWorkTable/Create
        public ActionResult Create()
        {
            ViewBag.ClassID = new SelectList(db.ClassTable, "ClassID", "Name");
            ViewBag.HomeWorkTypeID = new SelectList(db.HomeWorkTypeTable, "HomeWorkTypeID", "HomeWorkTypeName");
            ViewBag.GroupeID = new SelectList(db.GroupeTable, "GroupeID", "Name");
            return View();
        }

        // POST: HomeWorkTable/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HomeWorkID,HomeWorkTypeID,ClassID,StudentID,HomeWorkTitle,HomeWorkDescription,SubmitDate,DocPath")] HomeWorkTable homeWorkTable)
        {
            if (ModelState.IsValid)
            {
                db.HomeWorkTable.Add(homeWorkTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.ClassTable, "ClassID", "Name", homeWorkTable.ClassID);
            ViewBag.HomeWorkTypeID = new SelectList(db.HomeWorkTypeTable, "HomeWorkTypeID", "HomeWorkTypeName", homeWorkTable.HomeWorkTypeID);
            ViewBag.GroupeID = new SelectList(db.GroupeTable, "GroupeID", "Name", homeWorkTable.GroupeID);
            return View(homeWorkTable);
        }
        public JsonResult GetGroupeList(int ClassID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<GroupeTable> Groupes = db.GroupeTable.Where(x => x.ClassId == ClassID).ToList();
            return Json(Groupes, JsonRequestBehavior.AllowGet);

        }
        // GET: HomeWorkTable/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeWorkTable homeWorkTable = db.HomeWorkTable.Find(id);
            if (homeWorkTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.ClassTable, "ClassID", "Name", homeWorkTable.ClassID);
            ViewBag.HomeWorkTypeID = new SelectList(db.HomeWorkTypeTable, "HomeWorkTypeID", "HomeWorkTypeName", homeWorkTable.HomeWorkTypeID);
            ViewBag.GroupeID = new SelectList(db.GroupeTable, "GroupeID", "Name", homeWorkTable.GroupeID);
            return View(homeWorkTable);
        }

        // POST: HomeWorkTable/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HomeWorkID,HomeWorkTypeID,ClassID,StudentID,HomeWorkTitle,HomeWorkDescription,SubmitDate,DocPath")] HomeWorkTable homeWorkTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(homeWorkTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.ClassTable, "ClassID", "Name", homeWorkTable.ClassID);
            ViewBag.HomeWorkTypeID = new SelectList(db.HomeWorkTypeTable, "HomeWorkTypeID", "HomeWorkTypeName", homeWorkTable.HomeWorkTypeID);
            ViewBag.GroupeID = new SelectList(db.GroupeTable, "GroupeID", "Name", homeWorkTable.GroupeID);
            return View(homeWorkTable);
        }

        // GET: HomeWorkTable/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeWorkTable homeWorkTable = db.HomeWorkTable.Find(id);
            if (homeWorkTable == null)
            {
                return HttpNotFound();
            }
            return View(homeWorkTable);
        }

        // POST: HomeWorkTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HomeWorkTable homeWorkTable = db.HomeWorkTable.Find(id);
            db.HomeWorkTable.Remove(homeWorkTable);
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
