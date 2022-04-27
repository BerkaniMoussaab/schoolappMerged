
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using SchoolManagementSystem;
 
using System.Net;
using System.Net.Mail;



using System.Web.Mvc;

using System.Web.UI.WebControls;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginUser(string email, string password)
        {
            try
            {


                if (email != null && password != null)
                {
                    var finduser = db.UserTable.Where(u => u.EmailAddress == email && u.Password == password).ToList();
                    if (finduser.Count() > 0)
                    {

                        Session["UserID"] = finduser[0].UserID;
                        Session["UserTypeID"] = finduser[0].UserTypeID;
                        Session["FullName"] = finduser[0].FullName;
                        Session["UserName"] = finduser[0].UserName;
                        Session["Password"] = finduser[0].Password;
                        Session["ContactNo"] = finduser[0].ContactNo;
                        Session["EmailAddress"] = finduser[0].EmailAddress;
                        Session["Address"] = finduser[0].Address;
                        var userid = finduser[0].UserID;
                        var studentphoto = db.StudentTable.Where(s => s.UserID == userid).FirstOrDefault();
                        if (studentphoto != null)
                        {
                            Session["Photo"] = studentphoto.Photo;
                        }
                        else
                        {
                            var employee = db.StaffTable.Where(e => e.UserID == userid).FirstOrDefault();
                         //   Session["Photo"] = employee.Photo;
                        }
                        //UserTypeID UserTypeName
                        //1   Admin
                        //2   Operator
                        //3   Teacher
                        //4   Student

                        string url = string.Empty;
                        if (finduser[0].UserTypeID == 2)
                        {
                            return RedirectToAction("About");
                        }
                        else if (finduser[0].UserTypeID == 3)
                        {

                            return RedirectToAction("About");

                        }

                        else if (finduser[0].UserTypeID == 4)
                        {
                            return RedirectToAction("About");
                        }
                        else if (finduser[0].UserTypeID == 1)
                        {
                            url = "About";

                        }
                        else
                        {
                            url = "About";
                        }

                        return RedirectToAction(url);
                    }
                    else
                    {
                        Session["UserID"] = string.Empty;
                        Session["UserTypeID"] = string.Empty;
                        Session["FullName"] = string.Empty;
                        Session["UserName"] = string.Empty;
                        Session["Password"] = string.Empty;
                        Session["ContactNo"] = string.Empty;
                        Session["EmailAddress"] = string.Empty;
                        Session["Address"] = string.Empty;
                        ViewBag.message = "User Name and Password is incorrect!";

                    }
                }
                else
                {
                    Session["UserID"] = string.Empty;
                    Session["UserTypeID"] = string.Empty;
                    Session["FullName"] = string.Empty;
                    Session["UserName"] = string.Empty;
                    Session["Password"] = string.Empty;
                    Session["ContactNo"] = string.Empty;
                    Session["EmailAddress"] = string.Empty;
                    Session["Address"] = string.Empty;
                    ViewBag.message = "Some unexpected issue is occure please try again!";


                }
            }
            catch (Exception ex)
            {
                Session["UserID"] = string.Empty;
                Session["UserTypeID"] = string.Empty;
                Session["FullName"] = string.Empty;
                Session["UserName"] = string.Empty;
                Session["Password"] = string.Empty;
                Session["ContactNo"] = string.Empty;
                Session["EmailAddress"] = string.Empty;
                Session["Address"] = string.Empty;
                ViewBag.message = "Some unexpected issue is occure please try again!";


            }
            return View("Login");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Welcome to DawnCambrige High School";

            return View();
        }


        public ActionResult ForgotPassword()
        {
            return View();
        }
        public ActionResult chat()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            var password = db.UserTable.Where(s => s.EmailAddress == email).FirstOrDefault();
            if (password != null)
            {

                bool status = false;
                try
                {
                    int[] prots = new int[] { 25, 465, 2525, 2526, 587 };
                    foreach (var item in prots)
                    {
                        try
                        {
                            string HostAddress = ConfigurationManager.AppSettings["Host"].ToString();
                            string FormEmailId = ConfigurationManager.AppSettings["MailFrom"].ToString();
                            string Password = ConfigurationManager.AppSettings["Password"].ToString();
                            string Port = ConfigurationManager.AppSettings["Port"].ToString();
                            MailMessage mailMessage = new MailMessage();
                            mailMessage.From = new MailAddress(FormEmailId);
                            mailMessage.Subject = "Password";
                            mailMessage.Body = "User Name : " + password.UserName + "\n  Email Address : " +
                            password.EmailAddress + "\n Password : " + password.Password;

                            mailMessage.To.Add(new MailAddress(password.EmailAddress.Trim()));
                            SmtpClient smtp = new SmtpClient();
                            smtp.Host = HostAddress;
                            //smtp.UseDefaultCredentials = false;
                            //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            NetworkCredential networkCredential = new NetworkCredential();
                            networkCredential.UserName = mailMessage.From.Address;
                            networkCredential.Password = Password;
                            smtp.Credentials = networkCredential;
                            //smtp.Port = 465; //Convert.ToInt32(Port);
                            //smtp.EnableSsl = true;
                            smtp.SendMailAsync(mailMessage).Wait();
                            status = true;
                        }
                        catch (Exception ex) 
                        {

                            
                        }
                    }
                    ViewBag.Message = "Please Check Email!";
                }
                catch (Exception e)
                {
                    ViewBag.Message = "Email Not Found!";
                }
            }
            return View();
        }

     
        


        public ActionResult ChangePassword()
        {
            return View();
        }

        public ActionResult ChangePasswordU(string oldpassword, string newpassword, string confirmpassword)
        {
            if (newpassword != confirmpassword)
            {
                ViewBag.Message = "Not Matched!";
                return View("ChangePassword");
            }
            int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            var getuser = db.UserTable.Find(userid);
            if (getuser.Password == oldpassword.Trim())
            {
                getuser.Password = newpassword.Trim();
            }
            else
            {
                ViewBag.Message = "Old Password is Incorrect!";
                return View("ChangePassword");
            }
            db.Entry(getuser).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Logout");
        }



        public ActionResult Logout()
        {
            Session["UserID"] = string.Empty;
            Session["UserTypeID"] = string.Empty;
            Session["FullName"] = string.Empty;
            Session["UserName"] = string.Empty;
            Session["Password"] = string.Empty;
            Session["ContactNo"] = string.Empty;
            Session["EmailAddress"] = string.Empty;
            Session["Address"] = string.Empty;
            return RedirectToAction("Login");
        }
       


        
        public ActionResult Index()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login");
            }
            else
            {
                return View();
            }
        }


       
    }
}