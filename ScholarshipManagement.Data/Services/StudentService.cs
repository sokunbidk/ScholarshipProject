﻿using System.Linq;
using System.Threading.Tasks;
using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Interfaces;
using ScholarshipManagement.Data.Exceptions;
using ScholarshipManagement.Data.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ScholarshipManagement.Data.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        private readonly IUserRepository _userRepository;
        public StudentService(IStudentRepository studentRepository, IUserRepository userRepository)
        {
            _studentRepository = studentRepository;
            _userRepository = userRepository;
        }
        public async Task<BaseResponse> CreateStudentAsync(CreateStudentRequestModel model)
        {

            var user = await _userRepository.GetUserMemeberCodeAsync(model.MemberCode);

            /*if (user == null)
            {
                throw new NotFoundException("User does not exist");
            }*/

            Student student = new()
            //CreateStudentRequestModel student = new()
            {

                    UserId = 1,
                    SurName = model.SurName,
                    FirstName = model.FirstName,
                    OtherName = model.OtherName,
                    Address = model.Address,
                    CircuitId = model.CircuitId,
                    JamaatId = model.JamaatId,
                    AuxiliaryBody = model.AuxiliaryBody,
                    //PhoneNumber = user.PhoneNumber,
                    //EmailAddress = user.Email,
                    //MemberCode = user.MemberCode,
                    Gender = model.Gender,
                    DateOfBirth = model.DateOfBirth,
                    GuardianFullName = model.GuardianFullName,
                    GuardianPhoneNumber = model.GuardianPhoneNumber,
                    GuardianMemberCode = model.GuardianMemberCode,
                    Photograph = model.Photograph
                };

                await _studentRepository.AddAsync(student);
                await _studentRepository.SaveChangesAsync();
            
            return new BaseResponse
            {
                Status = true,
                Message = "Student successfully created"
            };

        }

        public async Task<BaseResponse> UpdateStudentAsync(int id, UpdateStudentRequestModel model)
        {
            var student = await _studentRepository.GetAsync(id);

            if (student == null)
            {
                throw new NotFoundException("Student does not exist");
            }

            student.Address = model.Address;
            student.AuxiliaryBody = model.AuxiliaryBody;
            student.DateOfBirth = model.DateOfBirth;
            student.FirstName = model.FirstName;
            student.GuardianFullName = model.GuardianFullname;
            student.GuardianPhoneNumber = model.GuardianPhone;
            student.JamaatId = model.JamaatId;
            student.CircuitId = model.CircuitId;
            student.OtherName = model.OtherName;
            student.Photograph = model.Photograph;

            await _studentRepository.UpdateAsync(student);
            await _studentRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "Biodata successfully updated"
            };
        }
        public async Task<StudentsResponseModel> GetStudents(StudentsResponseModel model)
        {
            var student = await _studentRepository.Query().Select(r => new StudentDto
            {
                Address = r.Address,
                AuxiliaryBody = r.AuxiliaryBody,
                DateOfBirth = r.DateOfBirth,
                FirstName = r.FirstName,
                GuardianFullname = r.GuardianFullName,
                GuardianPhoneNumber = r.GuardianPhoneNumber,
                MemberCode = r.User.MemberCode,
                PhoneNumber = r.User.PhoneNumber,
                EmailAddress = r.User.Email,
                OtherName = r.OtherName,
                Photograph = r.Photograph
            }).ToListAsync();

            return new StudentsResponseModel
            {
                Data = student,
                Status = true,
                Message = "Successful"
            };
        }

        public async Task<StudentResponseModel> GetStudent(int id)
        {
            var student = await _studentRepository.GetAsync(id);
            if (student == null)
            {
                throw new NotFoundException("Student does not exist");
            }
            return new StudentResponseModel
            {
                Data = new StudentDto
                {
                    Address = student.Address,
                    AuxiliaryBody = student.AuxiliaryBody,
                    CircuitName = student.Circuit.CircuitName,
                    JamaatName = student.Jamaat.Name,
                    DateOfBirth = student.DateOfBirth,
                    EmailAddress = student.User.Email,
                    FirstName = student.FirstName,
                    GuardianFullname = student.GuardianFullName,
                    GuardianPhoneNumber = student.GuardianPhoneNumber,
                    MemberCode = student.User.MemberCode,
                    OtherName = student.OtherName,
                    PhoneNumber = student.User.PhoneNumber,
                    Photograph = student.Photograph
                },

                Status = true,
                Message = "Successful"
            };

        }
        //public async Task<StudentResponseModel> GetStudentByMemberCode(int MemberCode)
        //{
        //    var student = await _studentRepository.GetAsync(MemberCode);
        //    if (student == null)
        //    {
        //        throw new NotFoundException("Student does not exist");
        //    }
        //    return new StudentResponseModel
        //    {
        //        Data = new StudentDto
        //        {
        //            Address = student.Address,
        //            AuxiliaryBody = student.AuxiliaryBody,
        //            Circuit = student.Circuit,
        //            DateOfBirth = student.DateOfBirth,
        //            EmailAddress = student.EmailAddress,
        //            FirstName = student.FirstName,
        //            Guardian = student.Guardian,
        //            GuardianPhoneNumber = student.GuardianPhoneNumber,
        //            Jamaat = student.Jamaat,
        //            MemberCode = student.User.MemberCode,
        //            OtherName = student.OtherName,
        //            PhoneNumber = student.PhoneNumber,
        //            Photograph = student.Photograph
        //        },
        //        Status = true,
        //        Message = "Successful"
        //    };
        //}
    }

}
