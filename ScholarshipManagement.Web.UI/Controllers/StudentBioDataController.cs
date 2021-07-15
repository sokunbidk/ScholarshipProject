using Microsoft.AspNetCore.Mvc;
using ScholarshipManagement.Data;
using ScholarshipManagement.Data.Services;

namespace ScholarshipManagement.Web.UI.Controllers
{
    public class StudentBioDataController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentBioDataController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(CreateStudentRequestModel model)
        {
            var registration = _studentService.CreateStudentAsync(model);
            return View(registration);
        }
       
    }
}
