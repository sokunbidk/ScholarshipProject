using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Entities;
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

        public Task<BaseResponse> UpdateUserAsync(int id, UpdateUserRequestModel model);

        public Task<UserDto> LoginUserAsync(LoginUserRequestModel model);
        //Task<UsersResponseModel> GetUser();
        public Task<List<UserDto>> GetUser();
        Task<Entities.Circuit> GetUserCircuit(int id);
        Task<UserResponseModel> GetUser(int id);
        public List<UserDto> GetUserType();
        public Task<UserDto> GetUserAsync(string email);
        //public void Task<UserResponseModel> DeleteUser(int id);
        public void DeleteUser(int id);
    }
}
