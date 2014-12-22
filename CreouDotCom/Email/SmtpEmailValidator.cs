using System;
using System.Net.Mail;

namespace CreouDotCom.Email
{
    public class SmtpEmailValidator
    {
        public bool ValidateEmailAddress(String address)
        {
            try
            {
                var mailAddress = new MailAddress(address);
            }
            catch (FormatException)
            {
                return false;
            }

            return true;
        }
    }
}