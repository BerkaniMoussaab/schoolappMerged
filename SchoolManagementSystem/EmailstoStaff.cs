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
    
    public partial class EmailstoStaff
    {
        public int EmailId { get; set; }
        public int StaffId { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual StaffTable StaffTable { get; set; }
    }
}
