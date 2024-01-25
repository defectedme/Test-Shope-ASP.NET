using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using Test_Shope_ASP.NET.Context.Services;
using Test_Shope_ASP.NET.Models;

namespace Test_Shope_ASP.NET.Controllers
{
    public class ShopChatHubController : Controller
    {


        private readonly UserManager<ApplicationUser> _userManager;



        public ShopChatHubController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;


        }

        [HttpGet]
        public IActionResult Index()
        {

            var users = _userManager.Users;
            return View(users);
        }


        //[HttpGet]
        //public IActionResult Emailrequest()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Emailrequest(EmailModel model)
        //{
        //    using (MailMessage mm = new MailMessage(model.Email, model.To))
        //    {
        //        mm.Subject = model.Subject;
        //        mm.Body = model.Body;
        //        if (model.Attachment.Length > 1)
        //        {
        //            string fileName = Path.GetFileName(model.Attachment.FileName);
        //            mm.Attachments.Add(new Attachment(model.Attachment.OpenReadStream(), fileName));
        //        }
        //        mm.IsBodyHtml = false;
        //        using (SmtpClient smtp = new SmtpClient())
        //        {
        //            smtp.Host = "smtp.poczta.onet.pl";
        //            smtp.EnableSsl = true;
        //            NetworkCredential NetworkCred = new NetworkCredential(model.Email, model.Password);
        //            smtp.UseDefaultCredentials = true;
        //            smtp.Credentials = NetworkCred;
        //            smtp.Port = 465;
        //            smtp.Send(mm);
        //            ViewBag.Message = "Email sent.";
        //        }
        //    }

        //    return View();
        //}




    }
}
