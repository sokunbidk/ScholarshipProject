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
        public Task<BaseResponse> UpdateApplicationAsync(Guid id, UpdateApplictionRequestModel model);

        public Task<ApplicationsResponseModel> GetApplications();
        public Task<ApplicationResponseModel> GetApplication(Guid id);
    }
}
