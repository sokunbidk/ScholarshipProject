using ScholarshipManagement.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScholarshipManagement.Data.Entities
{
    public class ApplicationForm : BaseEntity
    {
        //public int ApplicationFormNumber { get; set; }

        public int StudentId { get; set; } //Foreign Key

        public Student Student { get; set; }

        [Required, MaxLength(50)]
        public string SchoolSession { get; set; }

        [Required, MaxLength(50)]
        public string NameOfSchool { get; set; }

        [Required, MaxLength(50)]
        public InstitutionType InstitutionType { get; set; }

        [Required, MaxLength(50)]
        public string Discipline { get; set; }

        [Required, MaxLength(50)]
        public int Duration { get; set; }

        [Required]
        public string DegreeInView { get; set; }

        [Required, MaxLength(50)]
        public DateTime DateAdmitted { get; set; }

        public DateTime YearToGraduate { get; set; }

        [StringLength(200)]
        public string LetterOfAdmission { get; set; }

        [Required, MaxLength(50)]
        public string SchoolBill { get; set; }

        [Required, MaxLength(50)]
        public string AcademicLevel { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal AmountRequested { get; set; }

        [Required, MaxLength(10)]
        public string BankAccountNumber { get; set; }

        [Required, MaxLength(50)]
        public string BankName { get; set; }

        [Required, MaxLength(90)]
        public string BankAccountName { get; set; }

        [Required, StringLength(200)]
        public string LastSchoolResult { get; set; }
        public string Remarks { get; set; }
        //public int StatusId { get; set; }
        public ApprovalStatus Status { get; set; }
    }
}
