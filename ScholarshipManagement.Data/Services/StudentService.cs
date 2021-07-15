using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Interfaces;
using ScholarshipManagement.Data.Exceptions;
using ScholarshipManagement.Data.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ScholarshipManagement.Data.Services
{
    public class StudentService :IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<BaseResponse> CreateStudentAsync(CreateStudentRequestModel model)
        {

            var studentidExists = await _studentRepository.ExistsAsync(u => u.PhoneNumber == model.PhoneNumber);
            if (studentidExists)
            {
                throw new BadRequestException($"Applicant with name '{model.PhoneNumber}' already exists.");

            }

            var studentExists = await _studentRepository.ExistsAsync(u => u.MemberCode == model.MemberCode);
            if (studentExists)
            {
                throw new BadRequestException($"Applicant with name '{model.MemberCode}' already exists.");

            }
            var student = new Student
            {
                Address = model.Address,
                AuxiliaryBody = model.AuxiliaryBody,
                CircuitId = model.Circuit.Id,
                DateOfBirth = model.DateOfBirth,
                EmailAddress = model.EmailAddress,
                FirstName = model.FirstName,
                Guardian = model.Guardian,
                GuardianPhoneNumber = model.GuardianPhone,
                JamaatId = model.Jamaat.Id,
                MemberCode = model.MemberCode,
                OtherName = model.OtherName,
                PhoneNumber = model.PhoneNumber,
                Photograph = model.Photograph


            };

            await _studentRepository.AddAsync(student);
            await _studentRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "Role successfully created"
            };

        }

        public async Task<BaseResponse> UpdateStudentAsync(Guid id, UpdateStudentRequestModel model)
        {
            var studentExists = await _studentRepository.ExistsAsync(u => u.Id != id && u.MemberCode == model.MemberCode);

            if (studentExists)
            {
                throw new BadRequestException($"Student with MemberCode '{model.MemberCode}' already exists.");
            }

            var student = await _studentRepository.GetAsync(id);

            if (student == null)
            {
                throw new NotFoundException("Student does not exist");
            }

            student.Address = model.Address;
            student.AuxiliaryBody = model.AuxiliaryBody;
            student.CircuitId = model.Circuit.Id;
            student.DateOfBirth = model.DateOfBirth;
            student.EmailAddress = model.EmailAddress;
            student.FirstName = model.FirstName;
            student.Guardian = model.Guardian;
            student.GuardianPhoneNumber = model.GuardianPhone;
            student.JamaatId = model.Jamaat.Id;
            student.MemberCode = model.MemberCode;
            student.OtherName = model.OtherName;
            student.PhoneNumber = model.PhoneNumber;
            student.Photograph = model.Photograph;

            await _studentRepository.UpdateAsync(student);
            await _studentRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "Role successfully updated"
            };
        }
        public async Task<StudentsResponseModel> GetStudent()
        {
            var student = await _studentRepository.Query().Select(r => new StudentDto

            {

                Address = r.Address,
                AuxiliaryBody = r.AuxiliaryBody,
                CircuitId = r.CircuitId,
                DateOfBirth = r.DateOfBirth,
                EmailAddress = r.EmailAddress,
                FirstName = r.FirstName,
                Guardian = r.Guardian,
                GuardianPhoneNumber = r.GuardianPhoneNumber,
                JamaatId = r.JamaatId,
                MemberCode = r.MemberCode,
                OtherName = r.OtherName,
                PhoneNumber = r.PhoneNumber,
                Photograph = r.Photograph

            }).ToListAsync();

            return new StudentsResponseModel

            {
                Data = student,
                Status = true,
                Message = "Successful"
            };
        }

        public async Task<StudentResponseModel> GetStudent(Guid id)
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
                    CircuitId = student.CircuitId,
                    DateOfBirth = student.DateOfBirth,
                    EmailAddress = student.EmailAddress,
                    FirstName = student.FirstName,
                    Guardian = student.Guardian,
                    GuardianPhoneNumber = student.GuardianPhoneNumber,
                    JamaatId = student.JamaatId,
                    MemberCode = student.MemberCode,
                    OtherName = student.OtherName,
                    PhoneNumber = student.PhoneNumber,
                    Photograph = student.Photograph
                },
                Status = true,
                Message = "Successful"
            };

        }
        public async Task<StudentResponseModel> GetStudentByMemberCode(Guid MemberCode)
        {
            var student = await _studentRepository.GetAsync(MemberCode);
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
                    CircuitId = student.CircuitId,
                    DateOfBirth = student.DateOfBirth,
                    EmailAddress = student.EmailAddress,
                    FirstName = student.FirstName,
                    Guardian = student.Guardian,
                    GuardianPhoneNumber = student.GuardianPhoneNumber,
                    JamaatId = student.JamaatId,
                    MemberCode = student.MemberCode,
                    OtherName = student.OtherName,
                    PhoneNumber = student.PhoneNumber,
                    Photograph = student.Photograph
                },
                Status = true,
                Message = "Successful"
            };

        }


    }

}
