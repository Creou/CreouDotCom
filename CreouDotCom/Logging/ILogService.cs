using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CreouDotCom.Logging
{
    public interface ILogService
    {
        void logException(System.Net.Mail.SmtpException ex);

        void emailException(System.Net.Mail.SmtpException ex);
    }
}
