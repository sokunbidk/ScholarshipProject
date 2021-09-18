using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Enums;
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
        public Task<BaseResponse> UpdatePaymentAsync(int id, UpdatePaymentRequestModel model);

        Task<PaymentsResponseModel> GetPayments();
        Task<PaymentResponseModel> GetPayment(int id);
        //public Task<BaseResponse> CreatePaymentByApprovedApplicationAsync(List<ApprovalStatus> statuses,  int Id);
        public Task<BaseResponse> CreatePaymentByApprovedApplicationAsync(PendingApplicationsDto model, int id);
    }
}
