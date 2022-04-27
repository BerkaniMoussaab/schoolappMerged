
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
    public class StaffTableController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: StaffTable
        public ActionResult Index()
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var StaffTable = db.StaffTable.Include(s => s.DesignationTable).Include(s => s.UserTable);
            return View(StaffTable.ToList());
        }
        public ActionResult teacherAttendance()
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var StaffTable = db.StaffTable.Include(s => s.DesignationTable).Include(s => s.UserTable);
            return View(StaffTable.ToList());
        }
        public ActionResult Teachers()
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var StaffTable = db.StaffTable.Include(s => s.DesignationTable).Include(s => s.UserTable);
            return View(StaffTable.ToList());
        }

        // GET: StaffTable/Details/5
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
            StaffTable staffTable = db.StaffTable.Find(id);
            if (staffTable == null)
            {
                return HttpNotFound();
            }
            return View(staffTable);
        }

        // GET: StaffTable/Create
        public ActionResult Create()
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.DesignationID = new SelectList(db.DesignationTable, "DesignationID", "Title");
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName");
            return View(new StaffTable());
        }

        // POST: StaffTable/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StaffTable staffTable)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            
            staffTable.Photo = "/Content/EmployeePhoto/default.png";
            staffTable.UserID = 4;
            if (ModelState.IsValid)
            {
                var user = new UserTable();
                user.Address = staffTable.Address;
                user.ContactNo = staffTable.ContactNo;
                user.EmailAddress = staffTable.EmailAddress;
                user.FullName = staffTable.Name;
                user.UserName = staffTable.Name;
                user.UserTypeID = 6;
                user.Password = staffTable.EmailAddress;
                db.UserTable.Add(user);
                staffTable.UserID = user.UserID;
                db.StaffTable.Add(staffTable);
                db.SaveChanges();

                if (staffTable.PhotoFile != null)
                {
                    var folder = "/Content/EmployeePhoto";
                    var file = string.Format("{0}.png", staffTable.StaffID);
                    var response = FileHelper.UploadFile.UploadPhoto(staffTable.PhotoFile, folder, file);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        staffTable.Photo = pic;
                        db.Entry(staffTable).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                return RedirectToAction("Index");
            }

            ViewBag.DesignationID = new SelectList(db.DesignationTable, "DesignationID", "Title", staffTable.DesignationID);
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName", staffTable.UserID);
            return View(staffTable);
        }

        // GET: StaffTable/Edit/5
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
            StaffTable staffTable = db.StaffTable.Find(id);
            if (staffTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.DesignationID = new SelectList(db.DesignationTable, "DesignationID", "Title", staffTable.DesignationID);
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName", staffTable.UserID);
            return View(staffTable);
        }

        // POST: StaffTable/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StaffTable staffTable)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            staffTable.UserID = userid;
            if (ModelState.IsValid)
            {
                
                var folder = "/Content/EmployeePhoto";
                var file = string.Format("{0}.png", staffTable.StaffID);
                var response = FileHelper.UploadFile.UploadPhoto(staffTable.PhotoFile, folder, file);
                if (response)
                {
                    var pic = string.Format("{0}/{1}", folder, file);
                    staffTable.Photo = pic;
                }

                db.Entry(staffTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DesignationID = new SelectList(db.DesignationTable, "DesignationID", "Title", staffTable.DesignationID);
            ViewBag.UserID = new SelectList(db.UserTable, "UserID", "FullName", staffTable.UserID);
            return View(staffTable);
        }

        // GET: StaffTable/Delete/5
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
            StaffTable staffTable = db.StaffTable.Find(id);
            if (staffTable == null)
            {
                return HttpNotFound();
            }
            return View(staffTable);
        }

        // POST: StaffTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            StaffTable staffTable = db.StaffTable.Find(id);
            db.StaffTable.Remove(staffTable);
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
