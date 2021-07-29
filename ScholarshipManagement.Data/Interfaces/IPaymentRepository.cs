using ScholarshipManagement.Data.Entities;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.Interfaces
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<Payment> GetPayment(int id);

        Task<Payment> GetPaymentAsync(int applicationFormNumber);

        Task<Payment> GetPaymentByMemberCodeAsync(string memberCode);

    }

}
