using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.Entities
{
    public class Payment : BaseEntity
    {
        public int ApplicationFormId { get; set; }        //ApplicationId

        public ApplicationForm ApplicationForm { get; set; }

        public int StudentId { get; set; } //Foreign Key

        public Student Student { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public double AmountRecommended { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public double AmountApprovedAndGranted { get; set; }

        public DateTime DateApproved { get; set; }

        public bool ConfirmPayment { get; set; }

        public DateTime DatePaid { get; set; }

        public string ApprovedBy { get; set; }

        public string ProofOfChandaPmt { get; set; }
        public string BankName { get; set; }
        public string BankAccountNumber { get; set; }
        public int AcademicLeve { get; set; }
        public string Remarks { get; set; }

    }
}
