using Microsoft.AspNetCore.Mvc;
using ScholarshipManagement.Data;
using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Enums;
using ScholarshipManagement.Data.Interfaces;
using ScholarshipManagement.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ScholarshipManagement.Web.UI.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IApplicationService _applicationService;
        private readonly IApplicationRepository _applicationFormRepository;
        private readonly IStudentService _studentService;
        private readonly IPaymentService _paymentService;
        public PaymentController(IUserService userService, IUserRepository userRepository, IApplicationService applicationService, IApplicationRepository applicationFormRepository, IStudentService studentService, IPaymentService paymentService)
        {
            _userRepository = userRepository;
            _userService = userService;
            _applicationService = applicationService;
            _applicationFormRepository = applicationFormRepository;
            _studentService = studentService;
            _paymentService = paymentService;
        }
        public IActionResult PaymentDashboard()
        {
            return View();
        }
       
        //Get list of Approved Applications
        public async Task<IActionResult> ApprovedApplications()
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            //var user = await _userRepository.GetAsync(currentUserId);
            var userResponseModel = await _userService.GetUser(currentUserId);

            var userDto = userResponseModel.Data;


            List<ApprovalStatus> status = new List<ApprovalStatus>() { ApprovalStatus.Submitted };
            var isGlobal = true;
            List<int> circuitIds = null;


            if (userDto.UserType == UserType.Accounts)
            {
                status = new List<ApprovalStatus>()
                {
                    ApprovalStatus.Approved,
                   // ApprovalStatus.Disbursed
                };
            }

            var ApprovedApplications = await _applicationService.PendingApplicationsByStatus(status, isGlobal, circuitIds, userDto.Id);

            return View(ApprovedApplications);

        }
        //Fetch a particular Approved application to update/Pay
        [HttpGet]
        public async Task<IActionResult> MakePayment(int id)
        {
            try
            {
                ApplicationResponseModel response = await _paymentService.GetApplication(id);

                var approvedApplication = response.Data;

                return View(approvedApplication);
            }
            
            catch(Exception e)
            {

                ViewBag.Message = e.Message;
                return View();
            }
            
        }

        //Incert to Payment Table
        [HttpPost]
        public async Task<IActionResult> MakePayment(ApplicationDto model, int id)
        {
            try
            {
                var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                //var user = await _userRepository.GetAsync(currentUserId);

                await _paymentService.CreatePaymentByApprovedApplicationAsync(model, id,currentUserId);

            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View();

            }
            return RedirectToAction("ApprovedApplications");

        }
        public async Task<IActionResult> DisbursedList()
        {
            try
            {
                var pastPayment = await _paymentService.GetPayments();
                return View(pastPayment);
            }
            catch(Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            }
        }
    }
    
}
