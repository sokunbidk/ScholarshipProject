using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using ScholarshipManagement.Data;
using ScholarshipManagement.Data.Enums;
using ScholarshipManagement.Data.Services;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using ScholarshipManagement.Data.Entities;
using System.Threading.Tasks;
using System.Security.Claims;
using System;
using System.Linq;
using ScholarshipManagement.Data.DTOs;

namespace ScholarshipManagement.Web.UI.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private IWebHostEnvironment _env;
        private readonly ICircuitService _circuitService;
        private readonly IJamaatService _jamaatService;
        private readonly IUserService _userService; 
        private readonly IApplicationService _applicationService;



        public StudentController(IStudentService studentService, IWebHostEnvironment env, ICircuitService circuitService, IJamaatService jamaatService,IUserService userService,IApplicationService applicationService)
        {
            _studentService = studentService;
            _env = env;
            _circuitService = circuitService;
            _jamaatService = jamaatService;
            _userService = userService;
            _applicationService = applicationService;
        }

        public IActionResult Dashboard()
        {
            var currentUser = User.Identity.Name;
            ViewBag.Message = $"Assalam Alaikum:{currentUser}";
            return View();
        }
        public IActionResult DashboardReturningStudent()
        {
            var currentUser = User.Identity.Name;
            ViewBag.Message = $"Assalam Alaikum:{currentUser}";
            return View();
        }
        
        //Blank Registration Form New Candidate with dropdown for circuit/jamaat
        [HttpGet]
        public IActionResult NewCandidate()
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
            return View();
        }
        //Register New Student Method
        [HttpPost]
        public async Task<IActionResult> NewCandidate(CreateStudentRequestModel model)
        {
            try
            {
                Random random = new Random();
                var currentUser = User.FindFirst("Email").Value;
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var files = HttpContext.Request.Form.Files;


                string upload = _env.WebRootPath + @"\UploadedFiles\Photograph\";
                //string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(files[0].FileName);

                string fileName = currentUserId + "-Photograph-" + random.Next(100000).ToString();

                using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    
                {
                    files[0].CopyTo(fileStream);
                }
                //ViewBag.Message = "Photo Uploaded Successfully";
                model.Photograph = fileName + extension;

                await _studentService.CreateStudentAsync(model, currentUser);
                
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;

            }
            ViewBag.Message = "Submitted.Click Next To Apply for Scholarship";
            return View();
            
        }

        //View Profile-Returning Candidate
        [HttpGet]
        public async Task<IActionResult> ReturningCandidateRegView()
        {
            try
            {
                var currentUser = User.FindFirst("Email").Value;

                var student = await _studentService.GetStudentReturningCandidate(currentUser);

                return View(student);
            }
            catch (Exception e)
            {

                ViewBag.Message = "Student does not exist";
            }
            ViewBag.Message = "View Student Profile";
            return View();


        }
        public async Task<IActionResult> StudentApplicationStatus()
        {
            /*var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userResponseModel = await _userService.GetUser(currentUserId);

            var userDto = userResponseModel.Data;


            List<ApprovalStatus> status = new List<ApprovalStatus>() { ApprovalStatus.Draft }; 
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
                case UserType.Student:
                    status = new List<ApprovalStatus>() {ApprovalStatus.NaibAmir, ApprovalStatus.Amir, ApprovalStatus.Accounts, ApprovalStatus.Committee, ApprovalStatus.Draft, ApprovalStatus.Disbursed};
                    break;
            }
            var pendingApplications = await _applicationService.PendingApplicationsByStatus(status, isGlobal, circuitIds, currentUserId);

            //Student StatusQuery = (Student)pendingApplications.Where(p => currentUserId.Contains(p.Id));

            //Student StatusQuery = (Student)pendingApplications.Where(p => p.Id == currentUserId);

            return View(StatusQuery);*/

            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userResponseModel = await _userService.GetUser(currentUserId);

            var userDto = userResponseModel.Data;

            //List<PendingApplicationsDto> applicationStatus = await _applicationService.StudentApplicationStatus();
            var applicationStatus = await _applicationService.StudentApplicationStatus(currentUserId);

            return View(applicationStatus);
        }
        


    }
}
