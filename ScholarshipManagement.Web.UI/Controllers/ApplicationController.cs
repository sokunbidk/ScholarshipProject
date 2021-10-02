using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    
    public class ApplicationController : Controller
    {
        private readonly IApplicationService _applicationService;
        private readonly IStudentService _studentService;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _env;
        private readonly IApplicationRepository _applicationRepository;
        private readonly ICircuitService _circuitService;
        private readonly IJamaatService _jamaatService;
        

        public ApplicationController(IApplicationService applicationService,IStudentService studentService,IUserService userService, IWebHostEnvironment env, IApplicationRepository applicationFormRepository,ICircuitService circuitService,IJamaatService jamaatService)
        {
            _applicationService = applicationService;
            _studentService = studentService;
            _userService = userService;
            _env = env;
            _applicationRepository = applicationFormRepository;
            _circuitService = circuitService;
            _jamaatService = jamaatService;
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
        public async Task <IActionResult> CreateApplication(CreateApplicationRequestModel model)
        {  
            try
            {
                //Upload Documents
                Random random = new Random();
                var currentUser = User.FindFirst("Email").Value;
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var files = HttpContext.Request.Form.Files;


               // string admissionLetterupload = _env.WebRootPath + @"\UploadedFiles\AdmissionLetter\";
                string schBillupload = _env.WebRootPath + @"\UploadedFiles\SchBill\";
                string schResultupload = _env.WebRootPath + @"\UploadedFiles\SchResult\";


                //string admissionLetterfileName = currentUserId + "-admissionLetter-" + random.Next(100000).ToString();

                string schBillfileName = currentUserId + "-schBill-" + random.Next(100000).ToString();
                string schResultfileName = currentUserId + "-schResult-" + random.Next(100000).ToString();


                //string admissionLetterExtension = Path.GetExtension(files[0].FileName);
                string schBillExtension = Path.GetExtension(files[0].FileName);
                string schResultExtension = Path.GetExtension(files[1].FileName);


                /*using (var fileStream = new FileStream(Path.Combine(admissionLetterupload, admissionLetterfileName + admissionLetterExtension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }*/

                using (var fileStream = new FileStream(Path.Combine(schBillupload, schBillfileName + schBillExtension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                using (var fileStream = new FileStream(Path.Combine(schResultupload, schResultfileName + schResultExtension), FileMode.Create))
                {
                    files[1].CopyTo(fileStream);
                }


                //model.LetterOfAdmission = admissionLetterfileName + admissionLetterExtension;
                model.SchoolBill = schBillfileName + schBillExtension;
                model.LastSchoolResult = schResultfileName + schResultExtension;

              BaseResponse furtherApplication = await _applicationService.CreateApplicationAsync(model, currentUser);

                ViewBag.Message = furtherApplication.Message;
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;   

                return View();
            }           
            return View();

        }
        //New Candidate Application, after Registration
        [HttpPost]
        public async Task<IActionResult> CreateApplicationNewStudent(CreateApplicationRequestModel model)
        {
            try
            {
                Random random = new Random();
                var currentUser = User.FindFirst("Email").Value;
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;


                var files = HttpContext.Request.Form.Files;


                string admissionLetterupload = _env.WebRootPath + @"\UploadedFiles\AdmissionLetter\";
                string schBillupload = _env.WebRootPath + @"\UploadedFiles\SchBill\";
                //string schResultupload = _env.WebRootPath + @"\UploadedFiles\SchResult\";


                string admissionLetterfileName = currentUserId + "-admissionLetter-" + random.Next(100000).ToString();

                string schBillfileName = currentUserId + "-schBill-" + random.Next(100000).ToString();

                //string schResultfileName = currentUserId + "-schResult-" + random.Next(100000).ToString();


                string admissionLetterExtension = Path.GetExtension(files[0].FileName);
                string schBillExtension = Path.GetExtension(files[1].FileName);
                //string schResultExtension = Path.GetExtension(files[2].FileName);


                using (var fileStream = new FileStream(Path.Combine(admissionLetterupload, admissionLetterfileName + admissionLetterExtension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                using (var fileStream = new FileStream(Path.Combine(schBillupload, schBillfileName + schBillExtension), FileMode.Create))
                {
                    files[1].CopyTo(fileStream);
                }

                /*using (var fileStream = new FileStream(Path.Combine(schResultupload, schResultfileName + schResultExtension), FileMode.Create))
                {
                    files[2].CopyTo(fileStream);
                }*/


                model.LetterOfAdmission = admissionLetterfileName + admissionLetterExtension;
                model.SchoolBill = schBillfileName + schBillExtension;
                //model.LastSchoolResult = schResultfileName + schResultExtension;

                BaseResponse newApplication = await _applicationService.CreateNewApplicationAsync(model, currentUser);
                ViewBag.Message = newApplication.Message;

            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;        

                return View();
                
            }
            return View();
        }
        //Pending Applications
        public async Task<IActionResult> PendingApplications()
        {
            try
            {
                var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                var userResponseModel = await _userService.GetUser(currentUserId);

                var userDto = userResponseModel.Data;


                List<ApprovalStatus> status = new List<ApprovalStatus>() { ApprovalStatus.Submitted };
                var isGlobal = true;
                List<int> circuitIds = null;
                //int circuitId = 0;
                switch (userDto.UserType)
                {
                    case UserType.Circuit:

                        status = new List<ApprovalStatus>() { ApprovalStatus.Submitted, ApprovalStatus.Committee,ApprovalStatus.Declined };
                        isGlobal = false;
                        circuitIds = new List<int>();
                        /*var circuit = await _userService.GetUserCircuit(userDto.Id);
                        if (circuit != null)
                        {
                            circuitIds.Add(circuit.Id);

                        }*/
                        circuitIds.Add(userDto.CircuitId);
                        //circuitId = userDto.CircuitId;

                        break;
                    case UserType.Admin:
                        status = new List<ApprovalStatus>() { ApprovalStatus.NaibAmir, ApprovalStatus.Amir, ApprovalStatus.Accounts, ApprovalStatus.Committee, ApprovalStatus.Submitted, ApprovalStatus.Disbursed };
                        break;
                    case UserType.Committee:
                        status = new List<ApprovalStatus>() { ApprovalStatus.NaibAmir, ApprovalStatus.Amir, ApprovalStatus.Accounts, ApprovalStatus.Disbursed };
                        break;
                    case UserType.NaibAmir:
                        status = new List<ApprovalStatus>()
                    {
                       ApprovalStatus.NaibAmir ,ApprovalStatus.Amir
                    };
                        break;
                    case UserType.Amir:
                        status = new List<ApprovalStatus>() { ApprovalStatus.Amir, ApprovalStatus.Accounts, ApprovalStatus.Approved };
                        break;
                    case UserType.Accounts:
                        status = new List<ApprovalStatus>() { ApprovalStatus.Accounts, ApprovalStatus.Disbursed };
                        break;
                }
                var pendingApplications = await _applicationService.PendingApplicationsByStatus(status, isGlobal, circuitIds, userDto.Id);

                return View(pendingApplications);
            }
            catch(Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            }

        }
        //updates Application Status
        [HttpGet]
        public  IActionResult UpdateApprovalStatus(int id)
        {
            try
            {
                var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                var status  =  _applicationService.UpdateApprovalStatus(id, currentUserId);
                ViewBag.Message = status.Exception;
                return RedirectToAction("PendingApplications");
                
            }
            
            catch (Exception e)
            {
                ViewBag.Message = e.Message;

                //return View();
                return RedirectToAction("PendingApplications");
            }
  
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
            catch(Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            }
            //return ViewBag.Message = "Application Declined!";



        }
        public  IActionResult ResetAction(int id)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            _applicationService.ResetAction(id , currentUserId);

            return RedirectToAction("PendingApplications");
        }
        [HttpGet]
        public async Task<IActionResult> PendingApplicationsDetail(int id)
        {
            try
            {
                try
                {
                    ApplicationResponseModel  response = await _applicationService.GetApplication(id);
                    var ApplicationDetails = response.Data;
                   
                   return View(ApplicationDetails);
                   // return View(response);
                }
                catch(Exception e) { ViewBag.Message = e.Message; return View(); }
                
            }
            catch(Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            }

            
        }
        public async Task<IActionResult> PendingStudentsDetail(int id)
        {
            try
            {try
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

                    StudentResponseModel response = await _studentService.GetApplicantById(id);
                    var pendingStudent = response.Data;
                    return View(pendingStudent);
                }
                catch (Exception e) { ViewBag.Message = e.Message; return View(); }
            }
            catch(Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            } 
            
            
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

                return RedirectToAction("PendingApplications");
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
