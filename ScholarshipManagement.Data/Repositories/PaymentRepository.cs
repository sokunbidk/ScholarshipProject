using System;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagement.Data.ApplicationContext;
using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Interfaces;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.Repositories
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(SchoolDbContext context)
        {
            DbContext = context;
        }

        public async Task<Payment> GetPayment(Guid id)
        {
            return await Query().SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Payment> GetPaymentAsync(Guid applicationFormId)
        {
            return await Query().SingleOrDefaultAsync(u => u.ApplicationFormId == applicationFormId);
        }

        public async Task<Payment> GetPaymentByMemberCodeAsync(string memberCode)
        {
            return await Query().SingleOrDefaultAsync(u => u.ApplicationForm.Student.MemberCode == memberCode);
        }
    }
}
