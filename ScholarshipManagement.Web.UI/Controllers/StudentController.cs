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
using ScholarshipManagement.Data.Exceptions;

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
        
        //Blank Registration Form New Candidate
        [HttpGet]
        public IActionResult NewCandidate()
        {
            return View();
        }
        //Register New Student 
        [HttpPost]
        public async Task<IActionResult> NewCandidate(CreateStudentRequestModel model)
        {
            try
            {
                try
                {
                    Random random = new Random();
                    var currentUserEmail = User.FindFirst("Email").Value;
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

                    BaseResponse newStudent = await _studentService.CreateStudentAsync(model, currentUserEmail);
                    ViewBag.Message = newStudent.Message;
                }
                catch (Exception e) { ViewBag.Message = e.Message; return View(); } 
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View();

            }
            return View();
            //return RedirectToAction("NewCandidate");
  
        }

        //View Profile-Returning Candidate
        [HttpGet]
        public async Task<IActionResult> ReturningCandidateRegView()
        {
            try
            {
                var currentUser = User.FindFirst("Email").Value;

                var ReturningCandidate = await _studentService.GetStudentReturningCandidate(currentUser);

                return View(ReturningCandidate);
            }
            catch (Exception e)
            {

                ViewBag.Message = ViewBag.Message = e.Message;
                return View();
            }
        }
        public async Task<IActionResult> StudentApplicationStatus()
        {
            try
            {
                var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                var userResponseModel = await _userService.GetUser(currentUserId);

                var userDto = userResponseModel.Data;
               
                var applicationStatus = await _applicationService.StudentApplicationStatus(currentUserId);
                return View(applicationStatus);
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            }   
        }
        public async Task<IActionResult> StudentApplicationHistory()
        {
            try
            {
                var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                var userResponseModel = await _userService.GetUser(currentUserId);

                var userDto = userResponseModel.Data;

                var applicationStatus = await _applicationService.StudentApplicationHistory(currentUserId);
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
