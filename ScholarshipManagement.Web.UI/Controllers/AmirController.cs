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
    public class AmirController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IApplicationService _applicationService;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IStudentService _studentService;
        private readonly ICircuitService _circuitService;
        private readonly IJamaatService _jamaatService;
        public AmirController(IUserService userService, IUserRepository userRepository, IApplicationService applicationService, IApplicationRepository applicationFormRepository, IStudentService studentService, ICircuitService circuitService, IJamaatService jamaatService)
        {
            _userRepository = userRepository;
            _userService = userService;
            _applicationService = applicationService;
            _applicationRepository = applicationFormRepository;
            _studentService = studentService;
            _circuitService = circuitService;
            _jamaatService = jamaatService;
        }
        public IActionResult Dashboard_Amir()
        {
            return View();
        }
       /* public IActionResult UpdatePendingApplication()
        {
            return View();
        }*/
        public async Task<IActionResult> PendingApplicationsFocus()
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userResponseModel = await _userService.GetUser(currentUserId);

            var userDto = userResponseModel.Data;


            List<ApprovalStatus> status = new List<ApprovalStatus>() { ApprovalStatus.Submitted };
            var isGlobal = true;
            List<int> circuitIds = null;
            
             if (userDto.UserType == UserType.Amir)
             {
                status = new List<ApprovalStatus>()
                {
                    ApprovalStatus.Amir,
                    ApprovalStatus.Approved,
                    ApprovalStatus.Declined
                }; 
             }
            var pendingApplications = await _applicationService.PendingApplicationsByStatus(status, isGlobal, circuitIds, userDto.Id);

            return View(pendingApplications);

        }
       
       
        
       
        public async Task<IActionResult> PendingStudentsDetail(int id)
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

            var response = await _studentService.GetApplicantById(id);

            var pendingStudent = response.Data;

            return View(pendingStudent);
        }
        public async Task<IActionResult> PendingApplicationsDetail(int id)
        {
            try
            {
                try
                {
                    ApplicationResponseModel response = await _applicationService.GetApplication(id);
                    var ApplicationDetails = response.Data;

                    return View(ApplicationDetails);
                    // return View(response);
                }
                catch (Exception e) { ViewBag.Message = e.Message; return View(); }

            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            }


        }
        //updates Application Status
        [HttpGet]
        public IActionResult UpdateApprovalStatus(int id)
        {
            try
            {
                var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                var status = _applicationService.UpdateApprovalStatus(id, currentUserId);
                ViewBag.Message = status.Exception;
                return RedirectToAction("PendingApplicationsFocus");

            }

            catch (Exception e)
            {
                ViewBag.Message = e.Message;

                //return View();
                return RedirectToAction("PendingApplicationsFocus");
            }

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
                ViewBag.Message = e.Message;
                return View();
            }
            //return ViewBag.Message = "Application Declined!";



        }
        public IActionResult ResetAction(int id)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            _applicationService.ResetAction(id, currentUserId);

            return RedirectToAction("PendingApplicationsFocus");
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
    }
   
}
