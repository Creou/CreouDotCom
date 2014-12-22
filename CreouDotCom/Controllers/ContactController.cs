using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreouDotCom.ViewModels;
using CreouDotCom.Email;
using CreouDotCom.Properties;

namespace CreouDotCom.Controllers
{
    public partial class ContactController : Controller
    {
        private IEmailService _emailService;

        public ContactController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public virtual ActionResult Contact(string name, string email, string message)
        {
            ViewBag.MessageSubmitted = false;

            var model = new ContactViewModel() { 
                Name = name,
                Email = email,
                Message = message
            };

            return View("Contact", model);
        }

        [HttpPost]
        public virtual ActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool addressValid = _emailService.ValidateEmailAddress(model.Email);
                if (!addressValid)
                {
                    ModelState.AddModelError(ContactViewModel.Field_Email, "The email address is not vaild.");
                    ViewBag.MessageSubmitted = false;
                    return View();
                }
                else
                {
                    ViewBag.MessageSubmitted = true;
                }

                String errorMessage;

                //Set the subject
                String messageSubject = "www.creou.com - Message from " + model.Name;
                String confirmationSubject = "www.creou.com - Email confirmation";

                //Set the body
                String messageBody = "Message from Creou contact form.\n" +
                             "Time    : " + DateTime.Now + "\n" +
                             "Sent by : " + model.Name + "." + "\n" +
                             "Email   : " + model.Email + "\n" + "\n" +
                             "---------------------------------------" + "\n" + "\n" +
                             model.Message;

                String confirmationBody = "Your email has been received. We will get back to you as soon as possible.\n\n\n" +
                                           "Many thanks,\n\n\n" +
                                           "Creou Software";


                bool success = _emailService.SendEmail(Settings.Default.ContactEmailAddress, Settings.Default.ConfirmationFromEmailAddress, messageSubject, messageBody, out errorMessage);

                ViewBag.Success = success;
                if (!success)
                {
                    ViewBag.ErrorMessage = errorMessage;
                }

                if (success)
                {
                    bool confirmationSuccess = _emailService.SendEmail(model.Email, Settings.Default.ConfirmationFromEmailAddress, confirmationSubject, confirmationBody, out errorMessage);

                    ViewBag.Success = confirmationSuccess;
                    if (!confirmationSuccess)
                    {
                        ViewBag.ErrorMessage = errorMessage;
                    }
                }

                return View();
            }
            else
            {
                ViewBag.MessageSubmitted = false;
                return View();
            }
        }
    }
}
