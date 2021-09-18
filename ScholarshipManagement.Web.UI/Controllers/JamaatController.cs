using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ScholarshipManagement.Data;
using ScholarshipManagement.Data.DTOs;
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


        //View Jamaats Created in Line 58
        public ActionResult Index()
        {
             var jamaat = _jamaatService.GetJamaats();
            
            return View(jamaat);
        }
        //Supply Circuit Dropdown List
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

        //Creates the Jamaat Method
        [HttpPost]
        public async Task<IActionResult> Create(CreateJamaatRequestModel model)
        {
            try
            {
                await _jamaatService.CreateJamaatAsync(model);
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ViewBag.Message = e.Message;
            }
            return ViewBag.Message = "Jamaat Created Successfully";


        }
        //Get jamaat To be Update
        [HttpGet]
        public async Task<IActionResult> UpdateJamaat(int id)
        {
            
            List<Circuit> circuits = _circuitService.GetCircuitList();

            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (Circuit circuit in circuits)
            {
                SelectListItem item = new SelectListItem
                ( circuit.CircuitName,
                    circuit.Id.ToString()
                );

                listItems.Add(item);
            }
            ViewBag.Circuits = listItems;

            JamaatResponseModel jamaat = await _jamaatService.GetJamaat(id);

            var jamaatDto = jamaat.Data;

            return View(jamaatDto);
        }
        [HttpPost]
        public IActionResult UpdateJamaat(int id, UpdateJamaatRequestModel model)
        {
            try
            {
                _jamaatService.UpdateJamaatAsync(id, model);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            }
            ViewBag.Message = "Updated Successfully";



        }

    }
}
