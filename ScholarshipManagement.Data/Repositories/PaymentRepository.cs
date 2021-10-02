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

        public async Task<Payment> GetPayment(int id)
        {
            return await Query().SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Payment> GetPaymentAsync(int applicationFormId)
        {
            return await Query().SingleOrDefaultAsync(u => u.ApplicationId == applicationFormId);
        }

        public async Task<Payment> GetPaymentByMemberCodeAsync(string memberCode)
        {
            return await Query().SingleOrDefaultAsync(u => u.Application.Student.User.MemberCode == memberCode);
        }
    }
}
