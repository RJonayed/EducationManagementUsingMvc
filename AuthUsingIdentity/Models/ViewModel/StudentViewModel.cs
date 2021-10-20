using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthUsingIdentity.Models.ViewModel
{
    public class StudentViewModel
    {
        [Key]
        [Display(Name = "Student Id")]
        public int StudentId { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name Required")]
        public string StName { get; set; }
        [Display(Name = "Mobile No")]
        [Required(ErrorMessage = "Mobile No Required")]
        public string Phone { get; set; }
        [Display(Name = "Date of birth")]
        public string DateOfbirth { get; set; }
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email Required")]
        public string Email { get; set; }
        [Display(Name = "Address")]
        public string Addresss { get; set; }
        [Display(Name = "Image")]
        public string ImageName { get; set; }
        public string ImageData { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public string Remarks { get; set; }
    }
}