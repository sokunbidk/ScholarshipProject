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
using System.Collections.Generic;
using System.Web.WebPages.Html;
using ScholarshipManagement.Data.ApplicationContext;

namespace ScholarshipManagement.Data.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly SchoolDbContext _dbContext;


        public UserService(IUserRepository userRepository, SchoolDbContext context)
        {
            _userRepository = userRepository;
            _dbContext = context;
        }

       

        public async Task<UserEntityResponseModel> CreateUserAsync(CreateUserRequestModel model)
        {
            var userExists = await _userRepository.ExistsAsync(u => u.PhoneNumber.Equals(model.PhoneNumber) || u.Email.Equals(model.Email) || u.MemberCode.Equals(model.MemberCode)); 

            if (userExists == true)
            {
                throw new BadRequestException($"User With These Credentials Already Exists!");
            }

            if(model.Password != model.ConfirmPassword)
            {
                throw new BadRequestException($"Password does not match"); 
            }

            string salt = GenerateSalt();

            string hashedPassword = HashPassword(model.Password, salt);

            User user = new User
            {
                UserFullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                MemberCode = model.MemberCode,
                UserType = Enums.UserType.Student,
                HashSalt = salt,
                PasswordHash = hashedPassword,
                CreatedBy = model.FullName,
                JamaatId = model.JamaatId,
                CircuitId = model.CircuitId
            };
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return new UserEntityResponseModel
            {
                Data = user,
                Status = true,
                Message = "Created Successfully"          
            };
        }

        public async Task<UserResponseModel> LoginUserAsync(LoginUserRequestModel model)
        {
           
            var user = await _userRepository.GetUserAsync(model.Email);
            if (user == null)
            {
                throw new NotFoundException("Invalid Username/Password");
            }
            string hashedPassword = HashPassword(model.Password, user.HashSalt);
            if (user.PasswordHash != hashedPassword)
            {
                throw new NotFoundException("Invalid Username/Password");
            }

            //return new UserDto
            UserDto log = new UserDto
            {
                Id = user.Id,
                UserFullName =user.UserFullName,
                MemberCode = user.MemberCode,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserType = user.UserType   
            };
            return new UserResponseModel
            {
                Data = log,
                Status = true,
                Message = "Successful"
            };
        }
        public async Task<UserDto> GetUserAsync(string email)
        {
            User user = await _userRepository.GetUserAsync(email);
            UserDto Newuser = new UserDto()
            {
                Id = user.Id,
                UserFullName = user.UserFullName,
                MemberCode = user.MemberCode,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserType = user.UserType
            };return Newuser;
        }

        public async Task<BaseResponse> UpdateUserAsync(int id, UpdateUserRequestModel model)
        {
            var userExists = await _userRepository.ExistsAsync(u => u.Id != id && u.PhoneNumber == model.PhoneNumber);
            if (userExists)
            {
                throw new BadRequestException($"Phone number: '{model.PhoneNumber}' already exists.");
            }
            User user = await _userRepository.GetAsync(id);
           
            if (user == null)
            {
                throw new NotFoundException("User With this Identity Does Not exist");
            }
            
            
            user.PhoneNumber = model.PhoneNumber;
            user.UserFullName = model.UserFullName;
            user.Email = model.Email;
            user.MemberCode = model.MemberCode;
            user.UserType = model.UserType;
            
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "successfully updated"
            };
        }

        public async Task<List<UserDto>> GetUser()
        {
            List<UserDto> users = await _userRepository.Query().Select(r => new UserDto
            {
                Id = r.Id,
                UserFullName = r.UserFullName,
                Email = r.Email,
                PhoneNumber = r.PhoneNumber,
                MemberCode = r.MemberCode,
                UserType = r.UserType
            }).ToListAsync();

            return users;
           
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
                    UserFullName = user.UserFullName,
                    Email = user.Email,
                    CircuitId = user.CircuitId,
                    JamaatId = user.JamaatId,
                    PhoneNumber = user.PhoneNumber,
                    MemberCode = user.MemberCode,
                    UserType = user.UserType,
                    Id = user.Id
                },
                Status = true,
                Message = "Successful"
            }; 
        }
        public List<UserDto> GetUserType()
        {
            return _userRepository.GetUserType();
            //return userType;
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

        public Task<Circuit> GetUserCircuit(int id)
        {
            return _userRepository.GetUserCircuit(id);
        }
       
        public async void DeleteUser(int id)
        {
            //Task<User> user = _userRepository.GetAsync(id);
            var user = await _userRepository.GetAsync(id);
           /* UserDto r = new UserDto
            {
                UserFullName = user.UserFullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                MemberCode = user.MemberCode,
                UserType = user.UserType,

            };*/
                //await _userRepository.DeleteAsync(id);
                    _dbContext.Users.Remove(user);
            await _userRepository.SaveChangesAsync();
        }

    }
}
