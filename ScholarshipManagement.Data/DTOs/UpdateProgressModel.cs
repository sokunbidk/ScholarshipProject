using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.DTOs
{
    public class UpdateProgressModel
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
        public Entities.Circuit Circuit { get; set; }

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
}
