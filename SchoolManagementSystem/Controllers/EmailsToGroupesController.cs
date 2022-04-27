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
    public class EmailsToGroupesController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: EmailsToGroupes
        public ActionResult Index()
        {
            var EmailsToGroupes = db.EmailsToGroupes.Include(e => e.ClassTable).Include(e => e.GroupeTable);
            return View(EmailsToGroupes.ToList());
        }

        // GET: EmailsToGroupes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailsToGroupes EmailsToGroupes = db.EmailsToGroupes.Find(id);
            if (EmailsToGroupes == null)
            {
                return HttpNotFound();
            }
            return View(EmailsToGroupes);
        }

        // GET: EmailsToGroupes/Create
        public ActionResult Create()
        {
            ViewBag.ClassId = new SelectList(db.ClassTable, "ClassID", "Name");
            ViewBag.GroupeId = new SelectList(db.GroupeTable, "GroupeId", "Name");
            return View();
        }
        public JsonResult GetGroupeList(int ClassID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<GroupeTable> Groupes = db.GroupeTable.Where(x => x.ClassId == ClassID).ToList();
            return Json(Groupes, JsonRequestBehavior.AllowGet);

        }
        // POST: EmailsToGroupes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmailId,ClassId,GroupeId,Message,Date,Subject")] EmailsToGroupes EmailsToGroupes)
        {
            List<StudentTable> GroupeStudents = db.StudentTable.Where(s => s.GroupeId == EmailsToGroupes.GroupeId).ToList();
            foreach (var item in GroupeStudents)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(item.EmailAddress);
                    mail.From = new MailAddress("mossaab04@outlook.com");
                    // string mailpassword = "09031999BerKani";
                    NetworkCredential loginInfo = new NetworkCredential("mossaab04@outlook.com", "09031999MOSSAAB"); // password for connection smtp if u dont have have then pass blank

                    mail.Subject = EmailsToGroupes.Subject;
                    string Body = EmailsToGroupes.Message;
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
                EmailsToGroupes.Date = DateTime.Now;
                db.EmailsToGroupes.Add(EmailsToGroupes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassId = new SelectList(db.ClassTable, "ClassID", "Name", EmailsToGroupes.ClassId);
            ViewBag.GroupeId = new SelectList(db.GroupeTable, "GroupeId", "Name", EmailsToGroupes.GroupeId);
            return View(EmailsToGroupes);
        
        }

        // GET: EmailsToGroupes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailsToGroupes EmailsToGroupes = db.EmailsToGroupes.Find(id);
            if (EmailsToGroupes == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassId = new SelectList(db.ClassTable, "ClassID", "Name", EmailsToGroupes.ClassId);
            ViewBag.GroupeId = new SelectList(db.GroupeTable, "GroupeId", "Name", EmailsToGroupes.GroupeId);
            return View(EmailsToGroupes);
        }

        // POST: EmailsToGroupes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmailId,ClassId,GroupeId,Message,Date,Subject")] EmailsToGroupes EmailsToGroupes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(EmailsToGroupes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassId = new SelectList(db.ClassTable, "ClassID", "Name", EmailsToGroupes.ClassId);
            ViewBag.GroupeId = new SelectList(db.GroupeTable, "GroupeId", "Name", EmailsToGroupes.GroupeId);
            return View(EmailsToGroupes);
        }

        // GET: EmailsToGroupes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailsToGroupes EmailsToGroupes = db.EmailsToGroupes.Find(id);
            if (EmailsToGroupes == null)
            {
                return HttpNotFound();
            }
            return View(EmailsToGroupes);
        }

        // POST: EmailsToGroupes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmailsToGroupes EmailsToGroupes = db.EmailsToGroupes.Find(id);
            db.EmailsToGroupes.Remove(EmailsToGroupes);
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
