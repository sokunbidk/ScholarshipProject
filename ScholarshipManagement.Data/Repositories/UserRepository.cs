using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagement.Data.ApplicationContext;
using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Enums;
using ScholarshipManagement.Data.Interfaces;

namespace ScholarshipManagement.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(SchoolDbContext context)
        {
            DbContext = context;
        }

        public async Task<User> GetUserAsync(string email)
        {
            return await Query().SingleOrDefaultAsync(u => u.Email.Equals(email));
        }

        public async Task<User> GetUser(int id)
        {
            return await Query().SingleOrDefaultAsync(u => u.Id == id);
        }
        public List<UserDto> GetUserType()
        {
            //return DbContext.Users.AsNoTracking().OrderBy(c => c.UserFullName).ToList();
            return DbContext.Users
               .Where(u => u.UserType == UserType.Circuit)
               .Select(ut => new UserDto
               {
                   Id = ut.Id,
                   UserFullName = ut.UserFullName,
                   Email = ut.Email,

               }).OrderBy(c => c.UserFullName).ToList();
        }

        public async Task<User> GetUserByMemberCodeAsync(string memberCode)
        {
            return await Query().SingleOrDefaultAsync(u => u.MemberCode.Equals(memberCode));
        }
        public Task<Circuit> GetUserCircuit(int id)
        {
            return DbContext.Circuits.SingleOrDefaultAsync(p => p.PresidentId == id);
        }
        
    }   
}


