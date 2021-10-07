using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailSend;
using MailKit;
using MimeKit;

namespace EmailSend.Controllers
{

        public class SendController : Controller
        {
        [HttpPost]
        public async Task<IActionResult> SendMessage(string [] addreses)
        {
            SendService emailService = new SendService();
            string theme = Request.Form["Theme"];
            string message = Request.Form["Message"];
            string email = Request.Form["Email"];
            await emailService.SendEmailAsync(email, theme, message, addreses);
            //return RedirectToAction("SendMessage");
            return View("SendPage");
        }


    
             public IActionResult SendPage()
             {
               return View();
             }
        }
}
