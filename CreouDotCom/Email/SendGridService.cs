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
            string doNotReplyAddress = WebConfigurationManager.AppSettings["DoNotReplyAddress"].ToString();
            string sendGridUserName = WebConfigurationManager.AppSettings["SendGridUserName"].ToString();
            string sendGridPassword = WebConfigurationManager.AppSettings["SendGridPassword"].ToString();
            try
            {
                if (string.IsNullOrWhiteSpace(from)) from = doNotReplyAddress;
                if (string.IsNullOrWhiteSpace(from)) 
                {
                    errorMessage = "Sender address required";
                    return false;
                };
                var message = new SendGridMessage(new MailAddress(from), new MailAddress[]{new MailAddress(to)}, subject, null, body);
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