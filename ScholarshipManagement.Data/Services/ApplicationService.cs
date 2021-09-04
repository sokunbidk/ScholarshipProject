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
                UderId = student.UserId,
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
                //LetterOfAdmission = model.LetterOfAdmission,
                AmountRequested = model.AmountRequested,
                BankName = model.BankName,
                BankAccountNumber = model.BankAccountNumber,
                BankAccountName = model.BankAccountName,
                LastSchoolResult = model.LastSchoolResult,
                SchoolBill = model.SchoolBill,
                Status = ApprovalStatus.Draft
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

           // var studentExists = await _studentRepository.ExistsAsync(u =>u.UserId == student.UserId);
            
            if (student == null)
            {
                throw new BadRequestException("This Candidate is Yet To Register as a Student");
            }
            var applicantionFormSameLevelExists = await _applicationFormRepository.ExistsAsync(u => (u.AcademicLevel == model.AcademicLevel || u.SchoolSession == model.SchoolSession)
            && u.StudentId == student.Id);

            if (applicantionFormSameLevelExists)
            {
                throw new BadRequestException($"Application form already exist for  '{model.AcademicLevel}' OR {model.SchoolSession}Level.");
            }
            ApplicationForm applicantion = new ApplicationForm
            {
                UderId = student.UserId,
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
                //LastSchoolResult = model.LastSchoolResult,
                SchoolBill = model.SchoolBill,
                Status = ApprovalStatus.Draft
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
        /*public async Task<List<PendingApplicationsDto>> PendingApplications()
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

        }*/


        public async Task<List<PendingApplicationsDto>> PendingApplicationsByStatus(List<ApprovalStatus> statuses, bool isGlobal, List<int> circuitIds, int Id)
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
            /*if(!isGlobal)
            {
                applicationsQuery = applicationsQuery.Where(p => Id == p.Student.UserId);
            }*/
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
                                       Id = application.UderId,
                        ApplicationFormId = application.Id,
                        StudentId = application.StudentId,
                        SurName = application.Student.SurName +
                      application.Student.FirstName  + application.Student.OtherName,
                        //FirstName = application.Student.FirstName,
                        //OtherName = application.Student.OtherName,
                        AuxiliaryBody = application.Student.AuxiliaryBody,
                        CircuitName = application.Student.Jamaat.Circuit.CircuitName,
                        Jamaat = application.Student.Jamaat.JamaatName,
                        NameOfSchool = application.NameOfSchool,
                        Discipline = application.Discipline,
                        AcademenicLevel = application.AcademicLevel,
                        AmountRequested = application.AmountRequested,
                        GuardianFullName = application.Student.GuardianFullName,
                        GuardianPhoneNumber = application.Student.GuardianPhoneNumber,
                        Remarks = application.Remarks,
                        Status = application.Status,

                    };
                    pendingApplicationsList.Add(pendingApplication);
                }
                return pendingApplicationsList;
            }

        }
        public async Task<List<PendingApplicationsDto>> StudentApplicationStatus(int id)
        {
            var StatusQuary = _applicationFormRepository.Query()
                .Include(s => s.Student)
                .ThenInclude(j => j.Jamaat)
                .ThenInclude(c => c.Circuit)
                .Where(p => p.UderId == id)
                .Where(r => r.UderId == r.Student.UserId);
                
                

            var applicationStatus = await StatusQuary.ToListAsync();


            List<PendingApplicationsDto> studentStatusList = new List<PendingApplicationsDto>();
            foreach (var application in applicationStatus)
            {
                var studentStatus = new PendingApplicationsDto
                {
                    Id = application.UderId,
                    ApplicationFormId = application.Id,
                    StudentId = application.StudentId,
                    SurName = application.Student.SurName +
                      application.Student.FirstName + application.Student.OtherName,
                    //FirstName = application.Student.FirstName,
                    //OtherName = application.Student.OtherName,
                    AuxiliaryBody = application.Student.AuxiliaryBody,
                    CircuitName = application.Student.Jamaat.Circuit.CircuitName,
                    Jamaat = application.Student.Jamaat.JamaatName,
                    NameOfSchool = application.NameOfSchool,
                    Discipline = application.Discipline,
                    AcademenicLevel = application.AcademicLevel,
                    AmountRequested = application.AmountRequested,
                    GuardianFullName = application.Student.GuardianFullName,
                    GuardianPhoneNumber = application.Student.GuardianPhoneNumber,
                    Remarks = application.Remarks,
                    Status = application.Status,
                };
                studentStatusList.Add(studentStatus);
            }
            return (studentStatusList);
        }

        //View Pending Application To Edit
        public async Task<ApplicationResponseModel> GetApplication(int id)
        {
            var applicant = await _applicationFormRepository.GetAsync(id);

            ApplicationFormDto application = new ApplicationFormDto
            {

                InstitutionType = applicant.InstitutionType,
                NameOfSchool = applicant.NameOfSchool,
                AcademicLevel = applicant.AcademicLevel,
                SchoolSession = applicant.SchoolSession,
                Discipline = applicant.Discipline,
                Duration = applicant.Duration,
                DegreeInView = applicant.DegreeInView,
                YearToGraduate = applicant.YearToGraduate,
                DateAdmitted = applicant.DateAdmitted,
                //LetterOfAdmission = applicant.LetterOfAdmission,
                AmountRequested = applicant.AmountRequested,
                BankName = applicant.BankName,
                BankAccountNumber = applicant.BankAccountNumber,
                BankAccountName = applicant.BankAccountName,
                //LastSchoolResult = applicant.LastSchoolResult,
                //SchoolBill = applicant.SchoolBill,
            };
            return new ApplicationResponseModel
            { 
                Data = application,
                Status = true,
                Message = "Successful"
            };
        }

        public async Task<BaseResponse> UpdateApplicationAsync(int id, UpdateApplicationRequestModel model)
        {

            ApplicationForm application = await _applicationFormRepository.GetAsync(id);
            {

                application.InstitutionType = model.InstitutionType;
                application.NameOfSchool = model.NameOfSchool;
                application.AcademicLevel = model.AcademicLevel;
                application.SchoolSession = model.SchoolSession;
                application.Discipline = model.Discipline;
                application.Duration = model.Duration;
                application.DegreeInView = model.DegreeInView;
                application.DateAdmitted = model.DateAdmitted;
                application.YearToGraduate = model.YearToGraduate;
                //application.LetterOfAdmission = model.LetterOfAdmission;
                application.AmountRequested = model.AmountRequested;
                application.BankName = model.BankName;
                application.BankAccountNumber = model.BankAccountNumber;
                application.BankAccountName = model.BankAccountName;
               // application.LastSchoolResult = model.LastSchoolResult;
                //application.SchoolBill = model.SchoolBill;

                await _applicationFormRepository.UpdateAsync(application);
                await _applicationFormRepository.SaveChangesAsync();
            }

            return new BaseResponse
            {
                Status = true,
                Message = "successfully updated"
            };

        }
        public async Task<BaseResponse> UpdateApprovalStatus(int id, int userId)
        {
            
            var application = await _applicationFormRepository.GetAsync(id);
            
            var user = await _userRepository.GetAsync(userId);

            if (application.Status == ApprovalStatus.Draft && user.UserType == UserType.Circuit)
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
                application.Status = ApprovalStatus.Disbursed;
                await _applicationFormRepository.UpdateAsync(application);
                await _applicationFormRepository.SaveChangesAsync();
            }
            else
            {
                throw new BadRequestException ("You Do Not Have Access Right to this Action");
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
            await _applicationFormRepository.UpdateAsync(application);
            await _applicationFormRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "Application form Declined"
            };

        }
        public async void DeleteApplication(int id)
        {
            
            var application = await _applicationFormRepository.GetAsync(id);

            /*UserDto r = new UserDto
            {
                UserFullName = application.UserFullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                MemberCode = user.MemberCode,
                UserType = user.UserType,

            };*/

            await _userRepository.DeleteAsync(application);
            await _userRepository.SaveChangesAsync();
        }
    }
}


