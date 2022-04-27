using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;


namespace SchoolManagementSystem.Controllers
{
    public class EmailsToAllStudentsController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: EmailsToAllStudents
        public ActionResult Index()
        {
            return View(db.EmailsToAllStudents.ToList());
        }

        // GET: EmailsToAllStudents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailsToAllStudents emailsToAllStudent = db.EmailsToAllStudents.Find(id);
            if (emailsToAllStudent == null)
            {
                return HttpNotFound();
            }
            return View(emailsToAllStudent);
        }

        // GET: EmailsToAllStudents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmailsToAllStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmailId,Message,Subject,Date")] EmailsToAllStudents emailsToAllStudent)
        {
            List<StudentTable> Students = db.StudentTable.ToList();
            foreach (var item in Students)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(item.EmailAddress);
                    mail.From = new MailAddress("mossaab04@outlook.com");
                    // string mailpassword = "09031999BerKani";
                    NetworkCredential loginInfo = new NetworkCredential("mossaab04@outlook.com", "09031999MOSSAAB"); // password for connection smtp if u dont have have then pass blank

                    mail.Subject = emailsToAllStudent.Subject;
                    string Body = emailsToAllStudent.Message;
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
                if (ModelState.IsValid)
            {
                    emailsToAllStudent.Date = DateTime.Now;
                    db.EmailsToAllStudents.Add(emailsToAllStudent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            }
            return View(emailsToAllStudent);
        
        }

        // GET: EmailsToAllStudents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailsToAllStudents emailsToAllStudent = db.EmailsToAllStudents.Find(id);
            if (emailsToAllStudent == null)
            {
                return HttpNotFound();
            }
            return View(emailsToAllStudent);
        }

        // POST: EmailsToAllStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmailId,Message,Subject,Date")] EmailsToAllStudents emailsToAllStudent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emailsToAllStudent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emailsToAllStudent);
        }

        // GET: EmailsToAllStudents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailsToAllStudents emailsToAllStudent = db.EmailsToAllStudents.Find(id);
            if (emailsToAllStudent == null)
            {
                return HttpNotFound();
            }
            return View(emailsToAllStudent);
        }

        // POST: EmailsToAllStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmailsToAllStudents emailsToAllStudent = db.EmailsToAllStudents.Find(id);
            db.EmailsToAllStudents.Remove(emailsToAllStudent);
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
