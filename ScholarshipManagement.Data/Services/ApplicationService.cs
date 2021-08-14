using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using ScholarshipManagement.Data.Exceptions;
using ScholarshipManagement.Data.DTOs;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagement.Data.ApplicationContext;
using ScholarshipManagement.Data.Repositories;
using ScholarshipManagement.Data.Enums;
using System.Collections.Generic;

namespace ScholarshipManagement.Data.Services
{
    public class ApplicationService : BaseRepository<Student>, IApplicationService
    {
        private readonly IApplicationFormRepository _applicationFormRepository;
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;
        public ApplicationService(SchoolDbContext context, IApplicationFormRepository applicationRepository, IUserRepository userRepository, IStudentRepository studentRepository)
        {
            DbContext = context;
            _applicationFormRepository = applicationRepository;
            _userRepository = userRepository;
            _studentRepository = studentRepository;
        }
        //Apply for Scholarship-Returning Student
        public async Task<BaseResponse> CreateApplicationAsync(CreateApplicationFormRequestModel model, string email)
        {
            Student student = await _studentRepository.GetStudentByEmail(email);

            //var user = await _userRepository.GetUserByMemberCodeAsync(model.MemberCode);

            var applicantionFormSameLevelExists = await _applicationFormRepository.ExistsAsync(u => (u.AcademicLevel == model.AcademicLevel || u.SchoolSession == model.SchoolSession)
            && u.StudentId == student.Id);

            if (applicantionFormSameLevelExists)
            {
                throw new BadRequestException($"Application form already exist for  '{model.AcademicLevel}' OR {model.SchoolSession}Level.");

            }
            var applicantion = new ApplicationForm
            {
                StudentId = student.Id,
                InstitutionType = model.InstitutionType,
                NameOfSchool = model.NameOfSchool,
                AcademicLevel = model.AcademicLevel,
                SchoolSession = model.SchoolSession,
                Discipline = model.Discipline,
                Duration = model.Duration,
                DegreeInView = model.DegreeInView,
                DateAdmitted = model.DateAdmitted,
                YearToGraduate = model.YearToGraduate,
                LetterOfAdmission = model.LetterOfAdmission,
                AmountRequested = model.AmountRequested,
                BankName = model.BankName,
                BankAccountNumber = model.BankAccountNumber,
                BankAccountName = model.BankAccountName,
                LastSchoolResult = model.LastSchoolResult,
                SchoolBill = model.SchoolBill,
                Status = ApprovalStatus.Default
                //StatusId = model.StatusId,

            };
            await _applicationFormRepository.AddAsync(applicantion);
            await _applicationFormRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "Application form successfully submitted"
            };
        }
        //Apply for Scholarship-New Student
        public async Task<BaseResponse> CreateNewApplicationAsync(CreateApplicationFormRequestModel model, string currentUser)
        {

            Student student = await _studentRepository.GetStudentByEmail(currentUser);

            var applicantion = new ApplicationForm
            {
                StudentId = student.Id,
                InstitutionType = model.InstitutionType,
                NameOfSchool = model.NameOfSchool,
                AcademicLevel = model.AcademicLevel,
                SchoolSession = model.SchoolSession,
                Discipline = model.Discipline,
                Duration = model.Duration,
                DegreeInView = model.DegreeInView,
                YearToGraduate = model.YearToGraduate,
                DateAdmitted = model.DateAdmitted,
                LetterOfAdmission = model.LetterOfAdmission,
                AmountRequested = model.AmountRequested,
                BankName = model.BankName,
                BankAccountName = model.BankAccountName,
                BankAccountNumber = model.BankAccountNumber,
                LastSchoolResult = model.LastSchoolResult,
                SchoolBill = model.SchoolBill,
                Status = ApprovalStatus.Default
                //StatusId = (int)ApprovalStatus.Default                     //Enum

            };

            await _applicationFormRepository.AddAsync(applicantion);
            await _applicationFormRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "Application form successfully submitted"
            };

        }
        //Pending Applications List
        public async Task<List<PendingApplicationsDto>> PendingApplications()
        {
            //var student = await _studentRepository.GetStudentByEmail(Email);
            var applications = await _applicationFormRepository.Query()
                .Include(d => d.Student)
                .ThenInclude(j => j.Jamaat)
                .ThenInclude(c => c.Circuit)
                .Where(ap => ap.Status == ApprovalStatus.Default)
                .ToListAsync();


            if (applications == null)

            {
                throw new NotFoundException("No Pending Application");
            }
            else
            {
                List<PendingApplicationsDto> pendingApplicationsList = new List<PendingApplicationsDto>();
                foreach (var application in applications)
                {
                    var pendingApplication = new PendingApplicationsDto
                    {
                        ApplicationFormId = application.Id,
                        StudentId = application.StudentId,
                        SurName = application.Student.SurName,
                        FirstName = application.Student.FirstName,
                        OtherName = application.Student.OtherName,
                        AuxiliaryBody = application.Student.AuxiliaryBody,
                        CircuitId = application.Student.Jamaat.Circuit.CircuitName,
                        Jamaat = application.Student.Jamaat.JamaatName,
                        NameOfSchool = application.NameOfSchool,
                        Discipline = application.Discipline,
                        AcademenicLevel = application.AcademicLevel,
                        AmountRequested = application.AmountRequested,
                        Remarks = application.Remarks,
                        Status = application.Status,

                    };
                    pendingApplicationsList.Add(pendingApplication);
                }
                return pendingApplicationsList;
            }

        }


        public async Task<List<PendingApplicationsDto>> PendingApplicationsByStatus(List<ApprovalStatus> statuses, bool isGlobal, List<int> circuitIds)
        {

            var applicationsQuery =  _applicationFormRepository.Query()
                .Include(d => d.Student)
                .ThenInclude(c => c.Jamaat)
                .ThenInclude(j => j.Circuit)
                .Where(ap => statuses.Contains(ap.Status));

            if (!isGlobal)
            {
               applicationsQuery =  applicationsQuery.Where(p => circuitIds.Contains(p.Student.Jamaat.CircuitId));
            }

            var applications = await applicationsQuery.ToListAsync();
            if (applications == null)

            {
                throw new NotFoundException("No Pending Application");
            }
            else
            {
                List<PendingApplicationsDto> pendingApplicationsList = new List<PendingApplicationsDto>();
                foreach (var application in applications)
                {
                    var pendingApplication = new PendingApplicationsDto
                    {
                        ApplicationFormId = application.Id,
                        StudentId = application.StudentId,
                        SurName = application.Student.SurName,
                        FirstName = application.Student.FirstName,
                        OtherName = application.Student.OtherName,
                        AuxiliaryBody = application.Student.AuxiliaryBody,
                        CircuitId = application.Student.Jamaat.Circuit.CircuitName,
                        Jamaat = application.Student.Jamaat.JamaatName,
                        NameOfSchool = application.NameOfSchool,
                        Discipline = application.Discipline,
                        AcademenicLevel = application.AcademicLevel,
                        AmountRequested = application.AmountRequested,
                        Remarks = application.Remarks,
                        Status = application.Status,

                    };
                    pendingApplicationsList.Add(pendingApplication);
                }
                return pendingApplicationsList;
            }

        }



        //Pending Applications Detail
        public async Task<ApplicationResponseModel> GetApplication(int id)
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
                    AcademicLevel = applicant.AcademicLevel,
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


        public async Task<BaseResponse> UpdateApplicationAsync(UpdateApplicationRequestModel model)
        {
            var applicantExists = await _applicationFormRepository.ExistsAsync(u => u.Id != model.StudentId && u.Student.User.MemberCode == model.MemberCode);

            if (applicantExists)
            {
                throw new BadRequestException($"Student with MemberCode {model.MemberCode}' already exists.");
            }

            await _applicationFormRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "Approval Given Successfully"
            };
        }

        public async Task<ApplicationsResponseModel> GetApplications()
        {
            var applicantform = await _applicationFormRepository.Query().Select(r => new ApplicationFormDto

            {

                InstitutionType = r.InstitutionType,
                NameOfSchool = r.NameOfSchool,
                AcademicLevel = r.AcademicLevel,
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

        public async Task<ApplicationsResponseModel> GetApplicationAsync(ApplicationFormViewModel model)
        {
            var applicantform = await _applicationFormRepository.Query().Select(r => new ApplicationFormDto
            {

                InstitutionType = r.InstitutionType,
                NameOfSchool = r.NameOfSchool,
                AcademicLevel = r.AcademicLevel,
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


        public Task<BaseResponse> UpdateAsync(UpdateApplicationRequestModel model)
        {
            throw new NotImplementedException();
        }
        public Task<ApplicationForm> GetApplicationFormAsync(int applicationFormNumber)
        {
            throw new NotImplementedException();
        }
        public Task<BaseResponse> UpdateApplicationAsync(int id, UpdateApplicationRequestModel model)
        {
            throw new NotImplementedException();

        }
        public async Task<BaseResponse> UpdateApprovalStatus(int id, int userId)
        {
            
            var application = await _applicationFormRepository.GetAsync(id);
            
            var user = await _userRepository.GetAsync(userId);

            if (application.Status == ApprovalStatus.Default && user.UserType == UserType.Circuit)
            {
                application.Status = ApprovalStatus.Committee;
                await _applicationFormRepository.UpdateAsync(application);
                await _applicationFormRepository.SaveChangesAsync();

            }
            else if (application.Status == ApprovalStatus.Committee && user.UserType == UserType.Committee)
            {
                application.Status = ApprovalStatus.NaibAmir;
                await _applicationFormRepository.UpdateAsync(application);
                await _applicationFormRepository.SaveChangesAsync();
            }
            else if (application.Status == ApprovalStatus.NaibAmir && user.UserType == UserType.NaibAmir)
            {
                application.Status = ApprovalStatus.Amir;
                await _applicationFormRepository.UpdateAsync(application);
                await _applicationFormRepository.SaveChangesAsync();
            }
            else if (application.Status == ApprovalStatus.Amir && user.UserType == UserType.Amir)
            {
                application.Status = ApprovalStatus.Accounts;
                await _applicationFormRepository.UpdateAsync(application);
                await _applicationFormRepository.SaveChangesAsync();
            }
            else if (application.Status == ApprovalStatus.Accounts && user.UserType == UserType.Accounts)
            {
                application.Status = ApprovalStatus.Disbursement;
                await _applicationFormRepository.UpdateAsync(application);
                await _applicationFormRepository.SaveChangesAsync();
            }


            return new BaseResponse
            {
                Status = true,
                Message = "Application form successfully updated"
            };
        }
        public async Task<BaseResponse> DeclineApprovalStatus(int id)
        {
            
            var application = await _applicationFormRepository.GetAsync(id);

            application.Status = ApprovalStatus.Declined;
            await _applicationFormRepository.AddAsync(application);
            await _applicationFormRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "Application form Declined"
            };

        }
    }
}


