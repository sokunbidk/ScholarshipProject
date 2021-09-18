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
    public class NaibAmirController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IApplicationService _applicationService;
        private readonly IApplicationFormRepository _applicationFormRepository;
        private readonly IStudentService _studentService;
        

        public NaibAmirController(IUserService userService, IUserRepository userRepository, IApplicationService applicationService, IApplicationFormRepository applicationFormRepository, IStudentService studentService)
        {
            _userRepository = userRepository;
            _userService = userService;
            _applicationService = applicationService;
            _applicationFormRepository = applicationFormRepository;
            _studentService = studentService;
        }
        public IActionResult Dashboard_NaibAmir()
        {
            return View();
        }
        public IActionResult UpdatePendingApplication()
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
           
            if (userDto.UserType == UserType.NaibAmir)

            {

                status = new List<ApprovalStatus>() 
                { 
                    ApprovalStatus.NaibAmir, 
                    ApprovalStatus.Amir 
                };

            }

            var pendingApplications = await _applicationService.PendingApplicationsByStatus(status, isGlobal, circuitIds, userDto.Id);

            return View(pendingApplications);

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
        public async Task<IActionResult> PendingStudentsDetail(int id)
        {

            var response = await _studentService.GetApplicantById(id);

            var pendingStudent = response.Data;

            return View(pendingStudent);
        }
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
    }
    
}
