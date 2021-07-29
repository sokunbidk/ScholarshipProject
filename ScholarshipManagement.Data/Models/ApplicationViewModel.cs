using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScholarshipManagement.Data
{
    public class ApplicationFormViewModel
    {
        public int ApplicationFormNumber { get; set; }

        public string SchoolSession { get; set; }

        public string NameOfSchool { get; set; }

        public string InstitutionType { get; set; }

        public string Discipline { get; set; }

        public string Duration { get; set; }

        public string DegreeInView { get; set; }

        public string DateAdmitted { get; set; }

        public string YearToGraduate { get; set; }

        public string LetterOfAdmission { get; set; }

        public string SchoolBill { get; set; }

        public string AcademenicLevel { get; set; }

        public decimal AmountRequested { get; set; }

        public string BankAccount { get; set; }

        public string BankName { get; set; }

        public string BankAccountName { get; set; }

        public string LastSchoolResult { get; set; }
    }


    public class CreateApplicationFormRequestModel
    {
        public int StudentId { get; set; }

        public int ApplicationFormNumber { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [Display(Name = "Institution Type")]
        public InstitutionType InstitutionType { get; set; }

        [Display(Name = "NameOfSchool")]
        public string NameOfSchool { get; set; }

        [Required(ErrorMessage = "What Level Are You")]
        [Display(Name = "AcademicLevel")]
        public string AcademicLevel { get; set; }

        [Display(Name = "School Session")]
        public string SchoolSession { get; set; }

        [Display(Name = "Discipline")]
        public string Discipline { get; set; }

        [Required(ErrorMessage = "You Must State Duration")]
        [Display(Name = "Duration")]
        public int Duration { get; set; }

        [Display(Name = "DegreeInView")]
        public string DegreeInView { get; set; }

        [Required, Display(Name = "Date Admitted")]
        public DateTime DateAdmitted { get; set; }

        [Required, Display(Name = "YearToGraduate")]
        public DateTime YearToGraduate { get; set; }

        [Display(Name = "LetterOfAdmission")]
        public string LetterOfAdmission { get; set; }

        [Display(Name = "SchoolBill")]
        public string SchoolBill { get; set; }

        [Required(ErrorMessage = "You Must State Amount")]
        [Display(Name = "AmountRequested")]
        public int AmountRequested { get; set; }

        [Display(Name = "Name of Bank")]
        public string BankName { get; set; }

        [Required, Display(Name = "Bank Account Number")]
        public string BankAccountNumber { get; set; }

        public string BankAccountName { get; set; }

        [Required, Display(Name = "LastSchoolResult")]
        public string LastSchoolResult { get; set; }
    }
    public class UpdateApplicationRequestModel
    {
        public int StudentId { get; set; }
        
        public int ApplicationFormNumber { get; set; }

        [Display(Name = "MemberCode")]
        public string MemberCode { get; set; }

        [Display(Name = "SurNames")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "You Must give Price")]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Display(Name = "OtherName")]
        public string OtherName { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "You Must State Quantity")]
        [Display(Name = "Jamaat")]
        public Jamaat Jamaat { get; set; }      

        [Display(Name = "Circuit")]
        public Circuit Circuit { get; set; }     

        [Required, Display(Name = "AuxiliaryBody")]
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

        [Required, Display(Name = "Guidian")]
        public string GuardianFullname { get; set; }

        [Required, Display(Name = "GuidianPhone")]
        public string GuardianPhone { get; set; }
        public string GuardianMemberCode { get; set; }

        [Required, Display(Name = "Photograph")]
        public string Photograph { get; set; }

        [Display(Name = "NameOfSchool")]
        public string NameOfSchool { get; set; }

        [Display(Name = "AcademicLevel")]
        public string AcademicLevel { get; set; }

        [Display(Name = "School Session")]
        public string SchoolSession { get; set; }

        [Display(Name = "Discipline")]
        public string Discipline { get; set; }

        [Display(Name = "Duration")]
        public int Duration { get; set; }

        [Display(Name = "DegreeInView")]
        public string DegreeInView { get; set; }

        [Required, Display(Name = "Date Admitted")]
        public DateTime DateAdmitted { get; set; }

        [Required, Display(Name = "YearToGraduate")]
        public DateTime YearToGraduate { get; set; }

        [Display(Name = "LetterOfAdmission")]
        public string LetterOfAdmission { get; set; }

        [Display(Name = "SchoolBill")]
        public string SchoolBill { get; set; }

        [Column(TypeName = "decimal(18, 2)"), Display(Name = "Amount Requested")]
        public double AmountRequested { get; set; }

        public string LastSchoolResult { get; set; }

        public bool CircuitConsent { get; set; } = false;
 
        public bool AccountDisbursement { get; set; } = false;

        public DateTime CreatedDate { get; set; }

        public int Approvals { get; set; }
        public string Remarks { get; set; }

    }
    public class ApplicationsResponseModel : BaseResponse
    {
        public IEnumerable<ApplicationFormDto> Data { get; set; } = new List<ApplicationFormDto>();
    }
    public class ApplicationResponseModel : BaseResponse
    {
        public ApplicationFormDto Data { get; set; }
    }
}
