using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly IApplicationRepository _applicationFormRepository;
        private readonly IStudentService _studentService;
        private readonly IJamaatService _jamaatService;
        private readonly ICircuitService _circuitService;
        public CommitteeController(IUserService userService, IUserRepository userRepository, IApplicationService applicationService, IApplicationRepository applicationFormRepository, IStudentService studentService,IJamaatService jamaatService,ICircuitService circuitService)
        {
            _userRepository = userRepository;
            _userService = userService;
            _applicationService = applicationService;
            _applicationFormRepository = applicationFormRepository;
            _studentService = studentService;
            _jamaatService = jamaatService;
            _circuitService = circuitService;
            
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CommitteeDashboard()
        {
            return View();
        }
       /* public IActionResult ActionRoom()
        {
            return View();
        }*/
        public async Task<IActionResult> PendingApplicationsFocus()
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);


            var userResponseModel = await _userService.GetUser(currentUserId);

            var userDto = userResponseModel.Data;


            List<ApprovalStatus> status = new List<ApprovalStatus>() ;
            var isGlobal = true;
            List<int> circuitIds = null;
            if (userDto.UserType == UserType.Committee)

            {

                status = new List<ApprovalStatus>() { ApprovalStatus.Committee };

            }           
            var pendingApplications = await _applicationService.PendingApplicationsByStatus(status, isGlobal, circuitIds, userDto.Id);

            return View(pendingApplications);
        }
        public async Task<IActionResult> PendingApplicationsInProgress()
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);


            var userResponseModel = await _userService.GetUser(currentUserId);

            var userDto = userResponseModel.Data;


            List<ApprovalStatus> status = new List<ApprovalStatus>(); 
            var isGlobal = true;
            List<int> circuitIds = null;
            if (userDto.UserType == UserType.Committee)

            {

                status = new List<ApprovalStatus>(){ ApprovalStatus.NaibAmir, ApprovalStatus.Amir, ApprovalStatus.Accounts, ApprovalStatus.Disbursed,ApprovalStatus.Approved ,ApprovalStatus.Declined};
            

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
            
            // This Action has been subsumed in "ActionRoom-POST" line 213. This is no longer usefull.

        }
        //updates Application Status-Decline
        [HttpGet]
        public IActionResult DeclineApprovalStatus(int id)
        {
            try
            {
                _applicationService.DeclineApprovalStatus(id);
                return RedirectToAction("PendingApplicationsFocus");
            }
            catch (Exception e)
            {
                ViewBag.Message = "Cannot Declined!";
            }
            return ViewBag.Message = "Application Declined!";

        }
        public IActionResult CloseApplication (int id)
        {
            try
            {
                var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                _applicationService.CloseApplication(id, currentUserId);
                return RedirectToAction("PendingApplicationsInProgress");

            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;

                return View();
            }

            // This Action has been subsumed in "ActionRoom-POST" line 213. This is no longer usefull.

        }
        [HttpGet]
      
        //View Student Profile To Edit
        [HttpGet]
        public async Task<IActionResult> UpdatePendingStudent(int id)
        {
            List<Circuit> circuits = _circuitService.GetCircuitList();
            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (Circuit circuit in circuits)
            {
                SelectListItem item = new SelectListItem(circuit.CircuitName, circuit.Id.ToString());
                listItems.Add(item);
            }
            ViewBag.Circuits = listItems;

            List<Jamaat> jamaats = _jamaatService.GetJamaatList();
            List<SelectListItem> jamaatList = new List<SelectListItem>();
            foreach (Jamaat jamaat in jamaats)
            {
                SelectListItem item = new SelectListItem(jamaat.JamaatName, jamaat.Id.ToString());
                jamaatList.Add(item);
            }
            ViewBag.Jamaats = jamaatList;
            //return View();

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

            var response = await _applicationService.GetApplication(id);
            var pendingApplication = response.Data;

            return View(pendingApplication);
            //return View(response);
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
        [HttpGet]
        public async Task<IActionResult> ActionRoom(int id)
        {

            ApplicationResponseModel response = await _applicationService.Recommendation(id);
            var ApplicationForAction = response.Data;

            
            return View(ApplicationForAction);
        }
        [HttpPost]
        public async Task<IActionResult> ActionRoom(int id, UpdateApplicationRequestModel model)
        {
            try
            {
                BaseResponse Remarks = await _applicationService.Recommendation(id, model);

                var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                await _applicationService.UpdateApprovalStatus(id, currentUserId);

                
                return RedirectToAction("PendingApplicationsFocus");
                /*ViewBag.Message = Remarks.Message;
                return View();*/
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            }
        }
        public IActionResult ResetAction(int id)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            _applicationService.ResetAction(id, currentUserId);

            return RedirectToAction("PendingApplicationsFocus");
        }
    }
}
