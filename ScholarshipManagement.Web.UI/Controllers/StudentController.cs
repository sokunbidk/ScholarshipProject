using Microsoft.AspNetCore.Mvc;
using ScholarshipManagement.Data;
using ScholarshipManagement.Data.Services;

namespace ScholarshipManagement.Web.UI.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public IActionResult NewCandidate()
        {
            return View();
        }


        [HttpPost]
        public  IActionResult NewCandidate(CreateStudentRequestModel model)
        {
           var student = _studentService.CreateStudentAsync(model);

            // return RedirectToAction("ReturningCandidate", "ApplicationForm");
            return RedirectToAction("NewCandidate");
        }
        

    }
}
