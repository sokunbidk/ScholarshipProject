using ScholarshipManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.Services
{
    public  interface IApplicationService
    {
        public Task<BaseResponse> CreateApplicationAsync(CreateApplicationFormRequestModel model);

        public Task<BaseResponse> UpdateApplicationAsync(int id, UpdateApplicationRequestModel model);

        public Task<BaseResponse> UpdateAsync(UpdateApplicationRequestModel model);

        public Task<ApplicationsResponseModel> GetApplications();

        public Task<ApplicationResponseModel> GetApplication(int id);
        public  Task<ApplicationForm> GetApplicationFormAsync(int applicationFormNumber);
    }
}
