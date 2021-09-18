using Microsoft.AspNetCore.Mvc;
using ScholarshipManagement.Data;
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
    public class CommitteeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IApplicationService _applicationService;
        private readonly IApplicationFormRepository _applicationFormRepository;
        private readonly IStudentService _studentService;
        public CommitteeController(IUserService userService, IUserRepository userRepository, IApplicationService applicationService, IApplicationFormRepository applicationFormRepository, IStudentService studentService)
        {
            _userRepository = userRepository;
            _userService = userService;
            _applicationService = applicationService;
            _applicationFormRepository = applicationFormRepository;
            _studentService = studentService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CommitteeDashboard()
        {
            return View();
        }
        public async Task<IActionResult> PendingApplicationsFocus()
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);


            var userResponseModel = await _userService.GetUser(currentUserId);

            var userDto = userResponseModel.Data;


            List<ApprovalStatus> status = new List<ApprovalStatus>() { ApprovalStatus.Submitted };
            var isGlobal = true;
            List<int> circuitIds = null;
            if (userDto.UserType == UserType.Committee)

            {

                status = new List<ApprovalStatus>() { ApprovalStatus.Committee };

            }
           

            var pendingApplications = await _applicationService.PendingApplicationsByStatus(status, isGlobal, circuitIds, userDto.Id);

            return View(pendingApplications);

        }
        //updates Application Status
        [HttpGet]
        public IActionResult UpdateApprovalStatus(int id)
        {
            try
            {
                var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                _applicationService.UpdateApprovalStatus(id, currentUserId);
                return RedirectToAction("PendingApplicationsFocus");
            }

            catch (Exception e)
            {
                ViewBag.Message = e.Message;

                return View();
            }
            ViewBag.Message = "Delivered To the Next Level";


        }
        //updates Application Status-Decline
        [HttpGet]
        public IActionResult DeclineApprovalStatus(int id)
        {
            try
            {
                _applicationService.DeclineApprovalStatus(id);
                return RedirectToAction("PendingApplications");
            }
            catch (Exception e)
            {
                ViewBag.Message -= "Cannot Declined!";
            }
            return ViewBag.Message -= "Application Declined!";



        }
        [HttpGet]
       /* public async Task<IActionResult> PendingApplicationsDetail(int id)
        {

            ApplicationResponseModel response = await _applicationService.GetApplication(id);

            var pendingApplication = response.Data;

            return View(pendingApplication);
        }
        public async Task<IActionResult> PendingStudentsDetail(int id)
        {

            var response = await _studentService.GetApplicantById(id);

            var pendingStudent = response.Data;

            return View(pendingStudent);
        }*/
        //View Student Profile To Edit
        [HttpGet]
        public async Task<IActionResult> UpdatePendingStudent(int id)
        {

            var response = await _studentService.GetApplicantById(id);

            var pendingStudent = response.Data;

            return View(pendingStudent);
        }
        [HttpPost]
        //Edit Student Profile
        public IActionResult UpdatePendingStudent(int id, UpdateStudentRequestModel model)
        {
            try
            {
                _studentService.UpdateStudentAsync(id, model);
                return RedirectToAction("UpdatePendingStudent");

            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            }
            

        }
        //View Application To Edit
        [HttpGet]
        public async Task<IActionResult> UpdatePendingApplication(int id)
        {

            ApplicationResponseModel response = await _applicationService.GetApplication(id);

            var pendingApplication = response.Data;

            return View(pendingApplication);
        }
        //Edit Applicaation
        [HttpPost]
        public IActionResult UpdatePendingApplication(int id, UpdateApplicationRequestModel model)
        {
            try
            {
                _applicationService.UpdateApplicationAsync(id, model);
                return RedirectToAction("UpdatePendingApplication");
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            }


        }
        public async Task<IActionResult> StudentPaymentHistory(int id)
        {
            try
            {
              
                var applicationStatus = await _applicationService.StudentPaymentHistory(id);
                return View(applicationStatus);
            }
            catch (Exception e)
            {

                ViewBag.Message = e.Message;
            }
            return View(ViewBag.Message);
        }
    }
}
