using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using ScholarshipManagement.Data.Exceptions;
using ScholarshipManagement.Data.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ScholarshipManagement.Data.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationFormRepository _applicationFormRepository;
        public ApplicationService(IApplicationFormRepository applicationRepository)
        {
            _applicationFormRepository = applicationRepository;
        }
        public async Task<BaseResponse> CreateApplicationAsync(CreateApplicationFormRequestModel model)
        { 
        
            var applicantionFormExists = await _applicationFormRepository.ExistsAsync(u => u.ApplicationFormNumber == model.ApplicationFormNumber);
            if (applicantionFormExists)
            {
                throw new BadRequestException($"Applicantion form with name '{model.ApplicationFormNumber}' already exists.");

            }
            var applicantionFormSameLevelExists = await _applicationFormRepository.ExistsAsync(u => u.AcademicLevel == model.AcademicLevel && u.Student.Id == model.StudentId);

            if (applicantionFormSameLevelExists)
            {
                throw new BadRequestException($"Application form already exist for  '{model.AcademicLevel}'Level.");

            }

            var applicantion = new ApplicationForm
            {
                InstitutionType = model.InstitutionType,
                NameOfSchool= model.NameOfSchool,
                AcademicLevel = model.AcademicLevel,
                SchoolSession = model.SchoolSession,
                Discipline = model.Discipline,
                Duration = model.Duration,
                DegreeInView = model.DegreeInView,
                YearToGraduate = model.YearToGraduate,
                LetterOfAdmission = model.LetterOfAdmission,
                AmountRequested = model.AmountRequested,
                BankName = model.BankName,
                BankAccountNumber = model.BankAccountNumber,
                LastSchoolResult = model.LastSchoolResult,
                SchoolBill = model.SchoolBill
                
             
            };

            await _applicationFormRepository.AddAsync(applicantion);
            await _applicationFormRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "Application form successfully submitted"
            };
       
        }

        public async Task<BaseResponse> UpdateApplicationAsync(Guid id, UpdateApplictionRequestModel model)
        {
            var applicantExists = await _applicationFormRepository.ExistsAsync(u => u.Id != id && u.Student.MemberCode == model.MemberCode);
            if (applicantExists)
            {
                throw new BadRequestException($"Student with MemberCode '{model.MemberCode}' already exists.");
            }
            var applicant = await _applicationFormRepository.GetAsync(id);
            if (applicant == null)
            {
                throw new NotFoundException("Role does not exist");
            }
            applicant.InstitutionType = model.InstitutionType;
            applicant.NameOfSchool = model.NameOfSchool;
            applicant.AcademicLevel = model.AcademicLevel;
            applicant.SchoolSession = model.SchoolSession;
            applicant.Discipline = model.Discipline;
            applicant.Duration = model.Duration;
            applicant.DegreeInView = model.DegreeInView;
            applicant.YearToGraduate = model.YearToGraduate;
            applicant.LetterOfAdmission = model.LetterOfAdmission;
            applicant.AmountRequested = model.AmountRequested;
            applicant.BankName = model.BankName;
            applicant.BankAccountNumber = model.BankAccountNumber;
            applicant.BankAccountName = model.BankAccountName;
            applicant.LastSchoolResult = model.LastSchoolResult;
            applicant.SchoolBill = model.SchoolBill;

            await _applicationFormRepository.UpdateAsync(applicant);
            await _applicationFormRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "Role successfully updated"
            };
        }

        public async Task<ApplicationsResponseModel> GetApplications()
        {
            var applicantform = await _applicationFormRepository.Query().Select(r => new ApplicationFormDto

            {

                InstitutionType = r.InstitutionType,
                NameOfSchool = r.NameOfSchool,
                AcademenicLevel = r.AcademicLevel,
                SchoolSession = r.SchoolSession,
                Discipline = r.Discipline,
                Duration = r.Duration,
                DegreeInView = r.DegreeInView,
                YearToGraduate = r.YearToGraduate,
                LetterOfAdmission = r.LetterOfAdmission,
                AmountRequested = r.AmountRequested,
                BankName = r.BankName,
                BankAccountNumber = r.BankAccountNumber,
                BankAccountName = r.BankAccountName,
                LastSchoolResult = r.LastSchoolResult,
                SchoolBill = r.SchoolBill,
            }).ToListAsync();

            return new ApplicationsResponseModel
            {
                Data = applicantform,
                Status = true,
                Message = "Successful"
            };
        }

        public async Task<ApplicationResponseModel> GetApplication(Guid id)
        {
            var applicant = await _applicationFormRepository.GetAsync(id);
            if (applicant == null)
            {
                throw new NotFoundException("Applicant does not exist");
            }
            return new ApplicationResponseModel
            {
                Data = new ApplicationFormDto
                {


                    InstitutionType = applicant.InstitutionType,
                    NameOfSchool = applicant.NameOfSchool,
                    AcademenicLevel = applicant.AcademicLevel,
                    SchoolSession = applicant.SchoolSession,
                    Discipline = applicant.Discipline,
                    Duration = applicant.Duration,
                    DegreeInView = applicant.DegreeInView,
                    YearToGraduate = applicant.YearToGraduate,
                    LetterOfAdmission = applicant.LetterOfAdmission,
                    AmountRequested = applicant.AmountRequested,
                    BankName = applicant.BankName,
                    BankAccountNumber = applicant.BankAccountNumber,
                    BankAccountName = applicant.BankAccountName,
                    LastSchoolResult = applicant.LastSchoolResult,
                    SchoolBill = applicant.SchoolBill,
                },


                Status = true,
                Message = "Successful"


            };


        }

    }
}
