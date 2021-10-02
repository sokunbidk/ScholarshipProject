using Microsoft.EntityFrameworkCore;
using ScholarshipManagement.Data.ApplicationContext;
using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.Repositories
{
    public class ApplicationRepository : BaseRepository<application>, IApplicationRepository
    {
        public ApplicationRepository(SchoolDbContext context)
        {
            DbContext = context;
        }

        public async Task<application> GetApplication(int id)
        {
            return await Query().SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<application> GetApplicationAsync(int applicationid)
        {
            return await Query().SingleOrDefaultAsync(u => u.Id == applicationid);
        }

        public async Task<application> GetApplicationByMemberCodeAsync(string memberCode)
        {
            return await Query().SingleOrDefaultAsync(u => u.Student.User.MemberCode == memberCode);
        }
    }
}
