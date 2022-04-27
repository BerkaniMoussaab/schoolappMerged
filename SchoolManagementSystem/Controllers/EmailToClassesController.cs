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
    public class EmailToClassesController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: EmailToClass
        public ActionResult Index()
        {
            var EmailToClass = db.EmailToClass.Include(e => e.ClassTable);
            return View(EmailToClass.ToList());
        }

        // GET: EmailToClass/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailToClass emailToClass = db.EmailToClass.Find(id);
            if (emailToClass == null)
            {
                return HttpNotFound();
            }
            return View(emailToClass);
        }

        // GET: EmailToClass/Create
        public ActionResult Create()
        {
            ViewBag.ClassId = new SelectList(db.ClassTable, "ClassID", "Name");
            return View();
        }

        // POST: EmailToClass/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "emailId,ClassId,MessageId,Subject,date")] EmailToClass emailToClass)
        {
            List<StudentTable> ClassStudents = db.StudentTable.Where(s => s.ClassID == emailToClass.ClassId).ToList();
            foreach (var item in ClassStudents)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(item.EmailAddress);
                    mail.From = new MailAddress("mossaab04@outlook.com");
                    // string mailpassword = "09031999BerKani";
                    NetworkCredential loginInfo = new NetworkCredential("mossaab04@outlook.com", "09031999MOSSAAB"); // password for connection smtp if u dont have have then pass blank

                    mail.Subject = emailToClass.Subject;
                    string Body = emailToClass.MessageId;
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
                emailToClass.date = DateTime.Now;
                db.EmailToClass.Add(emailToClass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassId = new SelectList(db.ClassTable, "ClassID", "Name", emailToClass.ClassId);
            return View(emailToClass);
        }

        // GET: EmailToClass/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailToClass emailToClass = db.EmailToClass.Find(id);
            if (emailToClass == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassId = new SelectList(db.ClassTable, "ClassID", "Name", emailToClass.ClassId);
            return View(emailToClass);
        }

        // POST: EmailToClass/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "emailId,ClassId,MessageId,Subject,date")] EmailToClass emailToClass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emailToClass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassId = new SelectList(db.ClassTable, "ClassID", "Name", emailToClass.ClassId);
            return View(emailToClass);
        }

        // GET: EmailToClass/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailToClass emailToClass = db.EmailToClass.Find(id);
            if (emailToClass == null)
            {
                return HttpNotFound();
            }
            return View(emailToClass);
        }

        // POST: EmailToClass/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmailToClass emailToClass = db.EmailToClass.Find(id);
            db.EmailToClass.Remove(emailToClass);
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
