using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.Services
{
    public interface IPaymentService
    {
        public Task<BaseResponse> CreatePaymentAsync(CreatePaymentRequestModel model);
        public Task<BaseResponse> UpdatePaymentAsync(Guid id, UpdatePaymentRequestModel model);

        Task<PaymentsResponseModel> GetPayments();
        Task<PaymentResponseModel> GetPayment(Guid id);
    }
}
