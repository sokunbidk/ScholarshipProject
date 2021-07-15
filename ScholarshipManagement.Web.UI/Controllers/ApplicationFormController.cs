using Microsoft.AspNetCore.Mvc;
using ScholarshipManagement.Data;
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

        public ApplicationFormController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(CreateApplicationFormRequestModel model)
        {
            _applicationService.CreateApplicationAsync(model);
             return View();
        }
    }
}
