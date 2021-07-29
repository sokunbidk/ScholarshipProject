using Microsoft.EntityFrameworkCore;
using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Exceptions;
using ScholarshipManagement.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<BaseResponse> CreateRoleAsync(CreateRoleRequestModel model)
        {
            var roleExists = await _roleRepository.ExistsAsync(u => u.RoleName == model.RoleName);
            if (roleExists)
            {
                throw new BadRequestException($"Role with name '{model.RoleName}' already exists.");
            }
            var role = new Role
            {
               
                RoleName = model.RoleName,
                Description = model.Description
            };
            
            await _roleRepository.AddAsync(role);
            await _roleRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "Role successfully created"
            };
        }

        public async Task<BaseResponse> UpdateRoleAsync(int id, UpdateRoleRequestModel model)
        {
            var roleExists = await _roleRepository.ExistsAsync(u => u.Id != id && u.RoleName == model.RoleName);
            if (roleExists)
            {
                throw new BadRequestException($"Role with name '{model.RoleName}' already exists.");
            }


            var role = await _roleRepository.GetAsync(id);
            if (role == null)
            {
                throw new NotFoundException("Role does not exist");
            }
            role.RoleName = model.RoleName;
            role.Description = model.Description;

            await _roleRepository.UpdateAsync(role);
            await _roleRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "Role successfully updated"
            };
        }

        public async Task<RolesResponseModel> GetRoles()
        {
            var roles = await _roleRepository.Query().Select(r => new RoleDto
            {
                Id = r.Id,
                RoleName = r.RoleName,
                Description = r.Description
            }).ToListAsync();

            return new RolesResponseModel
            {
                Data = roles,
                Status = true,
                Message = "Successful"
            };

            
        }

        public async Task<RoleResponseModel> GetRole(int id)
        {
            var role = await _roleRepository.GetAsync(id);
            if (role == null)
            {
                throw new NotFoundException("Role does not exist");
            }
            return new RoleResponseModel
            {
                Data = new RoleDto
                {
                    Id = id,
                    RoleName = role.RoleName,
                    Description = role.Description
                },
                Status = true,
                Message = "Successful"
            };


        }
    }
}
