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
       
        

        Task<PaymentResponseModel> GetPayment(int id);
        public Task<BaseResponse> CreatePaymentByApprovedApplicationAsync(ApplicationDto model, int id, int currentUserId);
        public  Task<ApplicationResponseModel> GetApplication(int id);
        public Task<List<PaymentDto>> GetPayments();
    }
}
