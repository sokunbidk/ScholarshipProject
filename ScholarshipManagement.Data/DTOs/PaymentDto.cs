using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.DTOs
{
    public class PaymentDto
    {
        public Guid Id { get; set; }

        public Guid ApplicationFormId { get; set; }

        public Guid ApplicationFormNumber { get; set; }

        public string memberCode { get; set; }

        public string Surname { get; set; }

        public string FirstName { get; set; }

        public double AmountRecommended { get; set; }

        public double AmountApprovedAndGranted { get; set; }

        public DateTime DateApproved { get; set; }

        public bool ConfirmPayment { get; set; }

        public DateTime DatePaid { get; set; }

        public string ApprovedBy { get; set; }
        public string BankName { get; set; }
        public string BankAccountNumber { get; set; }

        public string ProofOfChandaPmt { get; set; }

    }
}
