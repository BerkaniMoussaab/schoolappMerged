using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolManagementSystem.Controllers
{
    public class EmailstoStaffsController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: EmailstoStaff
        public ActionResult Index()
        {
            var EmailstoStaff = db.EmailstoStaff.Include(e => e.StaffTable);
            return View(EmailstoStaff.ToList());
        }

        // GET: EmailstoStaff/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailstoStaff emailstoStaff = db.EmailstoStaff.Find(id);
            if (emailstoStaff == null)
            {
                return HttpNotFound();
            }
            return View(emailstoStaff);
        }

        // GET: EmailstoStaff/Create
        public ActionResult Create()
        {
            ViewBag.StaffId = new SelectList(db.StaffTable, "StaffID", "Name");
            return View();
        }

        // POST: EmailstoStaff/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmailId,StaffId,Message,Subject,Date")] EmailstoStaff emailstoStaff)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(db.StaffTable.Find(emailstoStaff.StaffId).EmailAddress);
                    mail.From = new MailAddress("mossaab04@outlook.com");
                    // string mailpassword = "09031999BerKani";
                    NetworkCredential loginInfo = new NetworkCredential("mossaab04@outlook.com", "09031999MOSSAAB"); // password for connection smtp if u dont have have then pass blank

                    mail.Subject = emailstoStaff.Subject;
                    string Body = emailstoStaff.Message;
                    mail.Body = Body;
                    
                    mail.IsBodyHtml = true;
                    System.Net.Mail.SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp-mail.outlook.com";
                    smtp.Port = 25;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = loginInfo;
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                   
                }
                catch
                {

                }
                emailstoStaff.Date = DateTime.Now;
                db.EmailstoStaff.Add(emailstoStaff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StaffId = new SelectList(db.StaffTable, "StaffID", "Name", emailstoStaff.StaffId);
            return View(emailstoStaff);
        }

        // GET: EmailstoStaff/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailstoStaff emailstoStaff = db.EmailstoStaff.Find(id);
            if (emailstoStaff == null)
            {
                return HttpNotFound();
            }
            ViewBag.StaffId = new SelectList(db.StaffTable, "StaffID", "Name", emailstoStaff.StaffId);
            return View(emailstoStaff);
        }

        // POST: EmailstoStaff/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmailId,StaffId,Message,Subject,Date")] EmailstoStaff emailstoStaff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emailstoStaff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StaffId = new SelectList(db.StaffTable, "StaffID", "Name", emailstoStaff.StaffId);
            return View(emailstoStaff);
        }

        // GET: EmailstoStaff/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailstoStaff emailstoStaff = db.EmailstoStaff.Find(id);
            if (emailstoStaff == null)
            {
                return HttpNotFound();
            }
            return View(emailstoStaff);
        }

        // POST: EmailstoStaff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmailstoStaff emailstoStaff = db.EmailstoStaff.Find(id);
            db.EmailstoStaff.Remove(emailstoStaff);
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
