using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.Services
{
    public interface IUserService
    {
        public Task<BaseResponse> CreateUserAsync(CreateUserRequestModel model);
        public Task<BaseResponse> UpdateUserAsync(Guid id, UpdateUserRequestModel model);

        Task<UsersResponseModel> GetUser();
        Task<UserResponseModel> GetUser(Guid id);
    }
}
