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
    
    public partial class EventTable
    {
        public int EventID { get; set; }
        public string EventTitle { get; set; }
        public System.DateTime EventDate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int UserID { get; set; }
    
        public virtual UserTable UserTable { get; set; }
    }
}
