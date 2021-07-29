using Microsoft.EntityFrameworkCore;
using ScholarshipManagement.Data.ApplicationContext;
using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.Repositories
{
    public class JamaatRepository: BaseRepository<Jamaat>, IJamaatRepository
    {
        public JamaatRepository(SchoolDbContext context)
        {
            DbContext = context;
        }

        public async Task<Jamaat> GetJamaatAsync(int circuitid)
        {
            return await Query().SingleOrDefaultAsync(u => u.CircuitId == circuitid);
        }

        public async Task<Jamaat> GetJamaat(int id)
        {
            return await Query().SingleOrDefaultAsync(u => u.Id == id);
        }
    }
}
