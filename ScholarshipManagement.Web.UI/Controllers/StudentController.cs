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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult AdminDashboard()
        {
            return View();
        }

        public IActionResult NewCandidate()
        {
            return View();
        }

        [HttpPost]
        public  IActionResult NewCandidate(CreateStudentRequestModel model)
        {
            _studentService.CreateStudentAsync(model);

            return RedirectToAction("ReturningCandidate", "ApplicationForm");
        }
        

    }
}
