
using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Controllers
{
    public class SendEmailStaffController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();
        // GET: Email
        public ActionResult Index()
        {
            return View(new SendEmail());
        }
        [HttpPost]
        public ActionResult Index(SendEmail email)
        {
            try
            {

                var emp = db.StaffTable.ToList();
                foreach (var item in emp)
                {
                    try
                    {

                        Task.Delay(3000);
                        string HostAddress = ConfigurationManager.AppSettings["Host"].ToString();
                        string FormEmailId = ConfigurationManager.AppSettings["MailFrom"].ToString();
                        string Password = ConfigurationManager.AppSettings["Password"].ToString();
                        string Port = ConfigurationManager.AppSettings["Port"].ToString();
                        MailMessage mailMessage = new MailMessage();
                        mailMessage.From = new MailAddress(FormEmailId);
                        mailMessage.Subject = email.Name;
                        mailMessage.Body = email.Message;
                        mailMessage.To.Add(new MailAddress(item.EmailAddress));
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = HostAddress;
                        smtp.UseDefaultCredentials = false;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        NetworkCredential networkCredential = new NetworkCredential();
                        networkCredential.UserName = mailMessage.From.Address;
                        networkCredential.Password = Password;
                        smtp.Credentials = networkCredential;
                        smtp.Port = Convert.ToInt32(Port);
                        smtp.EnableSsl = true;
                        smtp.Send(mailMessage);

                    }
                    catch
                    {


                    }
                }
                return RedirectToAction("Mailsent");

            }
            catch
            {
                return RedirectToAction("Mailsent");

            }
        }
        public ActionResult Mailsent()
        {
            return View();
        }
    }
}