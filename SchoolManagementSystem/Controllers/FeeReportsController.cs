
using SchoolManagementSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class FeeReportsController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();
        // GET: FeeReports
        public ActionResult CustomTution()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var alltutionfee = db.SubmissionFeeTable.Where(e => e.SubmissionDate >= DateTime.Now && e.SubmissionDate <= DateTime.Now).ToList().OrderByDescending(e => e.SubmissionFeeID);
            return View(alltutionfee);
        }

        [HttpPost]
        public ActionResult CustomTution(DateTime fromDate, DateTime toDate)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var alltutionfee = db.SubmissionFeeTable.Where(e => e.SubmissionDate >= DateTime.Now && e.SubmissionDate <= DateTime.Now).ToList().OrderByDescending(e => e.SubmissionFeeID);
            return View(alltutionfee);
        }


        public ActionResult CustomAnnual()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var allannulfee = db.StudentPromotTable.Where(e => e.PromoteDate >= DateTime.Now && e.PromoteDate <= DateTime.Now && e.IsSubmit == true).ToList().OrderByDescending(e => e.StudentPromotID);
            return View(allannulfee);
        }

        [HttpPost]
        public ActionResult CustomAnnual(DateTime fromDate, DateTime toDate)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var allannulfee = db.StudentPromotTable.Where(e => e.PromoteDate >= fromDate && e.PromoteDate <= toDate && e.IsSubmit == true).ToList().OrderByDescending(e => e.StudentPromotID);
            return View(allannulfee);
        }
    }
}