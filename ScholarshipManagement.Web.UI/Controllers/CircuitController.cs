using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ScholarshipManagement.Data;
using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Interfaces;
using ScholarshipManagement.Data.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//using System.Web.Mvc;

namespace ScholarshipManagement.Web.UI.Controllers
{
    public class CircuitController : Controller
    {
        private readonly ICircuitService _circuitService;
        private readonly ICircuitRepository _circuitRepository;
        private readonly IUserService _userService;

        public CircuitController(ICircuitService circuitService, ICircuitRepository circuitRepository, IUserService userService)
        {
            _circuitService = circuitService;
            _circuitRepository = circuitRepository;
            _userService = userService;
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

        //Creat Circuit- inputs from User
        [HttpPost]
        public async Task<IActionResult> Create(CreateCircuitRequestModel model)
        {
            try
            {
                {
                    BaseResponse circuitCreate = await _circuitService.CreateCircuitAsync(model);
                    ViewBag.Message = circuitCreate.Message;
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;        
                return View();
            }          
        }
        //Get Circuit To be Update
        [HttpGet]
        public async Task<IActionResult> UpdateCircuit(int id)
        {
            CircuitResponseModel circuit = await _circuitService.GetCircuit(id);
            CircuitDto circuitDto = circuit.Data;

            return View(circuitDto);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCircuit(int id, UpdateCircuitRequestModel model)
        {
            try 
            { 
                BaseResponse circuitUpdate = await _circuitService.UpdateCircuitAsync(id, model);
                ViewBag.Message = circuitUpdate.Message;
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            }
        }







    }
}
