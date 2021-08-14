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
    public class ApplicationFormRepository : BaseRepository<ApplicationForm>, IApplicationFormRepository
    {
        public ApplicationFormRepository(SchoolDbContext context)
        {
            DbContext = context;
        }

        public async Task<ApplicationForm> GetApplicationForm(int id)
        {
            return await Query().SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<ApplicationForm> GetApplicationFormAsync(int applicationFormNumber)
        {
            return await Query().SingleOrDefaultAsync(u => u.Id == applicationFormNumber);
        }

        public async Task<ApplicationForm> GetApplicationFormByMemberCodeAsync(string memberCode)
        {
            return await Query().SingleOrDefaultAsync(u => u.Student.User.MemberCode == memberCode);
        }
    }
}
