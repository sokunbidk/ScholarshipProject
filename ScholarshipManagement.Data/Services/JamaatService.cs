﻿using Microsoft.EntityFrameworkCore;
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
            var jamaatExist = await _jamaatRepository.ExistsAsync(c => c.Name == model.Name || c.Email == model.Email);
            if (jamaatExist)
            {
                throw new BadRequestException ($" Jamaat with name '{model.Name}' or email '{model.Email}' already exist");
            }

            var jamaat = new Jamaat
            {
                Name = model.Name,
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

        public Task<JamaatResponseModel> GetJamaat(int id)
        {
            throw new NotImplementedException();
        }

        public Task<JamaatResponseModel> GetJamaatByName(string jamaatName)
        {
            throw new NotImplementedException();
        }

       
        public IList<JamaatViewModel> GetJamaats()
        {
            var jamaats = _jamaatRepository.GetJamaats().Select(r => new JamaatViewModel
            {
                Id = r.Id,
                Name = r.Name,
                Email = r.Email,
                PhoneNumber = r.PhoneNumber,
                Circuitid = r.Circuit.Id,
                //CircuitName =r.Circuit.CircuitName
            }).ToList();
            return jamaats;
        }
        

        public Task<BaseResponse> UpdateJamaatAsync(int id, UpdateJamaatRequestModel model)
        {
            throw new NotImplementedException();
        }
    }
}