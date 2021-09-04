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
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IApplicationService _applicationService;
        private readonly IApplicationFormRepository _applicationFormRepository;
        private readonly IStudentService _studentService;
        public AdminController(IUserService userService, IUserRepository userRepository, IApplicationService applicationService, IApplicationFormRepository applicationFormRepository,IStudentService studentService)
        {
            _userRepository = userRepository;
            _userService = userService;
            _applicationService = applicationService;
            _applicationFormRepository = applicationFormRepository;
            _studentService = studentService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var fullList = _userService.GetUser();

            return View(fullList);
        }
        public IActionResult AdminDashboard()
        {

            return View();
        }
        //Return
        public async Task<IActionResult> FetchUsers()
        {

            List<UserDto> users = await _userService.GetUser();
            return View(users);
        }
        //Get User To be Update
        [HttpGet]
        public async Task<IActionResult> UpdateUser(int id)
        {

            UserResponseModel user = await _userService.GetUser(id);

            UserDto userDto = user.Data;

            return View(userDto);
        }
        //Update
        [HttpPost]
        public IActionResult UpdateUser(int id, UpdateUserRequestModel model)
        {
            try
            {
                _userService.UpdateUserAsync(id, model);
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            }
            ViewBag.Message = "Updated Successfully";

            //System.Windows.Forms.MessageBox.Show("I am Testing this mbox");

        }
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userService.DeleteUser(id);

                return RedirectToAction("FetchUsers");
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            }
            ViewBag.Message = "Delete";

        }
        public async Task<IActionResult> PendingApplicationsEdit()
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userResponseModel = await _userService.GetUser(currentUserId);

            var userDto = userResponseModel.Data;


            List<ApprovalStatus> status = new List<ApprovalStatus>() { ApprovalStatus.Draft }; //Assign Default to status automatically
            var isGlobal = true;
            List<int> circuitIds = null;
            switch (userDto.UserType)
            {
                case UserType.Circuit:

                    status = new List<ApprovalStatus>() { ApprovalStatus.Draft };
                    isGlobal = false;
                    circuitIds = new List<int>();
                    var circuit = await _userService.GetUserCircuit(userDto.Id); //find Circuit,criteria user id
                    if (circuit != null)
                    {
                        circuitIds.Add(circuit.Id);
                    }
                    break;
                case UserType.Admin:
                    status = new List<ApprovalStatus>() { ApprovalStatus.NaibAmir, ApprovalStatus.Amir, ApprovalStatus.Accounts, ApprovalStatus.Committee, ApprovalStatus.Draft, ApprovalStatus.Disbursed };
                    break;
            }
            var pendingApplications = await _applicationService.PendingApplicationsByStatus(status, isGlobal, circuitIds, userDto.Id);

            return View(pendingApplications);

        }
        
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
            //System.Windows.Forms.MessageBox.Show("I am Testing this mbox");

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
        public IActionResult DeleteApplication(int id)
        {
            try
            {
                _applicationService.DeleteApplication(id);

                return RedirectToAction("PendingApplicationsEdit");
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            }
            

        }


    }
}

