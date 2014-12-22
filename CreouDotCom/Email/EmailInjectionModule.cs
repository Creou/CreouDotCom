using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;

namespace CreouDotCom.Email
{
    public class EmailInjectionModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IEmailService>().To<SendGridService>();
        }
    }
}