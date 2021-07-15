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
    public class RoleRepository: BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(SchoolDbContext context)
        {
            DbContext = context;
        }
    }
}
