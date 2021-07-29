using ScholarshipManagement.Data.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data
{
    public class PaymentViewModel
    {
        public int ApplicationFormId { get; set; }
        public double AmountRequested { get; set; }
        public double AmountRecommended { get; set; }
        public double AmountApprovedAndGranted { get; set; }
        public DateTime DateApproved { get; set; }
        public bool ConfirmPayment { get; set; }
        public DateTime DatePaid { get; set; }
        public string BankAccount { get; set; }
        public string BankName { get; set; }
        public string ApprovedBy { get; set; }
        public string ProofOfChandaPmt { get; set; }
        public int AcademicLeve { get; set; }
    }



    public class CreatePaymentRequestModel
    {
        public int ApplicationFormId { get; set; }

        [Display(Name = "StudentId")]
        public StudentViewModel StudentId { get; set; }

        [Required(ErrorMessage = "AmountRequested")]
        [Display(Name = "AmountRequested")]
        public double AmountRequested { get; set; }

        [Display(Name = "AmountRecommended")]
        public double AmountRecommended { get; set; }

        [Display(Name = "AmountApprovedAndGranted")]
        public double AmountApprovedAndGranted { get; set; }

        [Display(Name = "DateApproved")]
        public decimal DateApproved { get; set; }

        [Display(Name = "SKU Code")]
        public bool ConfirmPayment { get; set; }

        [Display(Name = "DatePaid")]
        public DateTime DatePaid { get; set; }

        [Display(Name = "BankAccount")]
        public string BankAccount { get; set; }

        [Required, Display(Name = "BankName")]
        public string BankName { get; set; }

        [Display(Name = "ApprovedBy")]
        public string ApprovedBy { get; set; }

        [Required, Display(Name = "ProofOfChandaPmt")]
        public string ProofOfChandaPmt { get; set; }

        [Display(Name = "Academic Level")]
        public int AcademicLevel { get; set; }
    }
    public class UpdatePaymentRequestModel
    {
        public int ApplicationFormId { get; set; }

        [Required(ErrorMessage = "AmountRequested")]
        [Display(Name = "AmountRequested")]
        public double AmountRequested { get; set; }

        [Display(Name = "AmountRecommended")]
        public double AmountRecommended { get; set; }

        [Display(Name = "AmountApprovedAndGranted")]
        public double AmountApprovedAndGranted { get; set; }

        [Display(Name = "DateApproved")]
        public DateTime DateApproved { get; set; }

        [Display(Name = "SKU Code")]
        public bool ConfirmPayment { get; set; }

        [Display(Name = "DatePaid")]
        public DateTime DatePaid { get; set; }

        [Display(Name = "BankAccount")]
        public string BankAccount { get; set; }

        [Required, Display(Name = "BankName")]
        public string BankName { get; set; }

        [Display(Name = "ApprovedBy")]
        public string ApprovedBy { get; set; }

        [Required, Display(Name = "ProofOfChandaPmt")]
        public string ProofOfChandaPmt { get; set; }

        [Display(Name = "Academic Leve")]
        public int AcademicLeve { get; set; }

        [Display(Name = "StudentId")]
        public StudentViewModel StudentId { get; set; }
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
