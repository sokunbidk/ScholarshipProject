using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ScholarshipManagement.Data;
using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Interfaces;
using ScholarshipManagement.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagement.Web.UI.Controllers
{
    public class JamaatController : Controller
    {
        private readonly IJamaatService _jamaatService;

        private readonly ICircuitService _circuitService;

        private readonly IJamaatRepository _jamaatRepository;

        public JamaatController(IJamaatService jamaatService, IJamaatRepository jamaatRepository, ICircuitService circuitService)
        {
            _jamaatService = jamaatService;
            _jamaatRepository = jamaatRepository;
            _circuitService = circuitService;
        }


        
        public ActionResult Index()
        {
             var jamaat = _jamaatService.GetJamaats();
            
            return View(jamaat);
        }
        [HttpGet]
        public IActionResult Create()
        {
            List<Circuit> circuits = _circuitService.GetCircuitList();

            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (Circuit circuit in circuits)
            {
                SelectListItem item = new SelectListItem(circuit.CircuitName, circuit.Id.ToString());
                listItems.Add(item);
            }
            ViewBag.Circuits = listItems;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateJamaatRequestModel model)
        {
            
            await _jamaatService.CreateJamaatAsync(model);
            return RedirectToAction ("Index");
        }




    }
}
