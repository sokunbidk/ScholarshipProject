using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        public StudentRepository(SchoolDbContext context)
        {
            DbContext = context;
        }

        public async Task<Student> GetStudentAsync(string memberCode)
        {
            return await Query().SingleOrDefaultAsync(u => u.MemberCode == memberCode);
        }

        public async Task<Student> GetStudent(Guid id)
        {
            return await Query().SingleOrDefaultAsync(u => u.Id == id);
        }
        public async Task<IList<ApplicationFormDto>> GetStudentApplicationFormsAsync(Guid studentId)
        {
            
            return await DbContext.Applications
                .Include(uc => uc.Student)
                .Where(u => u.StudentId == studentId)
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
                    MemberCode = uc.Student.MemberCode,
                    BankAccountName = uc.BankAccountName,
                    BankAccountNumber = uc.BankAccountNumber,
                    BankName = uc.BankName,
                    Created = uc.Created,
                    

                }).ToListAsync();
        }


        public async Task<IList<ApplicationFormDto>> GetStudentApplicationFormsAsync(string memberCode)
        {

            return await DbContext.Applications
                .Include(uc => uc.Student)
                .Where(u => u.Student.MemberCode == memberCode)
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
                    MemberCode = uc.Student.MemberCode,
                    BankAccountName = uc.BankAccountName,
                    BankAccountNumber = uc.BankAccountNumber,
                    BankName = uc.BankName,
                    Created = uc.Created
                }).ToListAsync();
        }

        public async Task<IList<PaymentDto>> GetStudentPaymentsAsync(Guid studentId)
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
                    memberCode = uc.ApplicationForm.Student.MemberCode,
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
                .Where(s => s.ApplicationForm.Student.MemberCode == memberCode)
                .Select(uc => new PaymentDto
                {
                    AmountRecommended = uc.AmountRecommended,
                    ApplicationFormId = uc.ApplicationFormId,
                    ApplicationFormNumber = uc.ApplicationForm.ApplicationFormNumber,
                    AmountApprovedAndGranted = uc.AmountApprovedAndGranted,
                    memberCode = uc.ApplicationForm.Student.MemberCode,
                    ApprovedBy = uc.ApprovedBy,
                    DateApproved = uc.DateApproved,
                    DatePaid = uc.DatePaid,
                    FirstName = uc.ApplicationForm.Student.FirstName


                }).ToListAsync();
        }


    }
}


