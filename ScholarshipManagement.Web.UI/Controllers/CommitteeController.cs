using Microsoft.AspNetCore.Mvc;
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
                case UserType.Committee:
                    status = new List<ApprovalStatus>() { ApprovalStatus.Committee };
                    break;
            }
            var pendingApplications = await _applicationService.PendingApplicationsByStatus(status, isGlobal, circuitIds, userDto.Id);

            return View(pendingApplications);

        }

    }
    
}
