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
    public class AbsenceTablesController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: AbsenceTable
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var AbsenceTable = db.AbsenceTable.Include(a => a.GroupeTable).Include(a => a.UserTable).Include(a => a.ClassTable).Include(a => a.StudentTable);
            List<AbsenceTable> SortedList = AbsenceTable.OrderByDescending(o => o.AbsenceDate).ToList();
            return View(SortedList);
        }
        public ActionResult StudentAbsences()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            int userId = (int)Session["UserID"];
            var AbsenceTable = db.AbsenceTable.Include(a => a.GroupeTable).Include(a => a.UserTable).Include(a => a.ClassTable).Include(a => a.StudentTable).Where(s => s.StudentTable.UserID == userId);


            return View(AbsenceTable.ToList());
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


        // GET: AbsenceTable/Details/5
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
            AbsenceTable absenceTable = db.AbsenceTable.Find(id);
            if (absenceTable == null)
            {
                return HttpNotFound();
            }
            return View(absenceTable);
        }

        // GET: AbsenceTable/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.SessionID = new SelectList(db.SessionTable, "GroupeId", "Name");
            ViewBag.GroupeId = new SelectList(db.GroupeTable, "GroupeId", "Name");
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName");
            ViewBag.ClassID = new SelectList(db.ClassTable, "ClassID", "Name");
            ViewBag.StudentID = new SelectList(db.StudentTable, "StudentID", "Name");
            return View();
        }

        // POST: AbsenceTable/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AbsenceTable absenceTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            absenceTable.AbsenceRecordingDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                try
                {
                    var date = (DateTime)absenceTable.AbsenceDate;
                    MailMessage mail = new MailMessage();
                    var student = db.StudentTable.Find(absenceTable.StudentID);
                    mail.To.Add(student.FatherEmail);
                    mail.From = new MailAddress("mossaab04@outlook.com");
                    // string mailpassword = "09031999BerKani";
                    NetworkCredential loginInfo = new NetworkCredential("mossaab04@outlook.com", "09031999MOSSAAB"); // password for connection smtp if u dont have have then pass blank

                    mail.Subject = "Absence de votre fils";

                    string Body = "Salut c'est l'ecole de votre fils " + student.Name + " on vous informe qui'il/elle est absent" + " le " + date.ToShortDateString() + "/ " + absenceTable.AbsenceHour;
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
                db.AbsenceTable.Add(absenceTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SessionID = new SelectList(db.SessionTable, "GroupeId", "Name", absenceTable.SessionID);
            ViewBag.GroupeId = new SelectList(db.GroupeTable, "GroupeId", "Name", absenceTable.GroupeId);
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName", absenceTable.UserID);
            ViewBag.ClassID = new SelectList(db.ClassTable, "ClassID", "Name", absenceTable.ClassID);
            ViewBag.StudentID = new SelectList(db.StudentTable, "StudentID", "Name", absenceTable.StudentID);
            return View(absenceTable);
        }

        // GET: AbsenceTable/Edit/5
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
            AbsenceTable absenceTable = db.AbsenceTable.Find(id);
            if (absenceTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupeId = new SelectList(db.GroupeTable, "GroupeId", "Name", absenceTable.GroupeId);
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName", absenceTable.UserID);
            ViewBag.ClassID = new SelectList(db.ClassTable, "ClassID", "Name", absenceTable.ClassID);
            ViewBag.StudentID = new SelectList(db.StudentTable, "StudentID", "Name", absenceTable.StudentID);
            return View(absenceTable);
        }

        // POST: AbsenceTable/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AbsenceTable absenceTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Entry(absenceTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupeId = new SelectList(db.GroupeTable, "GroupeId", "Name", absenceTable.GroupeId);
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName", absenceTable.UserID);
            ViewBag.ClassID = new SelectList(db.ClassTable, "ClassID", "Name", absenceTable.ClassID);
            ViewBag.StudentID = new SelectList(db.StudentTable, "StudentID", "Name", absenceTable.StudentID);
            return View(absenceTable);
        }

        // GET: AbsenceTable/Delete/5
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
            AbsenceTable absenceTable = db.AbsenceTable.Find(id);
            if (absenceTable == null)
            {
                return HttpNotFound();
            }
            return View(absenceTable);
        }

        // POST: AbsenceTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            AbsenceTable absenceTable = db.AbsenceTable.Find(id);
            db.AbsenceTable.Remove(absenceTable);
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
