using System.Net.Mail;
using CreouDotCom.Logging;
using System;
using CreouDotCom.Properties;
namespace CreouDotCom.Email
{
    public class EmailService : IEmailService
    {
        private ILogService _logService;

        public EmailService(ILogService logService)
        {
            _logService = logService;
        }

        public bool ValidateEmailAddress(String address) 
        {
            return new SmtpEmailValidator().ValidateEmailAddress(address);
        }

        public bool SendEmail(string to, string from, string subject, string body, out string errorMessage)
        {
            try
            {
                MailAddress fromAddress = null;
                MailAddress toAddess;
                try
                {
                    if (!String.IsNullOrEmpty(from))
                    {
                        fromAddress = new MailAddress(from);
                    }
                    toAddess = new MailAddress(to);
                }
                catch (FormatException )
                {
                    errorMessage = "Email address is not valid";
                    return false;
                }

                MailMessage message = new MailMessage();

                if (fromAddress != null)
                {
                    message.From = fromAddress;
                    message.ReplyToList.Add(fromAddress);
                }

                message.To.Add(toAddess);
                
                message.Subject = subject;
                message.Body = body;

                SmtpClient client = new SmtpClient();
                client.Send(message);

                errorMessage = String.Empty;
                return true;
            }
            catch (SmtpException ex)
            {
                _logService.logException(ex);
                _logService.emailException(ex);

                errorMessage = String.Empty;
                return false;
            }

        }
    }
}
