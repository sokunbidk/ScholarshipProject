using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScholarshipManagement.Data;
using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Interfaces;
using ScholarshipManagement.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagement.Web.UI.Controllers
{
    public class CircuitController : Controller
    {
        private readonly ICircuitService _circuitService;

        private readonly ICircuitRepository _circuitRepository;

        public CircuitController(ICircuitService circuitService, ICircuitRepository circuitRepository)
        {
            _circuitService = circuitService;
            _circuitRepository = circuitRepository;
        }
        //Projects from Db to View
        public ActionResult Index()
        {

            var circuit = _circuitService.GetCircuits();
            return View(circuit);
        }
        //BlankForm
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //Updates inputs from User
        [HttpPost]
        public async Task<IActionResult> Create(CreateCircuitRequestModel model)
        {
          await _circuitService.CreateCircuitAsync(model);
            return RedirectToAction ("Index");
        }

   
    }
}
