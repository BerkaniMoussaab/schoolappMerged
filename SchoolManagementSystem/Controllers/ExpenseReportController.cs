
using SchoolManagementSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class ExpenseReportController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();
        // GET: ExpenseReport
        public ActionResult AllExpenses()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var allexpense = db.ExpensesTable.ToList().OrderByDescending(e=>e.ExpensesID);
            return View(allexpense);
        }

        public ActionResult CustomExpenses()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var allexpense = db.ExpensesTable.Where(e => e.ExpensesDate >= DateTime.Now && e.ExpensesDate <= DateTime.Now).ToList().OrderByDescending(e => e.ExpensesID);
            return View(allexpense);
        }

        [HttpPost]
        public ActionResult CustomExpenses(DateTime fromDate, DateTime toDate)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var allexpense = db.ExpensesTable.Where(e=>e.ExpensesDate >= fromDate && e.ExpensesDate <= toDate ).ToList().OrderByDescending(e => e.ExpensesID);
            return View(allexpense);
        }
    }
}