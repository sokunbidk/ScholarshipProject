using ScholarshipManagement.Data.Enums;
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
        public int ApplicationId { get; set; } 
        public ApplicationForm ApplicationForm { get; set; }
        public int StudentId { get; set; } 
        public string StudentNames { get; set; }
        public Student Student { get; set; }
        public ApprovalStatus Status { get; set; }
        
        [Column(TypeName = "decimal(18, 2)")]
        public decimal AmountApproved { get; set; }
        public DateTime DatePaid { get; set; } = DateTime.Today;
    }
}
