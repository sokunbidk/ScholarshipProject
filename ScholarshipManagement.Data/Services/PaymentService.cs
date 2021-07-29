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

namespace ScholarshipManagement.Data.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        public async Task<BaseResponse> CreatePaymentAsync(CreatePaymentRequestModel model)
        {

            var paymentExists = await _paymentRepository.ExistsAsync(u => u.AcademicLeve == model.AcademicLevel || u.ApplicationFormId == model.ApplicationFormId);
            if (paymentExists)
            {
                throw new BadRequestException($"Candidate with this Id {model.ApplicationFormId}No has already been paid at his {model.AcademicLevel} Level");

            }
            var payment = new Payment
            {
                AmountApprovedAndGranted = model.AmountApprovedAndGranted,
                BankName = model.BankName,
                BankAccountNumber = model.BankAccount,
                ApprovedBy = model.ApprovedBy,
                ConfirmPayment = model.ConfirmPayment,
                DateApproved = DateTime.Today,
                DatePaid = model.DatePaid,
                ProofOfChandaPmt = model.ProofOfChandaPmt,
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
            var paymentExists = await _paymentRepository.ExistsAsync(u => u.AcademicLeve == model.AcademicLeve && u.ApplicationFormId == model.ApplicationFormId);
            if (paymentExists)
            {
                throw new BadRequestException($"Payment with MemberCode '{model.ApplicationFormId}' already exists.");
            }
            var payment = await _paymentRepository.GetAsync(id);
            if (payment == null)
            {
                throw new NotFoundException("Role does not exist");
            }

            payment.AmountApprovedAndGranted = model.AmountApprovedAndGranted;
            payment.BankName = model.BankName;
            payment.BankAccountNumber = model.BankAccount;
            payment.ApprovedBy = model.ApprovedBy;
            payment.ConfirmPayment = model.ConfirmPayment;
            payment.DateApproved = DateTime.Today;
            payment.DatePaid = model.DatePaid;
            payment.ProofOfChandaPmt = model.ProofOfChandaPmt;

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

                    AmountApprovedAndGranted = r.AmountApprovedAndGranted,
                    BankName = r.BankName,
                    BankAccountNumber = r.BankAccountNumber,
                    ApprovedBy = r.ApprovedBy,
                    ConfirmPayment = r.ConfirmPayment,
                    DateApproved = DateTime.Today,
                    DatePaid = r.DatePaid,
                    ProofOfChandaPmt = r.ProofOfChandaPmt,

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

                        AmountApprovedAndGranted = payment.AmountApprovedAndGranted,
                        BankName = payment.BankName,
                        BankAccountNumber = payment.BankAccountNumber,
                        ApprovedBy = payment.ApprovedBy,
                        ConfirmPayment = payment.ConfirmPayment,
                        DateApproved = DateTime.Today,
                        DatePaid = payment.DatePaid,
                        ProofOfChandaPmt = payment.ProofOfChandaPmt,
                    },


                    Status = true,
                    Message = "Successful"
                };
        }

    }
}
