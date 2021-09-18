using ScholarshipManagement.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.DTOs
{
    public class PaymentDto
    {
        public int ApplicationId { get; set; }
        public int StudentId { get; set; }
        public int UserId { get; set; }
        public decimal AmountApproved { get; set; }
        public DateTime DatePaid { get; set; }
        public ApprovalStatus Status { get; set; }
        public string Names { get; set; }
        public string NameOfSchool { get; set; }
        public string BankName { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankAccountName { get; set; }
        public int AcademicLevel { get; set; }
        public string SchoolSession { get; set; }

    }
}
