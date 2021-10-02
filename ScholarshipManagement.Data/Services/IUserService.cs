using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ScholarshipManagement.Data.Services
{
    public interface IUserService
    {
        public Task<UserEntityResponseModel> CreateUserAsync(CreateUserRequestModel model);
        

        public Task<BaseResponse> UpdateUserAsync(int id, UpdateUserRequestModel model);

        //public Task<UserDto> LoginUserAsync(LoginUserRequestModel model);
        public Task<UserResponseModel> LoginUserAsync(LoginUserRequestModel model);
        public Task<List<UserDto>> GetUser();
        Task<Entities.Circuit> GetUserCircuit(int id);
        Task<UserResponseModel> GetUser(int id);
        public List<UserDto> GetUserType();
        public Task<UserDto> GetUserAsync(string email);
        //public void Task<UserResponseModel> DeleteUser(int id);
        public void DeleteUser(int id);
        //public IEnumerable<SelectListItem> GetPresidentList();
        //public IEnumerable<System.Web.Mvc.SelectListItem> IUserService.GetPresidentList();
    }
}
