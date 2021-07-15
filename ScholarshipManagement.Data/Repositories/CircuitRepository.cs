using Microsoft.EntityFrameworkCore;
using ScholarshipManagement.Data.ApplicationContext;
using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.Repositories
{
    public class CircuitRepository : BaseRepository<Circuit>, ICircuitRepository
    {
        public CircuitRepository(SchoolDbContext context)
        {
            DbContext = context;
        }

        public async Task<IList<CircuitDto>> GetCircuitAsync(Guid circuitId)
        {
            return ( IList < CircuitDto >) await Query().SingleOrDefaultAsync(u => u.Id == circuitId);
        }
           
    }
            
}
