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
        public string MemberCode { get; set; }

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
       
        public string GuardianFullname { get; set; }
      
        public string GuardianPhoneNo { get; set; }
        
        public string Photograph { get; set; }
    }


    public class CreateStudentRequestModel
    {
        public int Id { get; set; }
        public User User { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [Display(Name = "Surname")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Firstname is required")]
        [Display(Name = "First Name")]
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

        public  Circuit Circuit { get; set; }

        [Required, Display(Name = "Auxiliary Body")]
        public AuxiliaryBody AuxiliaryBody { get; set; }

        [Required(ErrorMessage = "You must select a gender")]
        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required, Display(Name = "Guardian Name")]
        public string GuardianName { get; set; }

        [Required, Display(Name = "Guardian Phone Number")]
        public string GuardianPhone { get; set; }

        [Required, Display(Name = "Guardian MemberCode")]
        public string GuardianMemberCode { get; set; }
            
        [Required, Display(Name = "Upload Photograph")]
        public string Photograph { get; set; }
    }
    public class UpdateStudentRequestModel
    {
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
        public string GuardianFullname { get; set; }

        [Required, Display(Name = "Guardian Phone")]
        public string GuardianPhone { get; set; }

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
