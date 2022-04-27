using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using SchoolManagementSystem;

namespace SchoolManagementSystem.Controllers
{
    public class EmailsToAllStaffsController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: EmailsToAllStaffs
        public ActionResult Index()
        {
            return View(db.EmailsToAllStaffs.ToList());
        }

        // GET: EmailsToAllStaffss/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailsToAllStaffs EmailsToAllStaffs = db.EmailsToAllStaffs.Find(id);
            if (EmailsToAllStaffs == null)
            {
                return HttpNotFound();
            }
            return View(EmailsToAllStaffs);
        }

        // GET: EmailsToAllStaffss/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmailsToAllStaffss/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmailId,Message,Subject,Date")] EmailsToAllStaffs EmailsToAllStaffs)
        {
            List<StaffTable> Students = db.StaffTable.ToList();
            foreach (var item in Students)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(item.EmailAddress);
                    mail.From = new MailAddress("mossaab04@outlook.com");
                    // string mailpassword = "09031999BerKani";
                    NetworkCredential loginInfo = new NetworkCredential("mossaab04@outlook.com", "09031999MOSSAAB"); // password for connection smtp if u dont have have then pass blank

                    mail.Subject = EmailsToAllStaffs.Subject;
                    string Body = EmailsToAllStaffs.Message;
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
            }
            if (ModelState.IsValid)
            {
                EmailsToAllStaffs.Date = DateTime.Now;
                db.EmailsToAllStaffs.Add(EmailsToAllStaffs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(EmailsToAllStaffs);
        }

        // GET: EmailsToAllStaffss/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailsToAllStaffs EmailsToAllStaffs = db.EmailsToAllStaffs.Find(id);
            if (EmailsToAllStaffs == null)
            {
                return HttpNotFound();
            }
            return View(EmailsToAllStaffs);
        }

        // POST: EmailsToAllStaffss/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmailId,Message,Subject,Date")] EmailsToAllStaffs EmailsToAllStaffs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(EmailsToAllStaffs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(EmailsToAllStaffs);
        }

        // GET: EmailsToAllStaffss/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailsToAllStaffs EmailsToAllStaffs = db.EmailsToAllStaffs.Find(id);
            if (EmailsToAllStaffs == null)
            {
                return HttpNotFound();
            }
            return View(EmailsToAllStaffs);
        }

        // POST: EmailsToAllStaffss/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmailsToAllStaffs EmailsToAllStaffs = db.EmailsToAllStaffs.Find(id);
            db.EmailsToAllStaffs.Remove(EmailsToAllStaffs);
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
