using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagement.Data.ApplicationContext;
using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Interfaces;

namespace ScholarshipManagement.Data.Repositories
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        private readonly IUserRepository _userRepository;

        public StudentRepository(SchoolDbContext context,IUserRepository userRepository )
        {
            DbContext = context;
            _userRepository = userRepository;
        }

        public async Task<Student> GetStudentAsync(string memberCode)
        {
            return await Query().SingleOrDefaultAsync(u => u.User.MemberCode == memberCode);
        }

        public async Task<Student> GetStudent(int id)
        {
            return await Query().SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IList<UpdateApplicationRequestModel>> GetStudentApplicationFormsAsync()
        {
            return await DbContext.Applications
                
                .Include(uc => uc.Student)
                //.Where(u => u.Student.CircuitId == u.UserId
                .Select(uc => new UpdateApplicationRequestModel
                {
                    StudentId = uc.StudentId,
                    MemberCode = uc.Student.User.MemberCode,
                    SurName = uc.Student.SurName,
                    FirstName = uc.Student.FirstName,
                    OtherName = uc.Student.OtherName,
                    Address = uc.Student.Address,
                    Jamaat = uc.Student.Jamaat,
                    Circuit = uc.Student.Circuit,
                    PhoneNumber = uc.Student.User.PhoneNumber,
                    EmailAddress = uc.Student.User.Email,
                    Gender = uc.Student.Gender,
                    DateOfBirth = uc.Student.DateOfBirth,
                    AuxiliaryBody = uc.Student.AuxiliaryBody,
                    GuardianFullname = uc.Student.GuardianFullname,
                    GuardianPhone = uc.Student.GuardianPhoneNumber,
                    GuardianMemberCode = uc.Student.GuardianMemberCode,
                    Photograph = uc.Student.Photograph,
                    NameOfSchool = uc.NameOfSchool,
                    AcademicLevel = uc.AcademicLevel,
                    SchoolSession = uc.SchoolSession,
                    Discipline = uc.Discipline,
                    Duration = uc.Duration,
                    ApplicationFormNumber = uc.ApplicationFormNumber,
                    DegreeInView = uc.DegreeInView,
                    SchoolBill = uc.SchoolBill,
                    AmountRequested = uc.AmountRequested,
                    CreatedDate = uc.Created
                }).ToListAsync();
        }


        public async Task<IList<ApplicationFormDto>> GetStudentApplicationFormsAsync(string memberCode)
        {

            return await DbContext.Applications
                .Include(uc => uc.Student)
                .Where(u => u.Student.User.MemberCode == memberCode)
                .Select(uc => new ApplicationFormDto
                {
                    InstitutionType = uc.InstitutionType,
                    NameOfSchool = uc.NameOfSchool,
                    AcademenicLevel = uc.AcademicLevel,
                    SchoolSession = uc.SchoolSession,
                    Discipline = uc.Discipline,
                    Duration = uc.Duration,
                    ApplicationFormNumber = uc.ApplicationFormNumber,
                    DegreeInView = uc.DegreeInView,
                    SchoolBill = uc.SchoolBill,
                    AmountRequested = uc.AmountRequested,
                    MemberCode = uc.Student.User.MemberCode,
                    BankAccountName = uc.BankAccountName,
                    BankAccountNumber = uc.BankAccountNumber,
                    BankName = uc.BankName,
                    Created = uc.Created
                }).ToListAsync();
        }

        public async Task<IList<PaymentDto>> GetStudentPaymentsAsync(int studentId)
        {

            return await DbContext.Payments
                .Include(uc => uc.ApplicationForm)
                .ThenInclude(s => s.Student)
                .Where(s => s.ApplicationForm.StudentId == studentId)
                .Select(uc => new PaymentDto
                {
                    AmountRecommended = uc.AmountRecommended,
                    ApplicationFormId = uc.ApplicationFormId,
                    ApplicationFormNumber = uc.ApplicationForm.ApplicationFormNumber,
                    AmountApprovedAndGranted = uc.AmountApprovedAndGranted,
                    memberCode = uc.ApplicationForm.Student.User.MemberCode,
                    ApprovedBy = uc.ApprovedBy,
                    DateApproved = uc.DateApproved,
                    DatePaid = uc.DatePaid,
                    FirstName = uc.ApplicationForm.Student.FirstName

                 
                }).ToListAsync();
        }

        public async Task<IList<PaymentDto>> GetStudentPaymentsAsync(string memberCode)
        {

            return await DbContext.Payments
                .Include(uc => uc.ApplicationForm)
                .ThenInclude(s => s.Student)
                .Where(s => s.ApplicationForm.Student.User.MemberCode == memberCode)
                .Select(uc => new PaymentDto
                {
                    AmountRecommended = uc.AmountRecommended,
                    ApplicationFormId = uc.ApplicationFormId,
                    ApplicationFormNumber = uc.ApplicationForm.ApplicationFormNumber,
                    AmountApprovedAndGranted = uc.AmountApprovedAndGranted,
                    memberCode = uc.ApplicationForm.Student.User.MemberCode,
                    ApprovedBy = uc.ApprovedBy,
                    DateApproved = uc.DateApproved,
                    DatePaid = uc.DatePaid,
                    FirstName = uc.ApplicationForm.Student.FirstName


                }).ToListAsync();
        }


    }
}


