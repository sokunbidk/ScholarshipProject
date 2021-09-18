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
        private readonly IUserRepository _userRepository;
        public StudentService(IStudentRepository studentRepository, IUserRepository userRepository)
        {
            _studentRepository = studentRepository;
            _userRepository = userRepository;
        }
        //Create New Student-Bio Data
        public async Task<BaseResponse> CreateStudentAsync(CreateStudentRequestModel model, string currentUser)
        {
            User user = await _userRepository.GetUserAsync(currentUser);
            //var StudentExist = await _studentRepository.ExistsAsync(u => u.UserId.Equals user.Id);
            var StudentExist = await _studentRepository.ExistsAsync(user.Id);



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
               Message = "Submitted.Click Next To Apply for Scholarship"

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


        /*public async Task<BaseResponse> UpdateStudentAsync(int id, UpdateStudentRequestModel model)
        {
            var student = await _studentRepository.GetAsync(id);

            Student Editedstudent = new Student()
            {
                Address = model.Address,
                AuxiliaryBody = model.AuxiliaryBody,
                //DateOfBirth = model.DateOfBirth,
                FirstName = model.FirstName,
                GuardianFullName = model.GuardianFullName,
                GuardianPhoneNumber = model.GuardianPhoneNumber,
                OtherName = model.OtherName
            };

            await _studentRepository.UpdateAsync(Editedstudent);
            await _studentRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "Profile successfully updated"
            };
        }       */
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
                JamaatId = student.JamaatId,
                CircuitId = student.CircuitId,
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
        public async Task<BaseResponse> UpdateStudentAsync(int id, UpdateStudentRequestModel model)
        {

            Student student = await _studentRepository.GetAsync(id);
            {
                student.SurName = model.SurName;
                student.FirstName = model.FirstName;
                student.OtherName = model.OtherName;
                student.Address = model.Address;
                //student.CircuitId = model.CircuitId;
                //student.Jamaat = model.Jamaat;
                student.AuxiliaryBody = model.AuxiliaryBody;
                student.GuardianFullName = model.GuardianFullName;
                student.GuardianPhoneNumber = model.GuardianPhoneNumber;
                student.GuardianMemberCode = model.GuardianMemberCode;

            }

            await _studentRepository.UpdateAsync(student);
            await _studentRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "Updated Successfully"
            };
        }
    }

}
