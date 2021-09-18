using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScholarshipManagement.Data
{
    public class StudentViewModel
    {
        [Display(Name = "Sur Name")]
        public int StudentId { get; set; }
        [Display(Name = "Member Code")]
        public string MemberCode { get; set; }
        public int UserId { get; set; }
        [Display(Name = "Sur Name")]
        public string SurName { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Other Name")]
        public string OtherName { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        public string Jamaat { get; set; }
        [Display(Name = "Circuit")]
        public string Circuit { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }
        [Display(Name = "Gender")]
        public Gender Gender { get; set; }
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Auxiliary")]
        public AuxiliaryBody AuxiliaryBody { get; set; }
        [Display(Name = "Guardian Name")]
        public string GuardianFullName { get; set; }
        [Display(Name = "Guardian MemberCode")]
        public string GuardianMemberCode { get; set; }
        [Display(Name = "Guardian Phone No")]
        public string GuardianPhoneNumber { get; set; }
        
        public string Photograph { get; set; }
    }


    public class CreateStudentRequestModel
    {
        public int UserId { get; set; }
  
        [Required(ErrorMessage = "Surname is required")]
        [Display(Name = "Sur name")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Firstname is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Other Name")]
        public string OtherName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required, Display(Name = "Select Auxiliary Body")]
        public AuxiliaryBody AuxiliaryBody { get; set; }

        [Required(ErrorMessage = "Select gender")]
        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "DOB is required")]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Guardian Name is required")]
        [Display(Name = "Guardian Name")]
        public string GuardianFullName { get; set; }

        [Required(ErrorMessage = "Guardian Phone No is required")]
        [MaxLength(11)]
        [Display(Name = "Guardian Phone Number")]
        public string GuardianPhoneNumber { get; set; }

        [Required(ErrorMessage = "Guardian MemberCode is required")]
        [Display(Name = "Guardian MemberCode")]
        public string GuardianMemberCode { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Upload Photograph")]
        [Required(ErrorMessage = "Please Select Pix to upload.")]
        public string Photograph { get; set; }
    }
    public class UpdateStudentRequestModel
    {
        public string MemberCode { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [Display(Name = "Sur name")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Firstname is required")]
        [Display(Name = "Firs tName")]
        public string FirstName { get; set; }

        [Display(Name = "Other Name")]
        public string OtherName { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Choose Jamaat")]
        public int JamaatId { get; set; }

        public Jamaat Jamaat { get; set; }

        [Display(Name = "Choose Circuit")]
        public int CircuitId { get; set; }
        
        [Display(Name = "Auxiliary Body")]
        public AuxiliaryBody AuxiliaryBody { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "You Must State Gender")]
        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required, Display(Name = "Guardian")]
        public string GuardianFullName { get; set; }

        [Required, Display(Name = "Guardian Phone No")]
        public string GuardianPhoneNumber { get; set; }

        [Required, Display(Name = "Guardian MemberCode")]
        public string GuardianMemberCode { get; set; }
        
        [Required, Display(Name = "Photograph")]
        public string Photograph { get; set; }
    }
    public class StudentsResponseModel : BaseResponse
    {
        public IEnumerable<StudentDto> Data { get; set; } = new List<StudentDto>();
    }

    public class StudentResponseModel : BaseResponse
    {
        public StudentDto Data { get; set; }
    }
}
