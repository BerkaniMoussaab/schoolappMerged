using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.ViewModels
{
    public class EmployeeEducationTableMV
    {
        public int EmployeeEducationID { get; set; }
        [Required(ErrorMessage = "{0} Field is Requried!")]
        public string InstituteUniversity { get; set; }
        [Required(ErrorMessage = "{0} Field is Requried!")]
        public string TitleOfDiploma { get; set; }
        [Required(ErrorMessage = "{0} Field is Requried!")]
        public string Degree { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "{0} Field is Requried!")]
        public Nullable<System.DateTime> FromYear { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "{0} Field is Requried!")]
        public Nullable<System.DateTime> ToYear { get; set; }
        [Required(ErrorMessage = "{0} Field is Requried!")]
        public string City { get; set; }
        [Required(ErrorMessage = "{0} Field is Requried!")]
        public string Country { get; set; }
        public Nullable<int> EmployeeResumeID { get; set; }
        public int UserID { get; set; }

        public List<SelectListItem> ListOfCountry { get; set; }
        public List<SelectListItem> ListOfCity { get; set; }


    }
}