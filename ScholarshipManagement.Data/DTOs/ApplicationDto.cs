using ScholarshipManagement.Data.Enums;
using System;

namespace ScholarshipManagement.Data.DTOs
{
    public class ApplicationDto
    {
        public int ApplicationId { get; set; }
        public int StudentId { get; set; }
        public string Names { get; set; }

        public string MemberCode { get; set; }

        public DateTime Created { get; set; }

        public string SchoolSession { get; set; }

        public string NameOfSchool { get; set; }

        public InstitutionType InstitutionType { get; set; }

        public string Discipline { get; set; }

        public int Duration { get; set; }

        public string DegreeInView { get; set; }

        public DateTime DateAdmitted { get; set; }

        public DateTime YearToGraduate { get; set; }

        public string LetterOfAdmission { get; set; }

        public string SchoolBill { get; set; }

        public string AcademicLevel { get; set; }

        public decimal AmountRequested { get; set; }

        public decimal AmountRecommended { get; set; }
        public ApprovalStatus Status { get; set; }

        public string BankAccountNumber { get; set; }

        public string BankName { get; set; }

        public string BankAccountName { get; set; }

        public string LastSchoolResult { get; set; }
        public string Remarks { get; set; }
        public DateTime DateApproved { get; set; }   //Date Approved by Amir
    }
}
