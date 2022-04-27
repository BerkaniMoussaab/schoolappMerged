using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagementSystem.Models
{
    public class SendEmail
    {
        [Display(Name ="Title")]
        public string Name { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Not Correct")]
        public string Emails { get; set; }

        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
    }
}