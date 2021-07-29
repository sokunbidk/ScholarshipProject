using ScholarshipManagement.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace ScholarshipManagement.Data.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse> CreateUserAsync(CreateUserRequestModel model)
        {
            var userExists = await _userRepository.ExistsAsync(u => u.PhoneNumber.Equals(model.PhoneNumber)) || await _userRepository.ExistsAsync(u => u.Email.Equals(model.Email)) || await _userRepository.ExistsAsync(u=>u.MemberCode.Equals(model.MemberCode)); 

            if (userExists)
            {
                throw new BadRequestException($"User already exist!");
            }

            if(model.Password != model.ConfirmPassword)
            {
                throw new BadRequestException($"Password does not match");
            }

            string salt = GenerateSalt();

            string hashedPassword = HashPassword(model.Password, salt);

            var user = new User
            {
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                MemberCode = model.MemberCode,
                UserType = model.UserType,
                HashSalt= salt,
                PasswordHash = hashedPassword,
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "Password successfully created"
            };
        }

        public async Task<UserDto> LoginUserAsync(LoginUserRequestModel model)
        {
            var user = await _userRepository.GetUserAsync(model.Email);

            if(user == null)
            {
                throw new NotFoundException("User does not exist");
            }

            string hashedPassword = HashPassword(model.Password, user.HashSalt);

            if (!user.PasswordHash.Equals(hashedPassword))
            {
                throw new BadRequestException($"Invalid Password");
            }

            return new UserDto
            {
                Id = user.Id,
                MemberCode = user.MemberCode,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserType = user.UserType
            };

        }

        public async Task<BaseResponse> UpdateUserAsync(int id, UpdateUserRequestModel model)
        {
            var userExists = await _userRepository.ExistsAsync(u => u.Id != id && u.PhoneNumber == model.PhoneNumber);
            if (userExists)
            {
                throw new BadRequestException($"Phone number: '{model.PhoneNumber}' already exists.");
            }
            var user = await _userRepository.GetAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User does not exist");
            }

            user.PhoneNumber = model.PhoneNumber;

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "Phone number successfully updated"
            };
        }

        public async Task<UsersResponseModel> GetUser()
        {
            var users = await _userRepository.Query().Select(r => new UserDto
            {
                Id = r.Id,
                Email = r.Email,
                PhoneNumber = r.PhoneNumber,
                MemberCode = r.MemberCode
            }).ToListAsync();

            return new UsersResponseModel
            {
                Data = users,
                Status = true,
                Message = "Successful"
            };
        }

        public async Task<UserResponseModel> GetUser(int id)
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User does not exist");
            }
            return new UserResponseModel
            {
                Data = new UserDto
                {
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    MemberCode = user.MemberCode,
                },
                Status = true,
                Message = "Successful"
            };
        }

        private string GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string saltString = Convert.ToBase64String(salt);

            return saltString;
        }

        private string HashPassword(string password, string salt)
        {
            byte[] saltByte = Convert.FromBase64String(salt);
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltByte,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}
