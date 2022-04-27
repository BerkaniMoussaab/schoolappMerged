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
    public class ExpensesTableController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: ExpensesTable
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var ExpensesTable = db.ExpensesTable.Include(e => e.ExpenseTypeTable).Include(e => e.UserTable);
            return View(ExpensesTable.ToList());
        }

        // GET: ExpensesTable/Details/5
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
            ExpensesTable expensesTable = db.ExpensesTable.Find(id);
            if (expensesTable == null)
            {
                return HttpNotFound();
            }
            return View(expensesTable);
        }

        // GET: ExpensesTable/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.ExpensesTypeID = new SelectList(db.ExpenseTypeTable, "ExpensesTypeID", "Name");
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName");
            return View(new ExpensesTable());
        }

        // POST: ExpensesTable/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( ExpensesTable expensesTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            expensesTable.UserID = Convert.ToInt32(Session["UserID"]);
                if (ModelState.IsValid)
            {
                db.ExpensesTable.Add(expensesTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExpensesTypeID = new SelectList(db.ExpenseTypeTable, "ExpensesTypeID", "Name", expensesTable.ExpensesTypeID);
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName", expensesTable.UserID);
            return View(expensesTable);
        }

        // GET: ExpensesTable/Edit/5
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
            ExpensesTable expensesTable = db.ExpensesTable.Find(id);
            if (expensesTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExpensesTypeID = new SelectList(db.ExpenseTypeTable, "ExpensesTypeID", "Name", expensesTable.ExpensesTypeID);
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName", expensesTable.UserID);
            return View(expensesTable);
        }

        // POST: ExpensesTable/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExpensesID,ExpensesTypeID,ExpensesDate,Amount,Reason,UserID")] ExpensesTable expensesTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Entry(expensesTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExpensesTypeID = new SelectList(db.ExpenseTypeTable, "ExpensesTypeID", "Name", expensesTable.ExpensesTypeID);
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName", expensesTable.UserID);
            return View(expensesTable);
        }

        // GET: ExpensesTable/Delete/5
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
            ExpensesTable expensesTable = db.ExpensesTable.Find(id);
            if (expensesTable == null)
            {
                return HttpNotFound();
            }
            return View(expensesTable);
        }

        // POST: ExpensesTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ExpensesTable expensesTable = db.ExpensesTable.Find(id);
            db.ExpensesTable.Remove(expensesTable);
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
