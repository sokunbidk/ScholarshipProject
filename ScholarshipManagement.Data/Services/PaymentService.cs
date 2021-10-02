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
using ScholarshipManagement.Data.Enums;

namespace ScholarshipManagement.Data.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IUserRepository _userRepository;
        public PaymentService(IPaymentRepository paymentRepository, IApplicationRepository applicationRepository, IUserRepository userRepository)
        {
            _paymentRepository = paymentRepository;
            _applicationRepository = applicationRepository;
            _userRepository = userRepository;
        }

        public async Task<ApplicationResponseModel> GetApplication(int id)
        {
            var filterBeneficiary = await _applicationRepository.Query()
                 //var dddd = _applicationRepository.Query().SingleOrDefaultAsync(u => u.Id == id)
                 .Include(d => d.Student)
                 .SingleOrDefaultAsync(s => s.Id == id);

            var Beneficiary = filterBeneficiary;

                ApplicationDto newBeneficiary = new ApplicationDto
                {
                    ApplicationId = Beneficiary.Id,
                    StudentId = Beneficiary.Student.Id,
                    Names = $"{Beneficiary.Student.SurName},{Beneficiary.Student.FirstName} {Beneficiary.Student.OtherName}",
                    Status = Beneficiary.Status,
                    AmountRecommended = Beneficiary.AmountRecommended,
                    BankName = Beneficiary.BankName,
                    BankAccountNumber = Beneficiary.BankAccountNumber,
                    BankAccountName = Beneficiary.BankAccountName,
                    SchoolSession = Beneficiary.SchoolSession

                };
            
            return new ApplicationResponseModel
            {
               Data = newBeneficiary,
                Status = true,
                Message = "Successful"
            };

        }
        //Populate Table-Payment
        public async Task<BaseResponse> CreatePaymentByApprovedApplicationAsync(ApplicationDto model, int id,int currentUserId)
        {
            var application = await _applicationRepository.GetAsync(id);

            var user = await _userRepository.GetAsync(currentUserId);

            
            var paymentExist = await _paymentRepository.ExistsAsync(r => r.ApplicationId == id);
            if (paymentExist == true)
            {
                throw new ConflictException ("This Approval has already been Paid");
            }

            if (application.Status == ApprovalStatus.Accounts && user.UserType == UserType.Accounts)
            {
                application.Status = ApprovalStatus.Disbursed;
                await _applicationRepository.UpdateAsync(application);
                await _applicationRepository.SaveChangesAsync();
            }

            var finalBeneficiary = new Payment
            {
                ApplicationId = model.ApplicationId,
                StudentId = model.StudentId,
                StudentNames =model.Names,
                AmountApproved = model.AmountRecommended,
                Status = ApprovalStatus.Disbursed,
                BankName = model.BankName,
                BankAccountNumber = model.BankAccountNumber,
                BankAccountName = model.BankAccountName,
                DatePaid = DateTime.Today
            };
            await _paymentRepository.AddAsync(finalBeneficiary);
            await _paymentRepository.SaveChangesAsync(); 
            
             return new BaseResponse
             {
                 Status = true,
                 Message = "Role successfully created"
             };
        }
        public async Task<BaseResponse> CreatePaymentAsync(CreatePaymentRequestModel model)
        {

            //var paymentExists = await _paymentRepository.ExistsAsync(u => u.AcademicLeve == model.AcademicLevel || u.ApplicationFormId == model.ApplicationFormId);
            //if (paymentExists)
            {
                throw new BadRequestException($"Candidate with this Id {model.ApplicationId}No has already been paid at his {model.AcademicLevel} Level");

            }
            var payment = new Payment
            {
               /* AmountApprovedAndGranted = model.AmountApprovedAndGranted,
                BankName = model.BankName,
                BankAccountNumber = model.BankAccount,
                ApprovedBy = model.ApprovedBy,
                ConfirmPayment = model.ConfirmPayment,
                DateApproved = DateTime.Today,
                DatePaid = model.DatePaid,
                ProofOfChandaPmt = model.ProofOfChandaPmt,*/
            };

            await _paymentRepository.AddAsync(payment);
            await _paymentRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "Role successfully created"
            };

        }

        public async Task<BaseResponse> UpdatePaymentAsync(int id, UpdatePaymentRequestModel model)
        {
            //var paymentExists = await _paymentRepository.ExistsAsync(u => u.AcademicLeve == model.AcademicLeve && u.ApplicationFormId == model.ApplicationFormId);
            //if (paymentExists)
            {
                throw new BadRequestException($"Payment with MemberCode '{model.ApplicationId}' already exists.");
            }
            var payment = await _paymentRepository.GetAsync(id);
            if (payment == null)
            {
                throw new NotFoundException("Role does not exist");
            }

           /* payment.AmountApprovedAndGranted = model.AmountApprovedAndGranted;
            payment.BankName = model.BankName;
            payment.BankAccountNumber = model.BankAccount;
            payment.ApprovedBy = model.ApprovedBy;
            payment.ConfirmPayment = model.ConfirmPayment;
            payment.DateApproved = DateTime.Today;
            payment.DatePaid = model.DatePaid;
            payment.ProofOfChandaPmt = model.ProofOfChandaPmt;*/

            await _paymentRepository.UpdateAsync(payment);
            await _paymentRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "Role successfully updated"
            };
        }
        public async Task<PaymentsResponseModel> GetPaymentss()
        {   
                var payment = await _paymentRepository.Query().Select(r => new PaymentDto
                {
/*
                    AmountApprovedAndGranted = r.AmountApprovedAndGranted,
                    BankName = r.BankName,
                    BankAccountNumber = r.BankAccountNumber,
                    ApprovedBy = r.ApprovedBy,
                    ConfirmPayment = r.ConfirmPayment,
                    DateApproved = DateTime.Today,
                    DatePaid = r.DatePaid,
                    ProofOfChandaPmt = r.ProofOfChandaPmt,*/

                }).ToListAsync();
                return new PaymentsResponseModel

                {
                    Data = payment,
                    Status = true,
                    Message = "Successful"
                };
        }
            
        public async Task<PaymentResponseModel> GetPayment(int id)
        {
                var payment = await _paymentRepository.GetAsync(id);
                if (payment == null)
                {
                    throw new NotFoundException("Applicant does not exist");
                }
                return new PaymentResponseModel
                {
                    Data = new PaymentDto
                    {

                      /*  AmountApprovedAndGranted = payment.AmountApprovedAndGranted,
                        BankName = payment.BankName,
                        BankAccountNumber = payment.BankAccountNumber,
                        ApprovedBy = payment.ApprovedBy,
                        ConfirmPayment = payment.ConfirmPayment,
                        DateApproved = DateTime.Today,
                        DatePaid = payment.DatePaid,
                        ProofOfChandaPmt = payment.ProofOfChandaPmt,*/
                    },


                    Status = true,
                    Message = "Successful"
                };
        }
        /*public async Task<IList<PaymentDto>> GetStudentPaymentsAsync(int studentId)
        {
            
            return await DbContext
                 .Include(uc => uc.ApplicationForm)
                 .ThenInclude(s => s.Student)
                 .Where(s => s.ApplicationForm.StudentId == studentId)
                 .Select(uc => new PaymentDto
                 {
                     *//*AmountRecommended = uc.AmountRecommended,
                     ApplicationFormId = uc.ApplicationFormId,
                     ApplicationFormNumber = uc.ApplicationForm.Id,
                     AmountApprovedAndGranted = uc.AmountApprovedAndGranted,
                     memberCode = uc.ApplicationForm.Student.User.MemberCode,
                     ApprovedBy = uc.ApprovedBy,
                     DateApproved = uc.DateApproved,
                     DatePaid = uc.DatePaid,
                     FirstName = uc.ApplicationForm.Student.FirstName*//*


                 }).ToListAsync();
        }*/
        public async Task<List<PaymentDto>> GetPayments()
        {
            var paymentQuery = _paymentRepository.Query()
                                        .Include(r => r.Application)
                                        .ThenInclude(s => s.Student)
                                        .ThenInclude(j => j.Jamaat)
                                        .ThenInclude(v => v.Circuit)
                                        .Where(n =>n.Status == ApprovalStatus.Disbursed);
                                        
            var approvalPaids = await paymentQuery.ToListAsync();
            List<PaymentDto> approvalList = new List<PaymentDto>();
            foreach(var List in approvalPaids)
            {
                PaymentDto tempList = new PaymentDto
                {
                    ApplicationId = List.Id,
                    NameOfSchool = List.Application.NameOfSchool,
                    Discipline = List.Application.Discipline,
                    AcademicLevel = List.Application.AcademicLevel,
                    SchoolSession = List.Application.SchoolSession,
                    Jamaat = List.Application.Student.Jamaat.JamaatName,
                    Circuit = List.Application.Student.Jamaat.Circuit.CircuitName,
                    Names = List.StudentNames,
                    Status = List.Status,
                    AmountApproved = List.AmountApproved,
                    BankName = List.BankName,
                    BankAccountNumber = List.BankAccountNumber,
                    BankAccountName = List.BankAccountName,
                    DatePaid = List.DatePaid

                };
                 approvalList.Add(tempList);
            }
            return (approvalList);
        }
    }
}
