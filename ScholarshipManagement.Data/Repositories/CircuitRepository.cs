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
    public class CircuitRepository : BaseRepository<Circuit>, ICircuitRepository
    {
        public CircuitRepository(SchoolDbContext context)
        {
            DbContext = context;
        }

        public IList<Circuit> GetCircuits()
        {
            return DbContext.Circuits.AsNoTracking().OrderBy(c => c.CircuitName).ToList();

        }

        public List<Circuit> GetAllCircuits()
        {
            return DbContext.Circuits.OrderBy(c => c.CircuitName).ToList();

        }
        
        public async Task<Circuit> GetCircuitAsync(int circuitId)
        {
            return await Query().SingleOrDefaultAsync(u => u.Id == circuitId);
        }

        public async Task<Circuit> GetCircuitByName(string circuitName)
        {
            return await Query().SingleOrDefaultAsync(u => u.CircuitName == circuitName);
        }


    }
}
