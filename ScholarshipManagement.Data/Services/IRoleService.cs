using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.Services
{
    public interface IRoleService
    {
        public Task<BaseResponse> CreateRoleAsync(CreateRoleRequestModel model);
        public Task<BaseResponse> UpdateRoleAsync(Guid id, UpdateRoleRequestModel model);

        Task<RolesResponseModel> GetRoles();
        Task<RoleResponseModel> GetRole(Guid id);
    }
}
