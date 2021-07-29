using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace ScholarshipManagement.Data.Interfaces
{
    public interface ICircuitRepository 
    {
        //Task<IList<CircuitDto>> GetCircuitsAsync();
        //Task<CircuitDto> GetCircuitAsync(string circuitID);

        Task<CircuitDto> GetCircuit(int id);
        Task<Jamaat> GetCircuitAsync(int circuitID);

        //Task<IList<CircuitDto>> GetCircuitAsync(Guid circuitId);
    }
}
