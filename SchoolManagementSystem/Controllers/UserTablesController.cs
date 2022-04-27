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
    public class UserTablesController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: UserTable
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var UserTable = db.UserTable.Include(u => u.UserTypeTable);
            return View(UserTable.ToList());
        }

        // GET: UserTable/Details/5
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
            UserTable userTable = db.UserTable.Find(id);
            if (userTable == null)
            {
                return HttpNotFound();
            }
            return View(userTable);
        }

        // GET: UserTable/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.UserTypeID = new SelectList(db.UserTypeTable, "UserTypeID", "TypeName");
            return View();
        }

        // POST: UserTable/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,UserTypeID,FullName,UserName,Password,ContactNo,EmailAddress,Address")] UserTable userTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.UserTable.Add(userTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserTypeID = new SelectList(db.UserTypeTable, "UserTypeID", "TypeName", userTable.UserTypeID);
            return View(userTable);
        }

        // GET: UserTable/Edit/5
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
            UserTable userTable = db.UserTable.Find(id);
            if (userTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserTypeID = new SelectList(db.UserTypeTable, "UserTypeID", "TypeName", userTable.UserTypeID);
            return View(userTable);
        }

        // POST: UserTable/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,UserTypeID,FullName,UserName,Password,ContactNo,EmailAddress,Address")] UserTable userTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Entry(userTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserTypeID = new SelectList(db.UserTypeTable, "UserTypeID", "TypeName", userTable.UserTypeID);
            return View(userTable);
        }

        // GET: UserTable/Delete/5
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
            UserTable userTable = db.UserTable.Find(id);
            if (userTable == null)
            {
                return HttpNotFound();
            }
            return View(userTable);
        }

        // POST: UserTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            UserTable userTable = db.UserTable.Find(id);
            db.UserTable.Remove(userTable);
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
