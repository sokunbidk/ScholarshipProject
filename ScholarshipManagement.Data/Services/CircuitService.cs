using Microsoft.EntityFrameworkCore;
using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Exceptions;
using ScholarshipManagement.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ScholarshipManagement.Data.Services
{
    public class CircuitService : ICircuitService
    {
        private readonly ICircuitRepository _circuitRepository;
        private readonly IUserRepository _userRepository;

        public CircuitService(ICircuitRepository circuitRepository, IUserRepository userRepository)
        {
            _circuitRepository = circuitRepository;
            _userRepository = userRepository;
        }
        public async Task<BaseResponse> CreateCircuitAsync(CreateCircuitRequestModel model)
        {
            var circuitExist = await _circuitRepository.ExistsAsync(c => c.CircuitName == model.CircuitName || c.Email == model.Email);
            if (circuitExist)
            {
                throw new BadRequestException ($" Circuit with name '{model.CircuitName}' or email '{model.Email}' already exist");
            }

            var circuit = new Circuit
            {
                CircuitName = model.CircuitName,
                Email = model.Email,
               


            };
            await _circuitRepository.AddAsync(circuit);
            await _circuitRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "Circuit successfully added"
            };

        }

        public async Task<CircuitResponseModel> GetCircuit(int id)
        {
            var circuit = await _circuitRepository.GetAsync(id);
            if (circuit == null)
            {
                throw new NotFoundException("Circuit does not Exist");
            }
            return new CircuitResponseModel
            {
                Data = new CircuitDto
                {
                    CircuitName = circuit.CircuitName,
                    Email = circuit.Email,
                },
                Status = true,
                Message = "Successful"
            };
        }
       

        public Task<CircuitResponseModel> GetCircuitByName(string circuitName)
        {
            throw new NotImplementedException();
        }

       //Bring out all circuit and project each of them into CircuitViewModel.Each is r
        public  IList<CircuitViewModel> GetCircuits()
        {
            var circuits = _circuitRepository.GetCircuits().Select(r => new CircuitViewModel
            {
                Id = r.Id,
                CircuitName = r.CircuitName,
                Email = r.Email,
               

            }).ToList();

            return circuits;
        }

        public List<Circuit> GetCircuitList()
        {
            return _circuitRepository.GetAllCircuits();
        }
        //Update
        public async Task<BaseResponse> UpdateCircuitAsync(int id, UpdateCircuitRequestModel model)
        {
            var circuitExists = await _circuitRepository.ExistsAsync(u => u.CircuitName != model.CircuitName || u.Email != model.Email);
            if (!circuitExists)
            {
                throw new BadRequestException($"Circuit With this Identity: '{model.CircuitName}' already exists.");
            }

            Circuit circuit = await _circuitRepository.GetAsync(id);
            //Circuit circuit = new Circuit()
            {
                circuit.CircuitName = model.CircuitName;
                circuit.Email = model.Email;
            };
            await _circuitRepository.UpdateAsync(circuit);
            await _circuitRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "Updated Successfully"

            };
        }

        public IEnumerable<SelectListItem> GetUserList()
        {
            throw new NotImplementedException();
        }

    }
}
