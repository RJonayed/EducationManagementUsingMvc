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

    public partial class Admission
    {
        [Display(Name ="Admission Id")]
        public int AdmissionID { get; set; }
        [Display(Name ="Class Roll")]
        //[Required(ErrorMessage = "Roll Required")]
        public int clsRoll { get; set; }
        [Display(Name ="Teacher")]
        //[Required(ErrorMessage = "Teacher Required")]
        public int TeacherId { get; set; }
        [Display(Name ="Student")]
        //[Required(ErrorMessage = "Student Required")]
        public int StudentsId { get; set; }
        [Display(Name ="Course")]
        //[Required(ErrorMessage = "Module Required")]
        public int CourseDtlId { get; set; }
        [Display(Name ="Module")]
        public int ModuleDtlId { get; set; }
        [Display(Name ="Admission Cost")]
        //[Required(ErrorMessage = "Admission Required")]
        public decimal AdsCost { get; set; }
        [Display(Name ="Course Cost")]
        //[Required(ErrorMessage = "Course Cost Required")]
        public decimal CourseCost { get; set; }
        [Display(Name ="Admission date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime AdsDate { get; set; }
        [Display(Name = "Schedule")]
        public string Remarks { get; set; }
        [Display(Name ="Institution")]
        public int TspId { get; set; }
    
        public virtual CourseDtl CourseDtl { get; set; }
        public virtual ModuleDtl ModuleDtl { get; set; }
        public virtual StudentInformation StudentInformation { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual TspInfo TspInfo { get; set; }
    }
}