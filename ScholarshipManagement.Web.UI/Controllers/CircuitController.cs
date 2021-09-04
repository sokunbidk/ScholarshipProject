using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ScholarshipManagement.Data;
using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Exceptions;
using ScholarshipManagement.Data.Interfaces;
using ScholarshipManagement.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
            List<UserDto> circuitusers = _userService.GetUserType();
            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (UserDto user in circuitusers)
            {
                SelectListItem item = new SelectListItem(user.UserFullName, user.Id.ToString());
                listItems.Add(item);
            }
            ViewBag.PresidentId = listItems;
            return View();
        }

        //Creat Circuit- inputs from User
        [HttpPost]
        public async Task<IActionResult> Create(CreateCircuitRequestModel model)

        {
            try
            {
                {
                    await _circuitService.CreateCircuitAsync(model);
                    
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {

                
                ViewBag.Message = e.Message;          //Exception message in the method

                return View();
            }
            ViewBag.Message = "Circuit successfull created"; //success message

        }

        
        

        

   
    }
}
