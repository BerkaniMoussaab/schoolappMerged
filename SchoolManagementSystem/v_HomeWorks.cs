//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchoolManagementSystem
{
    using System;
    using System.Collections.Generic;
    
    public partial class v_HomeWorks
    {
        public int HomeWorkID { get; set; }
        public int HomeWorkTypeID { get; set; }
        public string HomeWorkTypeName { get; set; }
        public int ClassID { get; set; }
        public string Name { get; set; }
        public int StudentID { get; set; }
        public string Student { get; set; }
        public string HomeWorkTitle { get; set; }
        public string HomeWorkDescription { get; set; }
        public System.DateTime SubmitDate { get; set; }
        public string DocPath { get; set; }
    }
}