using System.Linq;
using System.Threading.Tasks;
using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Interfaces;
using ScholarshipManagement.Data.Exceptions;
using ScholarshipManagement.Data.DTOs;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Grpc.Core;
using System.Security.Claims;

namespace ScholarshipManagement.Data.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IUserRepository _userRepository;
        public StudentService(IStudentRepository studentRepository, IUserRepository userRepository, IApplicationRepository applicationRepository)
        {
            _studentRepository = studentRepository;
            _applicationRepository = applicationRepository;
            _userRepository = userRepository;
        }
        //Create New Student-Bio Data
        public async Task<BaseResponse> CreateStudentAsync(CreateStudentRequestModel model, string currentUserEmail)
        {
            User user = await _userRepository.GetUserAsync(currentUserEmail);
            var StudentExist = await _studentRepository.ExistsAsync(u => (u.UserId == user.Id) || u.EmailAddress == currentUserEmail );

            if (StudentExist == true)                                        
            {
                throw new NotFoundException("Student Already Registered");
            }

            if (model.SurName == null || model.FirstName == null || model.Address == null || model.GuardianFullName == null || model.GuardianPhoneNumber == null || model.GuardianMemberCode == null || model.Photograph == null)
            {
                throw new BadRequestException("You Are missing out important information");
            }

            Student student = new()
            {
                UserId = user.Id,
                SurName = model.SurName,
                FirstName = model.FirstName,
                OtherName = model.OtherName,
                Address = model.Address,
                CircuitId = user.CircuitId,
                JamaatId = user.JamaatId,
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
               Message = "Submitted. Click Next To Apply for Scholarship"

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
            //return new StudentViewModel
            StudentViewModel ReturningCandidate = new StudentViewModel
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
            return ReturningCandidate;

        }
        //This gets all Students-Not Used Yet
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
        //Get

        public async Task<StudentResponseModel> GetApplicantById(int id)
        {

            var applicantQuery = _applicationRepository.Query()
                .Include(m => m.Student)
                .ThenInclude(u => u.User)
                .ThenInclude(j => j.Jamaat)
                .ThenInclude(c => c.Circuit)
                .Where(r => r.Id == id);

            var applicant = await applicantQuery.SingleAsync();


            StudentDto applicationQ = new StudentDto
            {
                Id = applicant.Student.Id,
                ApplicationId = applicant.Id,               
                SurName = applicant.Student.SurName,
                FirstName = applicant.Student.FirstName,
                OtherName = applicant.Student.OtherName,
                Address = applicant.Student.Address,
                CircuitId = applicant.Student.User.CircuitId,              
                JamaatId = applicant.Student.User.JamaatId,
                AuxiliaryBody = applicant.Student.AuxiliaryBody,
                PhoneNumber = applicant.Student.User.PhoneNumber,
                EmailAddress = applicant.Student.User.Email,
                MemberCode = applicant.Student.User.MemberCode,
                Gender = applicant.Student.Gender,
                DateOfBirth = applicant.Student.DateOfBirth,
                GuardianFullName = applicant.Student.GuardianFullName,
                GuardianPhoneNumber = applicant.Student.GuardianPhoneNumber,
                GuardianMemberCode = applicant.Student.GuardianMemberCode,
                Photograph = applicant.Student.Photograph

            };

            return new StudentResponseModel

            {
                Data = applicationQ,
                Status = true,
                Message = "Successful"
            };
        }
        //Post
        public async Task<BaseResponse> UpdateStudentAsync(int id, UpdateStudentRequestModel model)
        {
            var applicantQuery = _applicationRepository.Query()
               .Include(m => m.Student)
               .ThenInclude(u => u.User)
               .ThenInclude(j => j.Jamaat)
               .ThenInclude(c => c.Circuit)
               .Where(r => r.Id == id);

            var applicant = await applicantQuery.SingleAsync();
            {
                
                applicant.Student.SurName = model.SurName;
                applicant.Student.FirstName = model.FirstName;
                applicant.Student.OtherName = model.OtherName;
                applicant.Student.Address = model.Address;
                applicant.Student.User.CircuitId = model.CircuitId;          
                applicant.Student.User.JamaatId = model.JamaatId;           
                applicant.Student.AuxiliaryBody = model.AuxiliaryBody;
                applicant.Student.User.PhoneNumber = model.PhoneNumber;
                applicant.Student.User.Email = model.EmailAddress;
                applicant.Student.User.MemberCode = model.MemberCode;
                applicant.Student.Gender = model.Gender;
                applicant.Student.DateOfBirth = model.DateOfBirth;
                applicant.Student.GuardianFullName = model.GuardianFullName;
                applicant.Student.GuardianPhoneNumber = model.GuardianPhoneNumber;
                applicant.Student.GuardianMemberCode = model.GuardianMemberCode;
            };

            await _studentRepository.UpdateAsync(applicant);
            await _studentRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "Updated Successfully"
            };
        }
    }

}
