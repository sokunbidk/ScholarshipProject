
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.Services
{
    public interface IJamaatService
    {
        public Task<BaseResponse> CreateJamaatAsync(CreateJamaatRequestModel model);
        public Task<BaseResponse> UpdateJamaatAsync(int id, UpdateJamaatRequestModel model);

        //Task<CircuitsResponseModel> GetCircuits();
        IList<JamaatViewModel> GetJamaats();

        Task<JamaatResponseModel> GetJamaat(int id);

        //Task<IList<CircuitDto>> GetAllCircuitsAsync();

        Task<JamaatResponseModel> GetJamaatByName(string jamaatName);
    }
}
