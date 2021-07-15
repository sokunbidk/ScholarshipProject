using ScholarshipManagement.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using ScholarshipManagement.Data.Enums;
using ScholarshipManagement.Data.Repositories;

namespace ScholarshipManagement.Data.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse> CreateUserAsync(CreateUserRequestModel model)
        {
            var userExists = await _userRepository.ExistsAsync(u => u.UserName == model.UserName); 
            if (userExists)
            {
                throw new BadRequestException($"User with name '{model.UserName}' already exists.");
            }
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                MemberCode = model.MemberCode,
                PasswordHash = model.Password,
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "Role successfully created"
            };
        }

        public async Task<BaseResponse> UpdateUserAsync(Guid id, UpdateUserRequestModel model)
        {
            var userExists = await _userRepository.ExistsAsync(u => u.Id != id && u.UserName == model.UserName);
            if (userExists)
            {
                throw new BadRequestException($"Role with name '{model.UserName}' already exists.");
            }
            var user = await _userRepository.GetAsync(id);
            if (user == null)
            {
                throw new NotFoundException("Role does not exist");
            }

            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "Role successfully updated"
            };
        }

        public async Task<UsersResponseModel> GetUser()
        {
            var users = await _userRepository.Query().Select(r => new UserDto
            {
                UserId = r.Id,
                UserName = r.UserName,
                Email = r.Email,
                PhoneNumber = r.PhoneNumber,
                MemberCode = r.MemberCode,
               
            }).ToListAsync();

            return new UsersResponseModel
            {
                Data = users,
                Status = true,
                Message = "Successful"
            };


        }

        public async Task<UserResponseModel> GetUser(Guid id)
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User does not exist");
            }
            return new UserResponseModel
            {
                Data  = new UserDto
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    MemberCode = user.MemberCode,
                },
                Status = true,
                Message = "Successful"
            };


        }
    }
}
