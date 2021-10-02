using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.Services
{
    public  interface IApplicationService
    {
        public Task<BaseResponse> CreateApplicationAsync(CreateApplicationRequestModel model, string email);
        public Task<BaseResponse> CreateNewApplicationAsync(CreateApplicationRequestModel model, string currentUser);

        //public Task<BaseResponse> UpdateApplicationAsync(int id, UpdateApplicationRequestModel model);

        //public Task<BaseResponse> UpdateAsync(UpdateApplicationRequestModel model);



        //public Task<ApplicationResponseModel> GetApplication(int id);
        //public Task<List<ApplicationDto>> GetApplication(int id);
        //public Task<ApplicationDto> GetApplication(int id);
        public Task<BaseResponse> CloseApplication(int id, int currentUserId);
        public Task<ApplicationResponseModel> GetApplication(int id);
        public Task<BaseResponse> ResetAction(int id, int currentUserId);

        public Task<List<PendingApplicationsDto>> PendingApplicationsByStatus(List<ApprovalStatus> statuses,bool isGlobal, List<int> circuitIds, int Id);
        public Task<BaseResponse> UpdateApprovalStatus(int id, int userId);
        public Task<BaseResponse> DeclineApprovalStatus(int id);
        public Task<BaseResponse> UpdateApplicationAsync(int id, UpdateApplicationRequestModel model);
        public void DeleteApplication(int id);
        public Task<List<PendingApplicationsDto>> StudentApplicationStatus(int id);
        public Task<List<PendingApplicationsDto>> StudentApplicationHistory(int id);
        public Task<List<PendingApplicationsDto>> StudentPaymentHistory(int id);
        public Task<ApplicationResponseModel> Recommendation(int id);
        public Task<BaseResponse> Recommendation(int id, UpdateApplicationRequestModel model);


    }
}
