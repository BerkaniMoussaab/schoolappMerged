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
    
    public partial class EmailsToStudents
    {
        public int EmailId { get; set; }
        public int ClassId { get; set; }
        public int GroupeId { get; set; }
        public int StudentId { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public System.DateTime date { get; set; }
    
        public virtual ClassTable ClassTable { get; set; }
        public virtual GroupeTable GroupeTable { get; set; }
        public virtual StudentTable StudentTable { get; set; }
    }
}
