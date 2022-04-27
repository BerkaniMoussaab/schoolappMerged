



using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;


using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.Mvc;
using System.Configuration;

using System.Net;

namespace SchoolManagementSystem.Controllers
{
    public class SendEmailController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();
        // GET: Email
        public ActionResult Index()
        {
            return View(new SchoolManagementSystem.Models.SendEmail());
        }
        [HttpPost]
        public async Task<ActionResult> Index(SendEmail email)
        {
            if (ModelState.IsValid)
            {

                var std = db.StudentTable.ToList();
                var me = std.FirstOrDefault();
                if (ModelState.IsValid)
                {
                   
                    // System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                    MailMessage mail = new MailMessage();
                    mail.To.Add("khadijakha922@gmail.com");
                    mail.From = new MailAddress("mossaab04@outlook.com") ;
                    // string mailpassword = "09031999BerKani";
                    NetworkCredential loginInfo = new NetworkCredential("mossaab04@outlook.com", "09031999MOSSAAB"); // password for connection smtp if u dont have have then pass blank
                  
                    mail.Subject = email.Name;
                    string Body = email.Message;
                    mail.Body = Body;
                    mail.IsBodyHtml = true;
                    System.Net.Mail.SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp-mail.outlook.com";
                    smtp.Port = 25;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = loginInfo;
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                 
                    ViewBag.ClassID = new SelectList(db.ClassTable, "ClassID", "Name");

                    return View();
                }
                else
                {
                    return View();
                }
                //try
                //{


                //string HostAddress = "smtp.gmail.com";
                //    string FormEmailId = "bouchama.lab@gmail.com";
                //    string Password = "rimoubio";
                //     string Port = "465";
                //    MailMessage mailMessage = new MailMessage();
                //    mailMessage.From = new MailAddress(FormEmailId);
                //    mailMessage.Subject = email.Name;
                //    mailMessage.Body = email.Message;
                //    mailMessage.To.Add(new MailAddress(me.EmailAddress));
                //    SmtpClient smtp = new SmtpClient();
                //    smtp.Host = HostAddress;
                //    smtp.UseDefaultCredentials = false;
                //    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //    NetworkCredential networkCredential = new NetworkCredential();
                //    networkCredential.UserName = mailMessage.From.Address;
                //    networkCredential.Password = Password;
                //    smtp.Credentials = networkCredential;
                //    smtp.Port = Convert.ToInt32(Port);

                //    smtp.Send(mailMessage);

                //}
                //catch
                //{


                //}

                //var emp = db.StaffTable.ToList();
                //foreach (var item in emp)
                //{
                //    try
                //    {

                //        Task.Delay(3000);
                //        string HostAddress = ConfigurationManager.AppSettings["Host"].ToString();
                //        string FormEmailId = ConfigurationManager.AppSettings["MailFrom"].ToString();
                //        string Password = ConfigurationManager.AppSettings["Password"].ToString();
                //        string Port = ConfigurationManager.AppSettings["Port"].ToString();
                //        MailMessage mailMessage = new MailMessage();
                //        mailMessage.From = new MailAddress(FormEmailId);
                //        mailMessage.Subject = email.Name;
                //        mailMessage.Body = email.Message;
                //        mailMessage.To.Add(new MailAddress(item.EmailAddress));
                //        SmtpClient smtp = new SmtpClient();
                //        smtp.Host = HostAddress;
                //        smtp.UseDefaultCredentials = false;
                //        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //        NetworkCredential networkCredential = new NetworkCredential();
                //        networkCredential.UserName = mailMessage.From.Address;
                //        networkCredential.Password = Password;
                //        smtp.Credentials = networkCredential;
                //        smtp.Port = Convert.ToInt32(Port);
                //        smtp.EnableSsl = true;
                //        smtp.Send(mailMessage);

                //    }
                //    catch
                //    {


                //    }
                //}


                return RedirectToAction("Mailsent");
            }
            return View();
        }
        public ActionResult Mailsent()
        {
            return View();
        }
    }
}
