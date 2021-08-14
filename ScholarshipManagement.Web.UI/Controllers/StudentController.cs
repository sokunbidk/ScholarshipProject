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

namespace ScholarshipManagement.Web.UI.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private IWebHostEnvironment _env;
        private readonly ICircuitService _circuitService;
        private readonly IJamaatService _jamaatService;
        public StudentController(IStudentService studentService, IWebHostEnvironment env, ICircuitService circuitService, IJamaatService jamaatService)
        {
            _studentService = studentService;
            _env = env;
            _circuitService = circuitService;
            _jamaatService = jamaatService;
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
        public IActionResult AdminDashboard()
        {
            var currentUser = User.Identity.Name;
            ViewBag.Message = $"Assalam Alaikum:{currentUser}";
            return View();
        }
        //Blank Registration Form New Candidate
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
            var files = HttpContext.Request.Form.Files;
            

            string upload = _env.WebRootPath + @"\UploadedFiles\AdmissionLetter";
            string fileName = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(files[0].FileName);


            using (var fileStream = new FileStream(Path.Combine(upload, fileName+extension), FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }

            ViewBag.Message = "Uploaded Successfully";

            var currentUser = User.FindFirst("Email").Value;

            model.Photograph = fileName + extension;

            await _studentService.CreateStudentAsync(model, currentUser);
            return RedirectToAction("CreateApplicationNewStudent", "ApplicationForm");
        }

        //View Profile-Returning Candidate
        [HttpGet]
        public async Task<IActionResult> ReturningCandidateRegView()
        {
            //var currentUserName = User.Identity.Name;

            var currentUser = User.FindFirst("Email").Value;

            var student = await _studentService.GetStudentReturningCandidate(currentUser);

            return View(student);

        }


    }
}
