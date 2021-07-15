using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data
{
    public class ApplicationFormViewModel
    {
        public Guid ApplicationFormNumber { get; set; }

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


        public double AmountRequested { get; set; }

        public int BankAccount { get; set; }

        public string BankName { get; set; }
        public string BankAccountName { get; set; }
        public string LastSchoolResult { get; set; }
    }



    public class CreateApplicationFormRequestModel
    {
        public Guid StudentId { get; set; }

        public Guid ApplicationFormNumber { get; set; }

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
        public int DateAdmitted { get; set; }

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
    public class UpdateApplictionRequestModel
    {
        public Guid StudentId { get; set; }

        public Guid ApplicationFormNumber { get; set; }

        [Display(Name = "Membership Number")]
        public string MemberCode { get; set; }

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
        [Column(TypeName = "decimal(18, 2)")]
        public double AmountRequested { get; set; }

        [Display(Name = "Name of Bank")]
        public string BankName { get; set; }

        [Required, Display(Name = "Bank Account")]
        public string BankAccountNumber { get; set; }

        [Required, Display(Name = "Bank Account Name")]
        public string BankAccountName { get; set; }

        [Required, Display(Name = "LastSchoolResult")]
        public string LastSchoolResult { get; set; }
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
