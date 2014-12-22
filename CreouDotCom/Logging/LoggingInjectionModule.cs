using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;

namespace CreouDotCom.Logging
{
    public class LoggingInjectionModule: NinjectModule
    {
        public override void Load()
        {
            this.Bind<ILogService>().To<LogService>();
        }
    }
}