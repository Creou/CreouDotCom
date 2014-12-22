using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CreouDotCom.ViewModels
{
    public class ContactViewModel
    {
        public static String Field_Name = "Name";
        public static String Field_Email = "Email";
        public static String Field_Message = "Message";

        [Required(ErrorMessage="Name field is required.")]
        [DataType(DataType.Text)]
        public String Name { get; set; }

        [Required(ErrorMessage = "Email field is required.")]
        [DataType(DataType.Text)]
        public String Email { get; set; }

        [Required(ErrorMessage = "Message field is required.")]
        [DataType(DataType.MultilineText)]
        public String Message { get; set; }

        [ScaffoldColumn(false)]
        public bool MessageSubmitted { get; set; }

        [ScaffoldColumn(false)]
        public bool Success { get; set; }

        [ScaffoldColumn(false)]
        public String ErrorMessage { get; set; }

    }
}