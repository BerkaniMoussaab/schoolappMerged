using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagementSystem.ViewModels
{
    public class EmployeeSkillsVM
    {
        public int EmployeeSkillID { get; set; }
        [Required(ErrorMessage ="Please Enter Employee Skill Name!")]
        public string SkillName { get; set; }
        public Nullable<int> EmployeeResumeID { get; set; }
        public int UserID { get; set; }
    }
}