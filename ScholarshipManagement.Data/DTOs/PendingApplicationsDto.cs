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
        public int ApplicationId { get; set; }
        public string Names { get; set; }
        public string Jamaat { get; set; }
        public string CircuitName { get; set; }
        public string GuardianFullName { get; set; }
        public AuxiliaryBody AuxiliaryBody { get; set; }
        public string NameOfSchool { get; set; }
        public decimal AmountRequested { get; set; }
        public decimal AmountRecommended { get; set; }
        public decimal AmountGranted { get; set; }
        public string Discipline { get; set; }
        public string Remarks { get; set; }
        public ApprovalStatus Status { get; set; }
        public string SchoolSession { get; set; }
        public string AcademicLevel { get; set; }
        public string BankAccountName { get; set; }
        public DateTime DateDisbursed { get; set; }
        public string GuardianPhoneNumber { get; set; }
        public bool Active { get; set; }

    }
}
    
