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
        public int StudentId { get; set; }
        public string MemberCode { get; set; }
        public int UserId { get; set; }

        public string SurName { get; set; }

        public string FirstName { get; set; }

        public string OtherName { get; set; }
       
        public string Address { get; set; }
        
        public string Jamaat { get; set; }
       
        public string Circuit { get; set; }
        
        public string PhoneNumber { get; set; }
       
        public string EmailAddress { get; set; }
        
        public Gender Gender { get; set; }
     
        public DateTime DateOfBirth { get; set; }
       
        public AuxiliaryBody AuxiliaryBody { get; set; }
       
        public string GuardianFullName { get; set; }
        public string GuardianMemberCode { get; set; }

        public string GuardianPhoneNumber { get; set; }
        
        public string Photograph { get; set; }
    }


    public class CreateStudentRequestModel
    {
        public int UserId { get; set; }
  
        [Required(ErrorMessage = "Surname is required")]
        [Display(Name = "Surname")]
        public string SurName { get; set; }

        public string MemberCode { get; set; }

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

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Jamaat is required")]
        [Display(Name = "Select Jamaat")]
        public int JamaatId { get; set; }

        [Required(ErrorMessage = "Circuit is required")]
        [Display(Name = "Select Circuit")]
        public int CircuitId { get; set; }

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
        [Display(Name = "Surname")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Firstname is required")]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Display(Name = "OtherName")]
        public string OtherName { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Choose Jamaat")]
        public int JamaatId { get; set; }

        public Jamaat Jamaat { get; set; }

        [Display(Name = "Choose Circuit")]
        public int CircuitId { get; set; }
        
        [Display(Name = "AuxiliaryBody")]
        public AuxiliaryBody AuxiliaryBody { get; set; }

        [Display(Name = "PhoneNo")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "You Must State Gender")]
        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Display(Name = "DateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [Required, Display(Name = "Guardian")]
        public string GuardianFullName { get; set; }

        [Required, Display(Name = "Guardian Phone")]
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
