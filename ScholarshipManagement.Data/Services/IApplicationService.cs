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
        public Task<BaseResponse> CreateApplicationAsync(CreateApplicationFormRequestModel model, string email);
        public Task<BaseResponse> CreateNewApplicationAsync(CreateApplicationFormRequestModel model, string currentUser);

        //public Task<BaseResponse> UpdateApplicationAsync(int id, UpdateApplicationRequestModel model);

        //public Task<BaseResponse> UpdateAsync(UpdateApplicationRequestModel model);

        //public Task<ApplicationsResponseModel> GetApplications();

        public Task<ApplicationResponseModel> GetApplication(int id);
        //public  Task<ApplicationForm> GetApplicationFormAsync(int applicationFormNumber);

        //public Task<ApplicationsResponseModel> GetApplicationAsync(ApplicationFormViewModel model);
        
        //public Task<List<PendingApplicationsDto>> PendingApplications();

        public Task<List<PendingApplicationsDto>> PendingApplicationsByStatus(List<ApprovalStatus> statuses, bool isGlobal, List<int> circuitIds, int Id);
        public Task<BaseResponse> UpdateApprovalStatus(int id, int userId);
        public Task<BaseResponse> DeclineApprovalStatus(int id);
        public Task<BaseResponse> UpdateApplicationAsync(int id, UpdateApplicationRequestModel model);
        public void DeleteApplication(int id);
        public Task<List<PendingApplicationsDto>> StudentApplicationStatus(int id);
    }
}
