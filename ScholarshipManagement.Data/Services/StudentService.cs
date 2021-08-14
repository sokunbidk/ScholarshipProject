using System.Linq;
using System.Threading.Tasks;
using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Interfaces;
using ScholarshipManagement.Data.Exceptions;
using ScholarshipManagement.Data.DTOs;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Grpc.Core;

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
        //Create New Student-Bio Data
        public async Task<BaseResponse> CreateStudentAsync(CreateStudentRequestModel model,string currentUser)
        {
            User user = await _userRepository.GetUserAsync(currentUser);
            
            //MemberCode is used instead of currentuser just for further authentication

            if (user == null || user.MemberCode != model.MemberCode)
            {
                throw new NotFoundException("User Does Not Exist");
            }
           
            Student student = new()

            {

                UserId = user.Id,
                SurName = model.SurName,
                FirstName = model.FirstName,
                OtherName = model.OtherName,
                Address = model.Address,
                JamaatId = model.JamaatId,
                AuxiliaryBody = model.AuxiliaryBody,
                PhoneNumber = user.PhoneNumber,
                EmailAddress = user.Email,
                MemberCode = user.MemberCode,
                Gender = model.Gender,
                DateOfBirth = model.DateOfBirth,
                GuardianFullName = model.GuardianFullName,
                GuardianPhoneNumber = model.GuardianPhoneNumber,
                GuardianMemberCode = model.GuardianMemberCode,
                Photograph = model.Photograph,
                CreatedBy = user.UserFullName

            };

                await _studentRepository.AddAsync(student);
                await _studentRepository.SaveChangesAsync();
            
            return new BaseResponse
            {
                Status = true,
                Message = "Student successfully created"
            };

        }
        //Get-Returning Candidate ReadOnly-View
        public async Task<StudentViewModel> GetStudentReturningCandidate(string email)
        {
            //var student = await _studentRepository.GetStudentByEmail(Email);
            var student = await _studentRepository.Query()
                .Include(c => c.Jamaat)
                .ThenInclude(c => c.Circuit)
                .Include(u => u.User)
                .SingleOrDefaultAsync(s => s.EmailAddress == email);


            if (student == null)

            {
                throw new NotFoundException("Student does not exist");
            }
            return new StudentViewModel
            {
                StudentId = student.Id,
                UserId = student.UserId,
                SurName = student.SurName,
                FirstName = student.FirstName,
                OtherName = student.OtherName,
                Address = student.Address,
                AuxiliaryBody = student.AuxiliaryBody,
                Circuit = student.Jamaat.Circuit.CircuitName,
                Jamaat = student.Jamaat.JamaatName,
                DateOfBirth = student.DateOfBirth,
                EmailAddress = student.User.Email,
                Gender = student.Gender,
                GuardianFullName = student.GuardianFullName,
                GuardianPhoneNumber = student.GuardianPhoneNumber,
                MemberCode = student.User.MemberCode,
                PhoneNumber = student.User.PhoneNumber,
                Photograph = student.Photograph,
                GuardianMemberCode = student.GuardianMemberCode
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
                GuardianFullName = r.GuardianFullName,
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

        public async Task<StudentResponseModel> GetApplicantById(int id)
        {
            var student = await _studentRepository.GetStudentWithJamatByIdAsync(id);

            StudentDto applicant = new StudentDto
            {
                UserId = student.Id,
                SurName = student.SurName,
                FirstName = student.FirstName,
                OtherName = student.OtherName,
                Address = student.Address,
                CircuitName = student.Jamaat.Circuit.CircuitName,
                JamaatName = student.Jamaat.JamaatName,
                AuxiliaryBody = student.AuxiliaryBody,
                PhoneNumber = student.PhoneNumber,
                EmailAddress = student.EmailAddress,
                MemberCode = student.MemberCode,
                Gender = student.Gender,
                DateOfBirth = student.DateOfBirth,
                GuardianFullName = student.GuardianFullName,
                GuardianPhoneNumber = student.GuardianPhoneNumber,
                GuardianMemberCode = student.GuardianMemberCode,
                Photograph = student.Photograph
            };

            return new StudentResponseModel
            {
                Data = applicant,
                Status = true,
                Message = "Successful"
            };
        }
    }

}
