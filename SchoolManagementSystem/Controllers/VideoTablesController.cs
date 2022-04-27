using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;


namespace SchoolManagementSystem.Controllers
{
    public class VideoTableController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: VideoTable
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var VideoTable = db.VideoTable.Include(v => v.ClassTable).Include(v => v.SubjectTable).Include(v => v.UserTable);
            return View(VideoTable.ToList());
        }
        private const string YoutubeLinkRegex = "(?:.+?)?(?:\\/v\\/|watch\\/|\\?v=|\\&v=|youtu\\.be\\/|\\/v=|^youtu\\.be\\/)([a-zA-Z0-9_-]{11})+";

        internal static string GetVideoId(string input)
        {
           
            var regex = new Regex(YoutubeLinkRegex, RegexOptions.Compiled);
            foreach (Match match in regex.Matches(input))
            {
                //Console.WriteLine(match);
                foreach (var groupdata in match.Groups.Cast<Group>().Where(groupdata => !groupdata.ToString().StartsWith("http://") && !groupdata.ToString().StartsWith("https://") && !groupdata.ToString().StartsWith("youtu") && !groupdata.ToString().StartsWith("www.")))
                {
                    return groupdata.ToString();
                }
            }
            return string.Empty;
        }
        // GET: VideoTable/Details/5
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
            VideoTable videoTable = db.VideoTable.Find(id);
            if (videoTable == null)
            {
                return HttpNotFound();
            }
            return View(videoTable);
        }

        // GET: VideoTable/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.ClassID = new SelectList(db.ClassTable, "ClassID", "Name");
            ViewBag.SubjectId = new SelectList(db.SubjectTable, "SubjectID", "Name");
            ViewBag.UserId = new SelectList(db.UserTable, "UserID", "FullName");
            return View();
        }

        // POST: VideoTable/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VideoId,Title,link,Discription,SubjectId,ClassID,UserId")] VideoTable videoTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            videoTable.UserId = (int)Session["UserID"];

            if (ModelState.IsValid)
            {
                db.VideoTable.Add(videoTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.ClassTable, "ClassID", "Name", videoTable.ClassID);
            ViewBag.SubjectId = new SelectList(db.SubjectTable, "SubjectID", "Name", videoTable.SubjectId);
            ViewBag.UserId = new SelectList(db.UserTable, "UserID", "FullName", videoTable.UserId);
            return View(videoTable);
        }

        // GET: VideoTable/Edit/5
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
            VideoTable videoTable = db.VideoTable.Find(id);
            if (videoTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.ClassTable, "ClassID", "Name", videoTable.ClassID);
            ViewBag.SubjectId = new SelectList(db.SubjectTable, "SubjectID", "Name", videoTable.SubjectId);
            ViewBag.UserId = new SelectList(db.UserTable, "UserID", "FullName", videoTable.UserId);
            return View(videoTable);
        }

        // POST: VideoTable/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VideoId,Title,link,Discription,SubjectId,ClassID,UserId")] VideoTable videoTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Entry(videoTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.ClassTable, "ClassID", "Name", videoTable.ClassID);
            ViewBag.SubjectId = new SelectList(db.SubjectTable, "SubjectID", "Name", videoTable.SubjectId);
            ViewBag.UserId = new SelectList(db.UserTable, "UserID", "FullName", videoTable.UserId);
            return View(videoTable);
        }

        // GET: VideoTable/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoTable videoTable = db.VideoTable.Find(id);
            if (videoTable == null)
            {
                return HttpNotFound();
            }
            return View(videoTable);
        }

        // POST: VideoTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            VideoTable videoTable = db.VideoTable.Find(id);
            db.VideoTable.Remove(videoTable);
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
