using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CreouDotCom.Email
{
    public interface IEmailService
    {
        bool ValidateEmailAddress(String address);
        bool SendEmail(string to, string from, string subject, string body, out string errorMessage);
    }
}
