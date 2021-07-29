using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagement.Data.ApplicationContext;
using ScholarshipManagement.Data.Entities;
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


    }
}


