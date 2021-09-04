using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace ScholarshipManagement.Data.Interfaces
{
    public interface IUserRepository: IRepository<User>
    {
        Task<User> GetUserAsync(string email);
        Task<User> GetUser(int id);
        public List<UserDto> GetUserType();
        Task<User> GetUserByMemberCodeAsync(string memberCode);
        Task<Circuit?> GetUserCircuit(int id); 

    }
}
