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
    public partial class ResultMake
    {
        [Display(Name = "Result Id")]
        public int ResultMakeId { get; set; }
        [Display(Name = "Student Id")]
        //[Required(ErrorMessage = "Student Name Required")]
        public int StudentId { get; set; }
        [Display(Name = "Marks")]
        //[Required(ErrorMessage = "Marks Required")]
        public string MdlMarks { get; set; }
        [Display(Name ="Point")]
        //[Required(ErrorMessage = "Grade Required")]
        public decimal MdlPoint { get; set; }
        [Display(Name ="Grade")]
        public string MdlGrade { get; set; }
        [Display(Name ="Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime ResultDate { get; set; }
        public string Remarks { get; set; }
        [Display(Name ="Course Id")]
        //[Required(ErrorMessage = "Course Required")]
        public int CourseDtlId { get; set; }
        [Display(Name ="Module Id")]
        //[Required(ErrorMessage = "Module Required")]
        public int ModuleDtlId { get; set; }
    
        public virtual StudentInformation StudentInformation { get; set; }
        public virtual CourseDtl CourseDtl { get; set; }
        public virtual ModuleDtl ModuleDtl { get; set; }
    }
}
