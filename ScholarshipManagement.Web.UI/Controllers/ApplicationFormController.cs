﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ScholarshipManagement.Data;
using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Enums;
using ScholarshipManagement.Data.Interfaces;
using ScholarshipManagement.Data.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ScholarshipManagement.Web.UI.Controllers
{
    
    public class ApplicationFormController : Controller
    {
        private readonly IApplicationService _applicationService;
        private readonly IStudentService _studentService;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _env;
        private readonly IApplicationFormRepository _applicationFormRepository;
        

        public ApplicationFormController(IApplicationService applicationService,IStudentService studentService,IUserService userService, IWebHostEnvironment env, IApplicationFormRepository applicationFormRepository)
        {
            _applicationService = applicationService;
            _studentService = studentService;
            _userService = userService;
            _env = env;
            _applicationFormRepository = applicationFormRepository;
        }
        
     
        public IActionResult CreateApplication() //Blank Application
        {
            return View();
        }

        public IActionResult CreateApplicationNewStudent() //Blank Application
        {
            return View();
        }
        //Returning Candidate Application
        [HttpPost]
        public IActionResult CreateApplication(CreateApplicationFormRequestModel model)
        {

            Random random = new Random();
            var currentUser = User.FindFirst("Email").Value;
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var files = HttpContext.Request.Form.Files;


            string admissionLetterupload = _env.WebRootPath + @"\UploadedFiles\AdmissionLetter\";
            string schBillupload = _env.WebRootPath + @"\UploadedFiles\SchBill\";
            string schResultupload = _env.WebRootPath + @"\UploadedFiles\SchResult\";


            string admissionLetterfileName = currentUserId + "-admissionLetter-" + random.Next(100000).ToString();

            string schBillfileName = currentUserId + "-schBill-" + random.Next(100000).ToString();

            string schResultfileName = currentUserId + "-schResult-" + random.Next(100000).ToString();


            string admissionLetterExtension = Path.GetExtension(files[0].FileName);
            string schBillExtension = Path.GetExtension(files[1].FileName);
            string schResultExtension = Path.GetExtension(files[2].FileName);


            using (var fileStream = new FileStream(Path.Combine(admissionLetterupload, admissionLetterfileName + admissionLetterExtension), FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }

            using (var fileStream = new FileStream(Path.Combine(schBillupload, schBillfileName + schBillExtension), FileMode.Create))
            {
                files[1].CopyTo(fileStream);
            }

            using (var fileStream = new FileStream(Path.Combine(schResultupload, schResultfileName + schResultExtension), FileMode.Create))
            {
                files[2].CopyTo(fileStream);
            }


            model.LetterOfAdmission = admissionLetterfileName + admissionLetterExtension;
            model.SchoolBill = schBillfileName + schBillExtension;
            model.LastSchoolResult = schResultfileName + schResultExtension;

            _applicationService.CreateApplicationAsync(model, currentUser);

            return RedirectToAction("DashboardReturningStudent", "Student");
        }
        //New Candidate Application
        [HttpPost]
        public IActionResult CreateApplicationNewStudent(CreateApplicationFormRequestModel model)
        {
            Random random = new Random();
            var currentUser = User.FindFirst("Email").Value;
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;


            var files = HttpContext.Request.Form.Files;


            string admissionLetterupload = _env.WebRootPath + @"\UploadedFiles\AdmissionLetter\";
            string schBillupload = _env.WebRootPath + @"\UploadedFiles\SchBill\";
            string schResultupload = _env.WebRootPath + @"\UploadedFiles\SchResult\";


            string admissionLetterfileName = currentUserId + "-admissionLetter-" + random.Next(100000).ToString();

            string schBillfileName = currentUserId + "-schBill-" + random.Next(100000).ToString();

            string schResultfileName = currentUserId + "-schResult-" + random.Next(100000).ToString();


            string admissionLetterExtension = Path.GetExtension(files[0].FileName);
            string schBillExtension = Path.GetExtension(files[1].FileName);
            string schResultExtension = Path.GetExtension(files[2].FileName);


            using (var fileStream = new FileStream(Path.Combine(admissionLetterupload, admissionLetterfileName + admissionLetterExtension), FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }

            using (var fileStream = new FileStream(Path.Combine(schBillupload, schBillfileName + schBillExtension), FileMode.Create))
            {
                files[1].CopyTo(fileStream);
            }

            using (var fileStream = new FileStream(Path.Combine(schResultupload, schResultfileName + schResultExtension), FileMode.Create))
            {
                files[2].CopyTo(fileStream);
            }


            model.LetterOfAdmission = admissionLetterfileName + admissionLetterExtension;
            model.SchoolBill = schBillfileName + schBillExtension;
            model.LastSchoolResult = schResultfileName + schResultExtension;



            _applicationService.CreateNewApplicationAsync(model, currentUser);

            return RedirectToAction("DashboardReturningStudent", "Student");
        }

        
        //Pending Applications
        public async Task<IActionResult> PendingApplications()
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userResponseModel = await _userService.GetUser(currentUserId);

            var userDto = userResponseModel.Data;

            List<ApprovalStatus> status = new List<ApprovalStatus>() { ApprovalStatus.Default };
            var isGlobal = true;
            List<int> circuitIds = null;
            switch (userDto.UserType)
            {
                case UserType.Circuit:
                    status =new List<ApprovalStatus>() { ApprovalStatus.Default };
                    isGlobal = false;
                    circuitIds = new List<int>();
                    var circuit = await _userService.GetUserCircuit(userDto.Id);
                    if(circuit != null)
                    {
                        circuitIds.Add(circuit.Id);
                    }
                    break;
                case UserType.Committee:
                    status = new List<ApprovalStatus>() { ApprovalStatus.NaibAmir, ApprovalStatus.Amir, ApprovalStatus.Accounts, ApprovalStatus.Committee };
                    break;
                case UserType.NaibAmir:
                    status = new List<ApprovalStatus>() { ApprovalStatus.NaibAmir };
                    break;
                case UserType.Amir:
                    status = new List<ApprovalStatus>() { ApprovalStatus.Amir };
                    break;
                case UserType.Accounts:
                    status = new List<ApprovalStatus>() { ApprovalStatus.Accounts };
                    break;
                
            }


            var pendingApplications = await _applicationService.PendingApplicationsByStatus(status,isGlobal,circuitIds);

            return View(pendingApplications);
        }
        //updates Application Status
        [HttpGet]
        public IActionResult UpdateApprovalStatus(int id)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            _applicationService.UpdateApprovalStatus(id, currentUserId);
            return RedirectToAction("PendingApplications");
        }
        //updates Application Status
        [HttpGet]
        public IActionResult DeclineApprovalStatus(int id)
        {
            
            _applicationService.DeclineApprovalStatus(id);
            return RedirectToAction("PendingApplications");
        }
        [HttpGet]
        public async Task<IActionResult> PendingApplicationsDetail(int id)
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
        }

        [HttpPost]
        public IActionResult EditApplication(UpdateApplicationRequestModel model)
        {
            var students = _applicationService.UpdateAsync(model);

            return View(students);

        }
        [HttpGet]
        public IActionResult EditApplication(int applicationFormNumber)
        {
            var applications = _applicationService.GetApplicationFormAsync(applicationFormNumber);

            return View(applications);
        }
        
    }
}
