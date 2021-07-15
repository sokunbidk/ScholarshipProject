using ScholarshipManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.Interfaces
{
        public interface IPaymentRepository : IRepository<Payment>
        {
            Task<Payment> GetPayment(Guid id);

            Task<Payment> GetPaymentAsync(Guid applicationFormNumber);

            Task<Payment> GetPaymentByMemberCodeAsync(string memberCode);

        }
    
}
