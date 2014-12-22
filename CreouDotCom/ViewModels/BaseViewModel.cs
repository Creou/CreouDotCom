using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreouDotCom.ViewModels
{
    public enum MenuPage
    {
        Other = -1,
        Home = 0,
        Contact = 1,
        CV = 2,
        Blog = 3
    }

    public class BaseViewModel
    {
        public BaseViewModel()
        {
            Contact = new ContactViewModel();
        }
        public MenuPage MenuPage { get; set; }
        public ContactViewModel Contact { get; set; }
    }
}