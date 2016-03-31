using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using AarhusWebDevCoop.Models;
using System.Net.Mail;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace AarhusWebDevCoop.Controllers
{
    public class ContactFormSurfaceController : SurfaceController
    {
        // GET: ContactFormSurface
        public ActionResult Index()
        {
            return PartialView("ContactForm", new ContactFormViewModel());
        }

        [HttpPost]
        public ActionResult HandleFormSubmit(ContactFormViewModel model)
        {
            // check if the modelstate is valid if not return to the current page
            if (!ModelState.IsValid) { return CurrentUmbracoPage(); }

            // used to send mails commented out ATM
            #region send mail
            //MailMessage message = new MailMessage();
            //message.To.Add("peter@sirgal.dk");
            //message.Subject = model.Subject;
            //message.From = new MailAddress(model.Email, model.Name);
            //message.Body = model.Message;

            //using (SmtpClient smtp = new SmtpClient())
            //{
            //    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //    smtp.UseDefaultCredentials = false;
            //    smtp.EnableSsl = false;
            //    smtp.Host = "mail.sirgal.dk";
            //    smtp.Port = 587;
            //    smtp.Credentials = new System.Net.NetworkCredential("peter@sirgal.dk", "password");
            //    smtp.EnableSsl = false;
            //    // send mail
            //    smtp.Send(message);
            //}
            #endregion

            // used to create a document in the backoffice
            IContent comment = Services.ContentService.CreateContent(model.Subject, CurrentPage.Id, "Message");

            comment.SetValue("name", model.Name);
            comment.SetValue("email", model.Email);
            comment.SetValue("subject", model.Subject);
            comment.SetValue("message", model.Message);
            comment.SetValue("hideFromSitemap", true);

            Services.ContentService.SaveAndPublishWithStatus(comment);

            TempData["success"] = true;

            return RedirectToCurrentUmbracoPage();
        }
    }
}