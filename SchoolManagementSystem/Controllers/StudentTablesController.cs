using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
namespace SchoolManagementSystem.Controllers
{
    public class StudentTableController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: StudentTable
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var StudentTable = db.StudentTable.Include(s => s.ClassTable).Include(s => s.GroupeTable).Include(s => s.ProgrameTable).Include(s => s.SessionTable).Include(s => s.UserTable);
            return View(StudentTable.ToList());
        }
        public JsonResult GetGroupeList(int ClassID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<GroupeTable> Groupes = db.GroupeTable.Where(x => x.ClassId == ClassID).ToList();
            return Json(Groupes, JsonRequestBehavior.AllowGet);

        }
        // GET: StudentTable/Details/5
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
            StudentTable studentTable = db.StudentTable.Find(id);
            if (studentTable == null)
            {
                return HttpNotFound();
            }
            return View(studentTable);
        }

        // GET: StudentTable/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.ClassID = new SelectList(db.ClassTable, "ClassID", "Name");
            ViewBag.GroupeId = new SelectList(db.GroupeTable, "GroupeId", "Name");
            ViewBag.ProgrameID = new SelectList(db.ProgrameTable, "ProgrameID", "Name");
            ViewBag.SessionID = new SelectList(db.SessionTable, "SessionID", "Name");
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName");
            return View();
        }

        // POST: StudentTable/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,SessionID,ProgrameID,ClassID,UserID,Name,FatherName,DateofBirth,Gender,ContactNo,CNIC,FNIC,Photo,AddmissionDate,PreviousSchool,PreviousPercentage,EmailAddress,Address,Nationality,Religion,TribeorCaste,FathersGuardiansOccupationofProfession,FathersGuardiansPostalAddress,PhoneOffice,PhoneResident,GroupeId")] StudentTable studentTable)
        {
            
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
           
            studentTable.UserID = 6;
            studentTable.Photo = "/Content/EmployeePhoto/default.png";
            if (ModelState.IsValid)
            {

                var user = new UserTable();
                user.Address = studentTable.Address;
                user.ContactNo = studentTable.ContactNo;
                user.EmailAddress = studentTable.EmailAddress;
                user.FullName = studentTable.Name;
                user.UserName = studentTable.Name;
                user.UserTypeID = 4;
                user.Password = "8888";
                db.UserTable.Add(user);
                db.StudentTable.Add(studentTable);
                studentTable.UserID = user.UserID;
                db.SaveChanges();
            
                if (studentTable.PhotoFile != null)
                {
                    var folder = "/Content/StudentPhoto";
                    var file = string.Format("{0}.png", studentTable.StudentID);
                    var response = FileHelper.UploadFile.UploadPhoto(studentTable.PhotoFile, folder, file);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        studentTable.Photo = pic;
                        db.Entry(studentTable).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.ClassTable, "ClassID", "Name", studentTable.ClassID);
            ViewBag.GroupeId = new SelectList(db.GroupeTable, "GroupeId", "Name", studentTable.GroupeId);
            ViewBag.ProgrameID = new SelectList(db.ProgrameTable, "ProgrameID", "Name", studentTable.ProgrameID);
            ViewBag.SessionID = new SelectList(db.SessionTable, "SessionID", "Name", studentTable.SessionID);
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName", studentTable.UserID);
            return View(studentTable);
        }

        // GET: StudentTable/Edit/5
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
            StudentTable studentTable = db.StudentTable.Find(id);
            if (studentTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.ClassTable, "ClassID", "Name", studentTable.ClassID);
            ViewBag.GroupeId = new SelectList(db.GroupeTable, "GroupeId", "Name", studentTable.GroupeId);
            ViewBag.ProgrameID = new SelectList(db.ProgrameTable, "ProgrameID", "Name", studentTable.ProgrameID);
            ViewBag.SessionID = new SelectList(db.SessionTable, "SessionID", "Name", studentTable.SessionID);
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName", studentTable.UserID);
            return View(studentTable);
        }

        // POST: StudentTable/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,SessionID,ProgrameID,ClassID,UserID,Name,FatherName,DateofBirth,Gender,ContactNo,CNIC,FNIC,Photo,AddmissionDate,PreviousSchool,PreviousPercentage,EmailAddress,Address,Nationality,Religion,TribeorCaste,FathersGuardiansOccupationofProfession,FathersGuardiansPostalAddress,PhoneOffice,PhoneResident,GroupeId")] StudentTable studentTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Entry(studentTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.ClassTable, "ClassID", "Name", studentTable.ClassID);
            ViewBag.GroupeId = new SelectList(db.GroupeTable, "GroupeId", "Name", studentTable.GroupeId);
            ViewBag.ProgrameID = new SelectList(db.ProgrameTable, "ProgrameID", "Name", studentTable.ProgrameID);
            ViewBag.SessionID = new SelectList(db.SessionTable, "SessionID", "Name", studentTable.SessionID);
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName", studentTable.UserID);
            return View(studentTable);
        }

        // GET: StudentTable/Delete/5
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
            StudentTable studentTable = db.StudentTable.Find(id);
            if (studentTable == null)
            {
                return HttpNotFound();
            }
            return View(studentTable);
        }

        // POST: StudentTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            StudentTable studentTable = db.StudentTable.Find(id);
            db.StudentTable.Remove(studentTable);
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
