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
        private readonly IApplicationFormRepository _applicationFormRepository;
        public PaymentService(IPaymentRepository paymentRepository, IApplicationFormRepository applicationFormRepository)
        {
            _paymentRepository = paymentRepository;
            _applicationFormRepository = applicationFormRepository;
        }
        public async Task<BaseResponse> CreatePaymentByApprovedApplicationAsync(PendingApplicationsDto model, int id)
        {

            /*var applicationsQuery = _applicationFormRepository.Query()
                .Include(d => d.Student)
                .Where(ap => statuses.Contains(ap.Status));
                .Where(r => r.Id == Id);

            var applications = applicationsQuery;*/

            //List<PaymentDto> paymentList = new List<PaymentDto>();
            //foreach (var application in applications)

            var Payment = new Payment
            {

                ApplicationId = model.ApplicationFormId,
                StudentId = model.StudentId,
                StudentNames =model.Names,
                AmountApproved = model.AmountRecommended,
                Status = model.Status,
                      //Date approved by the Amir
            };
                    
                   // paymentList.Add(Payment);

                        await _paymentRepository.AddAsync(Payment);
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
                throw new BadRequestException($"Candidate with this Id {model.ApplicationFormId}No has already been paid at his {model.AcademicLevel} Level");

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
                throw new BadRequestException($"Payment with MemberCode '{model.ApplicationFormId}' already exists.");
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
        public async Task<PaymentsResponseModel> GetPayments()
        {   
                var payment = await _paymentRepository.Query().Select(r => new PaymentDto

                {

                   /* AmountApprovedAndGranted = r.AmountApprovedAndGranted,
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
            
           return await DbContext.Payments
                .Include(uc => uc.ApplicationForm)
                .ThenInclude(s => s.Student)
                .Where(s => s.ApplicationForm.StudentId == studentId)
                .Select(uc => new PaymentDto
                {
                    AmountRecommended = uc.AmountRecommended,
                    ApplicationFormId = uc.ApplicationFormId,
                    ApplicationFormNumber = uc.ApplicationForm.Id,
                    AmountApprovedAndGranted = uc.AmountApprovedAndGranted,
                    memberCode = uc.ApplicationForm.Student.User.MemberCode,
                    ApprovedBy = uc.ApprovedBy,
                    DateApproved = uc.DateApproved,
                    DatePaid = uc.DatePaid,
                    FirstName = uc.ApplicationForm.Student.FirstName


                }).ToListAsync();
        }*/

    }
}
