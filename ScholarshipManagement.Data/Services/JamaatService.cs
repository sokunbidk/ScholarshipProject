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

namespace ScholarshipManagement.Data.Services
{
    public class JamaatService : IJamaatService
    {
        private readonly IJamaatRepository _jamaatRepository;

        public JamaatService(IJamaatRepository jamaatRepository)
        {
            _jamaatRepository = jamaatRepository;
        }
        public async Task<BaseResponse> CreateJamaatAsync(CreateJamaatRequestModel model)
        {
            var jamaatExist = await _jamaatRepository.ExistsAsync(c => c.JamaatName == model.JamaatName || c.Email == model.Email);
            if (jamaatExist)
            {
                throw new BadRequestException ($" Jamaat with name '{model.JamaatName}' or email '{model.Email}' already exist");
            }

            var jamaat = new Jamaat
            {
                JamaatName = model.JamaatName,
                Email = model.Email,
                CircuitId = model.CircuitId,    //foreign Key
                PhoneNumber = model.PhoneNumber

            };
            await _jamaatRepository.AddAsync(jamaat);
            await _jamaatRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "Circuit successfully added"
            };

        }

        public List<Jamaat> GetJamaatList()
        {
            return _jamaatRepository.GetAllJamaats();
        }
        //Gets all Jamaat and List them by Records
        public IList<JamaatViewModel> GetJamaats()
        {
            var jamaats = _jamaatRepository.GetJamaats().Select(r => new JamaatViewModel
            {
                Id = r.Id,
                JamaatName = r.JamaatName,
                Email = r.Email,
                PhoneNumber = r.PhoneNumber,
                Circuitid = r.CircuitId,
                CircuitName = r.Circuit.CircuitName,
            }).ToList();
            return jamaats;
        }
        //Get
        public async Task<JamaatResponseModel> GetJamaat(int id)
        {
            var jamaat =  await _jamaatRepository.GetAsync(id);
            if (jamaat == null)
            {
                throw new NotFoundException("Jamaat does not exist");
            }
            return new JamaatResponseModel

            {
                
                Data = new JamaatDto

                {   
                    Id = jamaat.Id,
                    Circuitid = jamaat.CircuitId,
                    CircuitName = jamaat.Circuit.CircuitName,
                    JamaatName = jamaat.JamaatName,
                    Email = jamaat.Email,
                    PhoneNumber = jamaat.PhoneNumber,


                },
                Status = true,
                Message = "Successful"
            };
        }

        public Task<JamaatResponseModel> GetJamaatByName(string jamaatName)
        {
            throw new NotImplementedException();
        }

        //post
        public async Task<BaseResponse> UpdateJamaatAsync(int id, UpdateJamaatRequestModel model)
        {
            var jamaatExists = await _jamaatRepository.ExistsAsync(u => u.JamaatName != model.JamaatName || u.Email != model.Email);
            if (!jamaatExists)
            {
                throw new BadRequestException($"Jamaat With this Identity: '{model.JamaatName}' already exists.");
            }

            Jamaat jamaat = await _jamaatRepository.GetAsync(id);
            //var jamaat = new Jamaat();
            {
                jamaat.CircuitId = model.Circuitid;
                jamaat.JamaatName = model.JamaatName;
                jamaat.Email = model.Email;
                jamaat.PhoneNumber = model.PhoneNumber;  
            };
            await _jamaatRepository.UpdateAsync(jamaat);
            await _jamaatRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "Updated Successfully"

            };
        }
    }
}
