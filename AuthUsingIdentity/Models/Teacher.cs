//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AuthUsingIdentity.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Teacher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Teacher()
        {
            this.Admissions = new HashSet<Admission>();
        }
        [Display(Name = "Teacher Id")]
        public int TeacherId { get; set; }
        [Display(Name = "Name")]
        //[Required(ErrorMessage = "Teacher Name Required")]
        public string TchName { get; set; }
        public System.DateTime JoinDate { get; set; }
        [Display(Name = "Mobile No")]
        //[Required(ErrorMessage = "Mobile Required")]
        public int TchPhone { get; set; }
        [Display(Name = "Email")]
        //[Required(ErrorMessage = "Email Required")]
        [DataType(DataType.EmailAddress)]
        public string TchEmail { get; set; }
        [Display(Name ="Address")]
        public string TchAddress { get; set; }
        [Display(Name ="Salary")]
        //[Required(ErrorMessage = "Salary Required")]
        public decimal salary { get; set; }
        public string Remarks { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Admission> Admissions { get; set; }
    }
}
