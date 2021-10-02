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
using Grpc.Core;

namespace ScholarshipManagement.Data.Services
{
    public class ApplicationService : BaseRepository<Student>, IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;
        public ApplicationService(SchoolDbContext context, IApplicationRepository applicationRepository, IUserRepository userRepository, IStudentRepository studentRepository)
        {
            DbContext = context;
            _applicationRepository = applicationRepository;
            _userRepository = userRepository;
            _studentRepository = studentRepository;
        }
        //Apply for Scholarship-Returning Student
        public async Task<BaseResponse> CreateApplicationAsync(CreateApplicationRequestModel model, string email)
        {
            Student student = await _studentRepository.GetStudentByEmail(email);

            var SameLevelExists = await _applicationRepository.ExistsAsync(u => (u.AcademicLevel == model.AcademicLevel) && u.StudentId == student.Id);
            //student cannot apply within same academic year-Not implemented
            if (SameLevelExists == true)
            {
                throw new BadRequestException($"Application Already exist for {model.AcademicLevel} Level");

            }
            var SameSessionlExists = await _applicationRepository.ExistsAsync(u => (u.SchoolSession == model.SchoolSession) && u.StudentId == student.Id);
            if (SameSessionlExists == true)
            {
                throw new BadRequestException($"Application Already exist for '{model.SchoolSession} Session.");
            }
            else
            {
                var applicantion = new application
                {
                    UserId = student.UserId,
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
                    Status = ApprovalStatus.Submitted


                };
                await _applicationRepository.AddAsync(applicantion);
                await _applicationRepository.SaveChangesAsync();
            }
            return new BaseResponse
            {
                Status = true,
                Message = "successfull. Please Inform Your Circuit Amsa President/Circuit President for Consent"
            };
        }
        //Apply for Scholarship-New Student
        public async Task<BaseResponse> CreateNewApplicationAsync(CreateApplicationRequestModel model, string currentUser)
        {
            Student student = await _studentRepository.GetStudentByEmail(currentUser);

            var studentExists = await _studentRepository.ExistsAsync(u => u.EmailAddress == currentUser);
            //student cannot apply within same academic year-Not implemented

            if (student == null || studentExists == false)
            {
                throw new BadRequestException("This Candidate is Yet To Register as an Applicant or Student");
            }

            var SameLevelExists = await _applicationRepository.ExistsAsync(u => (u.AcademicLevel == model.AcademicLevel) && u.StudentId == student.Id);
            if (SameLevelExists == true)
            {
                throw new BadRequestException($"Application Already exist for {model.AcademicLevel} Level");

            }

            var SameSessionlExists = await _applicationRepository.ExistsAsync(u => (u.SchoolSession == model.SchoolSession) && u.StudentId == student.Id);
            if (SameSessionlExists == true)
            {
                throw new BadRequestException($"Application Already exist for '{model.SchoolSession} Session.");
            }
            else
            {
                application applicantion = new application
                {
                    UserId = student.UserId,
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
                    Status = ApprovalStatus.Submitted
                };

                await _applicationRepository.AddAsync(applicantion);
                await _applicationRepository.SaveChangesAsync();
            }
            return new BaseResponse
            {
                Status = true,
                Message = "Application form successfully submitted"
            };

        }
        public async Task<List<PendingApplicationsDto>> PendingApplicationsByStatus(List<ApprovalStatus> statuses, bool isGlobal, List<int> circuitIds, int Id)
        {

            var applicationsQuery = _applicationRepository.Query()
                .Include(d => d.Student)
                .ThenInclude(c => c.Jamaat)
                .ThenInclude(j => j.Circuit)
                .Where(ap => statuses.Contains(ap.Status));

            if (!isGlobal)
            {
                applicationsQuery = applicationsQuery.Where(p => circuitIds.Contains(p.Student.Jamaat.CircuitId));
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
                        Id = application.UserId,
                        ApplicationId = application.Id,
                        StudentId = application.StudentId,
                        Names = $"{application.Student.SurName}, {application.Student.FirstName }  {application.Student.OtherName}",
                        AuxiliaryBody = application.Student.AuxiliaryBody,
                        CircuitName = application.Student.Jamaat.Circuit.CircuitName,
                        Jamaat = application.Student.Jamaat.JamaatName,
                        NameOfSchool = application.NameOfSchool,
                        Discipline = application.Discipline,
                        AcademicLevel = application.AcademicLevel,
                        SchoolSession = application.SchoolSession,
                        AmountRequested = application.AmountRequested,
                        AmountRecommended = application.AmountRecommended,
                        GuardianFullName = application.Student.GuardianFullName,
                        GuardianPhoneNumber = application.Student.GuardianPhoneNumber,
                        BankAccountName = application.BankAccountName,
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
            var StatusQuary = _applicationRepository.Query()
                .Include(s => s.Student)
                .ThenInclude(j => j.Jamaat)
                .ThenInclude(c => c.Circuit)
                .Where(p => p.UserId == id)
                .Where(r => r.UserId == r.Student.UserId);
            //.Where(T => T.Status == ApprovalStatus.Submitted);

            var applicationStatus = await StatusQuary.ToListAsync();
            List<PendingApplicationsDto> studentStatusList = new List<PendingApplicationsDto>();
            foreach (var application in applicationStatus)
            {
                if (applicationStatus == null)
                {
                    throw new NotFoundException("You Do Not Have Any Pending Application");
                }

                else

                {
                    if (application.Status == ApprovalStatus.Committee || application.Status == ApprovalStatus.NaibAmir || application.Status == ApprovalStatus.Amir || application.Status == ApprovalStatus.Accounts)
                    {


                        var studentStatus = new PendingApplicationsDto

                        {

                            Id = application.UserId,
                            ApplicationId = application.Id,
                            StudentId = application.StudentId,
                            Names = $"{application.Student.SurName}, {application.Student.FirstName }  {application.Student.OtherName}",
                            AuxiliaryBody = application.Student.AuxiliaryBody,
                            CircuitName = application.Student.Jamaat.Circuit.CircuitName,
                            Jamaat = application.Student.Jamaat.JamaatName,
                            NameOfSchool = application.NameOfSchool,
                            Discipline = application.Discipline,
                            AcademicLevel = application.AcademicLevel,
                            SchoolSession = application.SchoolSession,
                            AmountRequested = application.AmountRequested,
                            GuardianFullName = application.Student.GuardianFullName,
                            GuardianPhoneNumber = application.Student.GuardianPhoneNumber,
                            Remarks = application.Remarks,
                            //Status = (ApprovalStatus)int.Parse("Work IN Progress")
                            Status = ApprovalStatus.In_Progress
                        };
                        studentStatusList.Add(studentStatus);
                    }
                    else
                    if ( application.Status == ApprovalStatus.Approved || application.Status == ApprovalStatus.Declined || application.Status == ApprovalStatus.Submitted)
                    {
                        var studentStatus = new PendingApplicationsDto

                        {

                            Id = application.UserId,
                            ApplicationId = application.Id,
                            StudentId = application.StudentId,
                            Names = $"{application.Student.SurName}, {application.Student.FirstName }  {application.Student.OtherName}",
                            AuxiliaryBody = application.Student.AuxiliaryBody,
                            CircuitName = application.Student.Jamaat.Circuit.CircuitName,
                            Jamaat = application.Student.Jamaat.JamaatName,
                            NameOfSchool = application.NameOfSchool,
                            Discipline = application.Discipline,
                            AcademicLevel = application.AcademicLevel,
                            SchoolSession = application.SchoolSession,
                            AmountRequested = application.AmountRequested,
                            GuardianFullName = application.Student.GuardianFullName,
                            GuardianPhoneNumber = application.Student.GuardianPhoneNumber,
                            Remarks = application.Remarks,
                            Status = application.Status,
                        };
                        studentStatusList.Add(studentStatus);
                    }
                }              
            }
            return (studentStatusList);
        }
        public async Task<List<PendingApplicationsDto>> StudentApplicationHistory(int id)
        {
            var StatusQuary = _applicationRepository.Query()
                .Include(s => s.Student)
                .ThenInclude(j => j.Jamaat)
                .ThenInclude(c => c.Circuit)
                .Where(p => p.UserId == id)
                .Where(r => r.UserId == r.Student.UserId);
            //.Where(T => T.Status == ApprovalStatus.Submitted);

            var applicationStatus = await StatusQuary.ToListAsync();
            List<PendingApplicationsDto> studentStatusList = new List<PendingApplicationsDto>();
            foreach (var application in applicationStatus)
            {
                if (applicationStatus == null)
                {
                    throw new NotFoundException("You Do Not Have Any Application in History");
                }
                else
                {
                    if (application.Status == ApprovalStatus.Disbursed)
                    {
                       
                        var studentStatus = new PendingApplicationsDto
                        {
                            Id = application.UserId,
                            ApplicationId = application.Id,
                            StudentId = application.StudentId,
                            Names = $"{application.Student.SurName}, {application.Student.FirstName }  {application.Student.OtherName}",
                            AuxiliaryBody = application.Student.AuxiliaryBody,
                            CircuitName = application.Student.Jamaat.Circuit.CircuitName,
                            Jamaat = application.Student.Jamaat.JamaatName,
                            NameOfSchool = application.NameOfSchool,
                            Discipline = application.Discipline,
                            AcademicLevel = application.AcademicLevel,
                            SchoolSession = application.SchoolSession,
                            AmountRequested = application.AmountRequested,
                            AmountGranted = application.AmountRecommended,
                            GuardianFullName = application.Student.GuardianFullName,
                            GuardianPhoneNumber = application.Student.GuardianPhoneNumber,
                            Remarks = application.Remarks,
                            Status = application.Status,
                        };
                        studentStatusList.Add(studentStatus);
                    }
                }
            }
            return (studentStatusList);
        }
        public async Task<List<PendingApplicationsDto>> StudentPaymentHistory(int id)
        {
            var StatusQuary = _applicationRepository.Query()
                .Include(s => s.Student)
                .ThenInclude(j => j.Jamaat)
                .ThenInclude(c => c.Circuit)
                .Where(p => p.StudentId == id)
                .Where(r => r.Status == ApprovalStatus.Disbursed);


            var applicationStatus = await StatusQuary.ToListAsync();
            List<PendingApplicationsDto> studentStatusList = new List<PendingApplicationsDto>();
            foreach (var application in applicationStatus)
            {
                if (applicationStatus == null)
                {
                    throw new NotFoundException("No Payment History");
                }
                else
                {
                    var studentStatus = new PendingApplicationsDto
                    {
                        Id = application.UserId,
                        ApplicationId = application.Id,
                        StudentId = application.StudentId,
                        Names = $"{application.Student.SurName}, {application.Student.FirstName }  {application.Student.OtherName}",
                        AuxiliaryBody = application.Student.AuxiliaryBody,
                        CircuitName = application.Student.Jamaat.Circuit.CircuitName,
                        Jamaat = application.Student.Jamaat.JamaatName,
                        NameOfSchool = application.NameOfSchool,
                        Discipline = application.Discipline,
                        AcademicLevel = application.AcademicLevel,
                        SchoolSession = application.SchoolSession,
                        AmountRequested = application.AmountRequested,
                        AmountGranted = application.AmountRecommended,
                        GuardianFullName = application.Student.GuardianFullName,
                        GuardianPhoneNumber = application.Student.GuardianPhoneNumber,
                        Remarks = application.Remarks,
                        Status = application.Status,
                        BankAccountName = application.BankAccountName
                    };
                    studentStatusList.Add(studentStatus);
                    

                }
            }
            return (studentStatusList);
        }

        //View Pending Application To Edit
        public async Task<ApplicationResponseModel> GetApplication(int id)  
        {
            
            var applicantQuery = _applicationRepository.Query().Include(m => m.Student).Where(r => r.Id == id);

           var applicant = await applicantQuery.SingleAsync();
           
            
                ApplicationDto applicationQ = new ApplicationDto
                {
                    StudentId = applicant.StudentId,
                    InstitutionType = applicant.InstitutionType,
                    NameOfSchool = applicant.NameOfSchool,
                    AcademicLevel = applicant.AcademicLevel,
                    SchoolSession = applicant.SchoolSession,
                    Discipline = applicant.Discipline,
                    Duration = applicant.Duration,
                    DegreeInView = applicant.DegreeInView,
                    YearToGraduate = applicant.YearToGraduate,
                    DateAdmitted = applicant.DateAdmitted,
                    LetterOfAdmission = applicant.LetterOfAdmission,  //if Null return Null
                    AmountRequested = applicant.AmountRequested,
                    AmountRecommended = applicant.AmountRecommended,
                    BankName = applicant.BankName,
                    BankAccountNumber = applicant.BankAccountNumber,
                    BankAccountName = applicant.BankAccountName,
                    LastSchoolResult = applicant.LastSchoolResult, //if Null return Null
                    SchoolBill = applicant.SchoolBill,
                    Remarks = applicant.Remarks,    
                   
                };  

                 return new ApplicationResponseModel

                {
                    Data = applicationQ,
                    Status = true,
                    Message = "Successful"
                };
        } 

        public async Task<BaseResponse> UpdateApplicationAsync(int id, UpdateApplicationRequestModel model)
        {

            application application = await _applicationRepository.GetAsync(id);
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
                application.AmountRecommended = model.AmountRecommended;
                application.BankName = model.BankName;
                application.BankAccountNumber = model.BankAccountNumber;
                application.BankAccountName = model.BankAccountName;
                application.Remarks = model.Remarks;
                // application.LastSchoolResult = model.LastSchoolResult;
                //application.SchoolBill = model.SchoolBill;
                await _applicationRepository.UpdateAsync(application);
                await _applicationRepository.SaveChangesAsync();
            }

            return new BaseResponse
            {
                Status = true,
                Message = "successfully updated"
            };

        }
        public async Task<BaseResponse> UpdateApprovalStatus(int id, int userId)
        {

            var application = await _applicationRepository.GetAsync(id);

            var user = await _userRepository.GetAsync(userId);

            if (application.Status == ApprovalStatus.Submitted && user.UserType == UserType.Circuit)
            {
                application.Status = ApprovalStatus.Committee;
                await _applicationRepository.UpdateAsync(application);
                await _applicationRepository.SaveChangesAsync();

            }
            else
            if (application.Status == ApprovalStatus.Committee && user.UserType == UserType.Committee)
            {
                application.Status = ApprovalStatus.NaibAmir;
                await _applicationRepository.UpdateAsync(application);
                await _applicationRepository.SaveChangesAsync();
            }
            else
            if (application.Status == ApprovalStatus.NaibAmir && user.UserType == UserType.NaibAmir)
            {
                application.Status = ApprovalStatus.Amir;
                await _applicationRepository.UpdateAsync(application);
                await _applicationRepository.SaveChangesAsync();
            }
            else
            if (application.Status == ApprovalStatus.Amir && user.UserType == UserType.Amir)
            {
                application.Status = ApprovalStatus.Approved;
                await _applicationRepository.UpdateAsync(application);
                await _applicationRepository.SaveChangesAsync();
            }
            else
            if (application.Status == ApprovalStatus.Approved && user.UserType == UserType.Accounts)
            {
                application.Status = ApprovalStatus.Disbursed;
                await _applicationRepository.UpdateAsync(application);
                await _applicationRepository.SaveChangesAsync();
            }
            /*if (application.Status == ApprovalStatus.NaibAmir && user.UserType == UserType.Committee)
            {
                application.Status = ApprovalStatus.NaibAmir;
                await _applicationRepository.UpdateAsync(application);
                await _applicationRepository.SaveChangesAsync();
            }*/
            else
            {
                throw new BadRequestException("You Do Not Have Access Right to this Action");
            }
            return new BaseResponse
            {
                Status = true,
                Message = "The Application is Moved To The Next Level"
            };
        }
        public async Task<BaseResponse> DeclineApprovalStatus(int id)
        {

            var application = await _applicationRepository.GetAsync(id);

            application.Status = ApprovalStatus.Declined;

            await _applicationRepository.UpdateAsync(application);
            await _applicationRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "Application form Declined"
            };

        }
        public async Task<BaseResponse> CloseApplication(int id, int currentUserId)
        {
            var user = await _userRepository.GetAsync(currentUserId);

            var application = await _applicationRepository.GetAsync(id);

            if (application.Status == ApprovalStatus.Disbursed || application.Status == ApprovalStatus.Declined && (user.UserType == UserType.Committee || user.UserType == UserType.Admin))
            {
                application.Status = ApprovalStatus.Closed;
                await _applicationRepository.UpdateAsync(application);
                await _applicationRepository.SaveChangesAsync();
            }

            await _applicationRepository.UpdateAsync(application);
            await _applicationRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "Application form Closed"
            };

        }
        public async void DeleteApplication(int id)
        {

            var application = await _applicationRepository.GetAsync(id);

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
        public async Task<IList<ApplicationDto>> GetStudentApplicationAsync(string memberCode)
        {

            return await DbContext.Applications
                .Include(uc => uc.Student)
                .Where(u => u.Student.User.MemberCode == memberCode)
                .Select(uc => new ApplicationDto
                {

                }).ToListAsync();
        }
        public async Task<ApplicationResponseModel> Recommendation(int id)
        {
            var applicant = await _applicationRepository.GetAsync(id);

            ApplicationDto application = new ApplicationDto
            {

                ApplicationId = applicant.Id,
                AmountRecommended = applicant.AmountRecommended,
                Remarks = applicant.Remarks
            };
            return new ApplicationResponseModel
            {
                Data = application,
                Status = true,
                Message = "Successful"
            };
        }
        public async Task<BaseResponse> Recommendation(int id, UpdateApplicationRequestModel model)
        {

            application application = await _applicationRepository.GetAsync(id);
            {

                application.AmountRecommended = model.AmountRecommended;
                application.Remarks = model.Remarks;

                await _applicationRepository.UpdateAsync(application);
                await _applicationRepository.SaveChangesAsync();
            }

            return new BaseResponse
            {
                Status = true,
                Message = "Recommendation Saved"
            };
        }
        public async Task<BaseResponse> ResetAction(int id, int currentUserId)
        {
            var application = await _applicationRepository.GetAsync(id);

            var user = await _userRepository.GetAsync(currentUserId);

            if (application.Status == ApprovalStatus.Committee || application.Status == ApprovalStatus.Declined && user.UserType == UserType.Circuit)
            {
                application.Status = ApprovalStatus.Submitted;
                await _applicationRepository.UpdateAsync(application);
                await _applicationRepository.SaveChangesAsync();

            }
            if (!(application.Status == ApprovalStatus.Committee) && user.UserType == UserType.Committee)
            {
                application.Status = ApprovalStatus.Committee;
                await _applicationRepository.UpdateAsync(application);
                await _applicationRepository.SaveChangesAsync();

            }
            if (application.Status == ApprovalStatus.Amir || application.Status == ApprovalStatus.Declined && user.UserType == UserType.NaibAmir)
            {
                application.Status = ApprovalStatus.NaibAmir;
                await _applicationRepository.UpdateAsync(application);
                await _applicationRepository.SaveChangesAsync();

            }
            if (application.Status == ApprovalStatus.Approved || application.Status == ApprovalStatus.Declined && user.UserType == UserType.Amir)
            {
                application.Status = ApprovalStatus.Amir;
                await _applicationRepository.UpdateAsync(application);
                await _applicationRepository.SaveChangesAsync();

            }
            return new BaseResponse
            {
                Status = true,
                Message = "RESET"
            };  
        }
}   }


