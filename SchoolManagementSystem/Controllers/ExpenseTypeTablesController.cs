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
    public class ExpenseTypeTableController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: ExpenseTypeTable
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            return View(db.ExpenseTypeTable.ToList());
        }

        // GET: ExpenseTypeTable/Details/5
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
            ExpenseTypeTable expenseTypeTable = db.ExpenseTypeTable.Find(id);
            if (expenseTypeTable == null)
            {
                return HttpNotFound();
            }
            return View(expenseTypeTable);
        }

        // GET: ExpenseTypeTable/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            return View(new ExpenseTypeTable());
        }

        // POST: ExpenseTypeTable/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExpensesTypeID,Name,IsActive")] ExpenseTypeTable expenseTypeTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.ExpenseTypeTable.Add(expenseTypeTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expenseTypeTable);
        }

        // GET: ExpenseTypeTable/Edit/5
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
            ExpenseTypeTable expenseTypeTable = db.ExpenseTypeTable.Find(id);
            if (expenseTypeTable == null)
            {
                return HttpNotFound();
            }
            return View(expenseTypeTable);
        }

        // POST: ExpenseTypeTable/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExpensesTypeID,Name,IsActive")] ExpenseTypeTable expenseTypeTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Entry(expenseTypeTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expenseTypeTable);
        }

        // GET: ExpenseTypeTable/Delete/5
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
            ExpenseTypeTable expenseTypeTable = db.ExpenseTypeTable.Find(id);
            if (expenseTypeTable == null)
            {
                return HttpNotFound();
            }
            return View(expenseTypeTable);
        }

        // POST: ExpenseTypeTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ExpenseTypeTable expenseTypeTable = db.ExpenseTypeTable.Find(id);
            db.ExpenseTypeTable.Remove(expenseTypeTable);
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
