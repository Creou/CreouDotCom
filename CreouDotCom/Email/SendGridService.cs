using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;


namespace CreouDotCom.Email
{
    public class SendGridService : IEmailService
    {
        public bool ValidateEmailAddress(string address)
        {
            return new SmtpEmailValidator().ValidateEmailAddress(address);
        }

        public bool SendEmail(string to, string from, string subject, string body, out string errorMessage)
        {
            string sendGridUserName = (string)WebConfigurationManager.AppSettings["SendGridUserName"];
            string sendGridPassword = (string)WebConfigurationManager.AppSettings["SendGridPassword"];
            
            if (string.IsNullOrEmpty(sendGridUserName) || string.IsNullOrEmpty(sendGridPassword))
            {
#if DEBUG
                errorMessage = "Email provider details missing.";
#else
                errorMessage = string.Empty;
#endif

                return false;
            }
            else
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(from))
                    {
                        errorMessage = "Sender address required";
                        return false;
                    };
                    var message = new SendGridMessage(new MailAddress(from), new MailAddress[] { new MailAddress(to) }, subject, null, body);
                    var credentials = new NetworkCredential(sendGridUserName, sendGridPassword);
                    var transport = new Web(credentials);
                    transport.Deliver(message);
                    errorMessage = string.Empty;
                    return true;
                }
                catch (Exception e)
                {
                    errorMessage = e.Message;
                    return false;
                }
            }
        }
    }
}