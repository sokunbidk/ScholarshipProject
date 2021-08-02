
using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ScholarshipManagement.Data.Services
{
    public interface ICircuitService
    {
        public Task<BaseResponse> CreateCircuitAsync(CreateCircuitRequestModel model);
        public Task<BaseResponse> UpdateCircuitAsync(int id, UpdateCircuitRequestModel model);

        //Task<CircuitsResponseModel> GetCircuits();
        IList<CircuitViewModel> GetCircuits();

        public List<Circuit> GetCircuitList();

        Task<CircuitResponseModel> GetCircuit(int id);

        //Task<IList<CircuitDto>> GetAllCircuitsAsync();

        Task<CircuitResponseModel> GetCircuitByName(string circuitName);
    }
}
