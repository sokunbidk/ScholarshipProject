using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data
{
    public class PaymentViewModel
    {
        public int ApplicationId { get; set; }
        public int StudentId { get; set; }
        public string UserId { get; set; }
        public double AmountApproved { get; set; }
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

    public class CreatePaymentRequestModel
    {
        public int ApplicationId { get; set; }

        [Display(Name = "StudentId")]
        public int StudentId { get; set; }

        [Display(Name = "Student Names")]
        public string StudentNames { get; set; }
        public int Status { get; set; }

        [Required(ErrorMessage = "AmountRequested")]
        [Display(Name = "AmountRequested")]
        public decimal AmountRequested { get; set; }

        [Display(Name = "AmountRecommended")]
        public decimal AmountRecommended { get; set; }

        [Display(Name = "AmountApprovedAndGranted")]
        public decimal AmountApproved { get; set; }

        [Display(Name = "DateApproved")]
        public decimal DateApproved { get; set; }

        [Display(Name = "DatePaid")]
        public DateTime DatePaid { get; set; }

        [Display(Name = "BankAccount")]
        public string BankAccount { get; set; }

        [Required, Display(Name = "BankName")]
        public string BankName { get; set; }

        [Display(Name = "ApprovedBy")]
        public string ApprovedBy { get; set; }

        [Display(Name = "Academic Level")]
        public int AcademicLevel { get; set; }
    }
    public class UpdatePaymentRequestModel
    {
        public int ApplicationId { get; set; }

        [Display(Name = "StudentId")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "AmountRequested")]
        [Display(Name = "AmountRequested")]
        public decimal AmountRequested { get; set; }

        [Display(Name = "AmountRecommended")]
        public decimal AmountRecommended { get; set; }

        [Display(Name = "AmountApproved")]
        public decimal AmountApproved { get; set; }
        public ApprovalStatus Status { get; set; }

        [Display(Name = "DateApproved")]
        public DateTime DateApproved { get; set; }

        
        [Display(Name = "DatePaid")]
        public DateTime DatePaid { get; set; }

        [Display(Name = "BankAccount")]
        public string BankAccount { get; set; }

        [Required, Display(Name = "BankName")]
        public string BankName { get; set; }

        [Display(Name = "Academic Leve")]
        public int AcademicLeve { get; set; }
   
    }
    public class PaymentsResponseModel : BaseResponse
    {
        public IEnumerable<PaymentDto> Data { get; set; } = new List<PaymentDto>();
    }

    public class PaymentResponseModel : BaseResponse
    {
        public PaymentDto Data { get; set; }
    }
}
