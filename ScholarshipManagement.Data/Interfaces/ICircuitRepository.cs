using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace ScholarshipManagement.Data.Interfaces
{
    public interface ICircuitRepository : IRepository<Circuit>
    {
        IList<Circuit> GetCircuits();

        public List<Circuit> GetAllCircuits();



        Task<Circuit> GetCircuitAsync(int circuitId);

        Task<Circuit> GetCircuitByName(string circuitName);

    }
}
