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
        private readonly IApplicationRepository _applicationFormRepository;
        private readonly IStudentService _studentService;
        public AdminController(IUserService userService, IUserRepository userRepository, IApplicationService applicationService, IApplicationRepository applicationFormRepository,IStudentService studentService)
        {
            _userRepository = userRepository;
            _userService = userService;
            _applicationService = applicationService;
            _applicationFormRepository = applicationFormRepository;
            _studentService = studentService;
        }
        [HttpGet]
        /*public IActionResult Index()
        {
            var fullList = _userService.GetUser();
            return View(fullList);
        }*/
        public IActionResult AdminDashboard()
        {
            return View();
        }
        //Return List of Users
        public async Task<IActionResult> FetchUsers()
        {
            List<UserDto> users = await _userService.GetUser();
            return View(users);
        }
        //Get User To be Update
        [HttpGet]
        public async Task<IActionResult> UpdateUser(int id)
        {
            try
            {   
                try
                {
                    UserResponseModel user = await _userService.GetUser(id);
                    UserDto userDto = user.Data;
                   return View(userDto);
                }
                catch (Exception e){ViewBag.Message = e.Message; return View(); }

            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            }
            
        }
        //Update
        [HttpPost]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserRequestModel model)
        {
            try
            {
               BaseResponse userUpdate = await _userService.UpdateUserAsync(id, model);
                ViewBag.Message = userUpdate.Message;
                return RedirectToAction("FetchUsers");
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            }
            //System.Windows.Forms.MessageBox.Show("I am Testing this mbox");
        }
        public  IActionResult DeleteUser(int id)
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
        }
        public async Task<IActionResult> PendingApplicationsEdit()
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userResponseModel = await _userService.GetUser(currentUserId);

            var userDto = userResponseModel.Data;


            List<ApprovalStatus> status = new List<ApprovalStatus>() { ApprovalStatus.Submitted }; //Assign Default to status automatically
            var isGlobal = true;
            List<int> circuitIds = null;

            if (userDto.UserType == UserType.Admin)

            {
                status = new List<ApprovalStatus>() { ApprovalStatus.NaibAmir, ApprovalStatus.Amir, ApprovalStatus.Accounts, ApprovalStatus.Committee, ApprovalStatus.Submitted, ApprovalStatus.Disbursed };
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

            var response = await _applicationService.GetApplication(id);
            var application = response.Data;
            
            return View(application);
        }
        //Edit Application
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

