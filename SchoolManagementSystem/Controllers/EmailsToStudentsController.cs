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
    public class EmailsToStudentsController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: EmailsToStudents
        public ActionResult Index()
        {
            var emailsToStudents = db.EmailsToStudents.Include(e => e.ClassTable).Include(e => e.GroupeTable).Include(e => e.StudentTable);
            return View(emailsToStudents.ToList());
        }

        // GET: EmailsToStudents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailsToStudents emailsToStudent = db.EmailsToStudents.Find(id);
            if (emailsToStudent == null)
            {
                return HttpNotFound();
            }
            return View(emailsToStudent);
        }

        // GET: EmailsToStudents/Create
        public ActionResult Create()
        {
            ViewBag.ClassId = new SelectList(db.ClassTable, "ClassID", "Name");
            ViewBag.GroupeId = new SelectList(db.GroupeTable, "GroupeId", "Name");
            ViewBag.StudentId = new SelectList(db.StudentTable, "StudentID", "Name");
            return View();
        }

        public JsonResult GetGroupeList(int ClassID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<GroupeTable> Groupes = db.GroupeTable.Where(x => x.ClassId == ClassID).ToList();
            return Json(Groupes, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetStudentList(int Groupe)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<StudentTable> Students = db.StudentTable.Where(x => x.GroupeId == Groupe).ToList();
            return Json(Students, JsonRequestBehavior.AllowGet);

        }
        // POST: EmailsToStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmailId,ClassId,GroupeId,StudentId,Message,Subject")] EmailsToStudents emailsToStudent)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(db.StudentTable.Find(emailsToStudent.StudentId).EmailAddress);
                    mail.From = new MailAddress("mossaab04@outlook.com");
                    // string mailpassword = "09031999BerKani";
                    NetworkCredential loginInfo = new NetworkCredential("mossaab04@outlook.com", "09031999MOSSAAB"); // password for connection smtp if u dont have have then pass blank

                    mail.Subject = emailsToStudent.Subject;
                    string Body = emailsToStudent.Message;
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
                emailsToStudent.date = DateTime.Now;
                db.EmailsToStudents.Add(emailsToStudent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassId = new SelectList(db.ClassTable, "ClassID", "Name", emailsToStudent.ClassId);
            ViewBag.GroupeId = new SelectList(db.GroupeTable, "GroupeId", "Name", emailsToStudent.GroupeId);
            ViewBag.StudentId = new SelectList(db.StudentTable, "StudentID", "Name", emailsToStudent.StudentId);
            return View(emailsToStudent);
        }

        // GET: EmailsToStudents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailsToStudents emailsToStudent = db.EmailsToStudents.Find(id);
            if (emailsToStudent == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassId = new SelectList(db.ClassTable, "ClassID", "Name", emailsToStudent.ClassId);
            ViewBag.GroupeId = new SelectList(db.GroupeTable, "GroupeId", "Name", emailsToStudent.GroupeId);
            ViewBag.StudentId = new SelectList(db.StudentTable, "StudentID", "Name", emailsToStudent.StudentId);
            return View(emailsToStudent);
        }

        // POST: EmailsToStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmailId,ClassId,GroupeId,StudentId,Message,Subject")] EmailsToStudents emailsToStudent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emailsToStudent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassId = new SelectList(db.ClassTable, "ClassID", "Name", emailsToStudent.ClassId);
            ViewBag.GroupeId = new SelectList(db.GroupeTable, "GroupeId", "Name", emailsToStudent.GroupeId);
            ViewBag.StudentId = new SelectList(db.StudentTable, "StudentID", "Name", emailsToStudent.StudentId);
            return View(emailsToStudent);
        }

        // GET: EmailsToStudents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailsToStudents emailsToStudent = db.EmailsToStudents.Find(id);
            if (emailsToStudent == null)
            {
                return HttpNotFound();
            }
            return View(emailsToStudent);
        }

        // POST: EmailsToStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmailsToStudents emailsToStudent = db.EmailsToStudents.Find(id);
            db.EmailsToStudents.Remove(emailsToStudent);
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
