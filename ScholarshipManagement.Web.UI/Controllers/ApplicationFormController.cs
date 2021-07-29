using Microsoft.AspNetCore.Mvc;
using ScholarshipManagement.Data;
using ScholarshipManagement.Data.Interfaces;
using ScholarshipManagement.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagement.Web.UI.Controllers
{
    
    public class ApplicationFormController : Controller
    {
        private readonly IApplicationService _applicationService;
        private readonly IStudentService _studentService;
        private readonly IStudentRepository _studentRepository;

        public ApplicationFormController(IApplicationService applicationService,IStudentService studentService,IStudentRepository studentRepository)
        {
            _applicationService = applicationService;
            _studentService = studentService;
            _studentRepository = studentRepository;
        }
        
        public IActionResult ReturningCandidate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ReturningCandidate(CreateApplicationFormRequestModel model)
        {
             _applicationService.CreateApplicationAsync(model);

             return RedirectToAction("Dashboard","Student");
        }

        [HttpPost]
        public IActionResult UpdateProgress(int id, UpdateApplicationRequestModel model)
        {
            
            var students = _applicationService.UpdateApplicationAsync(id, model);

            return View(students);
  
        }
        [HttpGet]
        public IActionResult UpdateProgress()
        {
             var applications = _studentRepository.GetStudentApplicationFormsAsync();

            return View(applications);
        }

        [HttpPost]
        public IActionResult EditApplication(UpdateApplicationRequestModel model)
        {
            var students = _applicationService.Update(model);

            return View(students);

            //return RedirectToAction(GetApplications);
        }
        [HttpGet]
        public IActionResult EditApplication(int id)
        {
            var applications = _applicationService.GetApplication(id);

            return View(applications);
        }
    }
}
