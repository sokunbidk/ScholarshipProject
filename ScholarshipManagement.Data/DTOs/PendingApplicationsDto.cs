using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Enums;
using System;
using System.Collections.Generic;

namespace ScholarshipManagement.Data.DTOs
{
    public class PendingApplicationsDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ApplicationFormId { get; set; }
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public string OtherName { get; set; }
        public string Jamaat { get; set; }
        public string CircuitName { get; set; }
        public string CircuitId { get; set; }
        public string GuardianFullName { get; set; }
        public AuxiliaryBody AuxiliaryBody { get; set; }
        public string NameOfSchool { get; set; }
        public decimal AmountRequested { get; set; }
        public string Discipline { get; set; }
        public string AcademenicLevel { get; set; }
        public string Remarks { get; set; }
        public int StatusId { get; set; }
        public ApprovalStatus Status { get; set; }
        //Other Application Detail
        public string SchoolSession { get; set; }
        public int Duration { get; set; }

        public string DegreeInView { get; set; }

        public DateTime DateAdmitted { get; set; }

        public DateTime YearToGraduate { get; set; }


        public string LetterOfAdmission { get; set; }


        public string SchoolBill { get; set; }


        public string AcademicLevel { get; set; }

        public string BankAccountNumber { get; set; }

        public string BankName { get; set; }

        public string BankAccountName { get; set; }

        public string LastSchoolResult { get; set; }

        // Other Student Details

        public string MemberCode { get; set; }

        public string Address { get; set; }

        public string Circuit { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string GuardianMemberCode { get; set; }

        public string GuardianPhoneNumber { get; set; }

        public string Photograph { get; set; }
    }   
}
    
