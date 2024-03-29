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
    
    public partial class ClassSubjectTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClassSubjectTable()
        {
            this.ExamMarksTable = new HashSet<ExamMarksTable>();
            this.ExamTime = new HashSet<ExamTime>();
            this.TimeTblTable = new HashSet<TimeTblTable>();
        }
    
        public int ClassSubjectID { get; set; }
        public int ClassID { get; set; }
        public int SubjectID { get; set; }
        public string Name { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual ClassTable ClassTable { get; set; }
        public virtual SubjectTable SubjectTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamMarksTable> ExamMarksTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamTime> ExamTime { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeTblTable> TimeTblTable { get; set; }
    }
}
