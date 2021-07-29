using Microsoft.AspNetCore.Mvc;
using ScholarshipManagement.Data;
using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Enums;
using ScholarshipManagement.Data.Services;
using System;
using System.Threading.Tasks;

namespace ScholarshipManagement.Web.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequestModel model)
        {
            try
            {
                await _userService.CreateUserAsync(model);
            }
            catch (Exception e)
            {
                ViewBag.CreateError = e.Message;
                _ = e.Message;
            }

            return RedirectToAction(nameof(Login));

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserRequestModel model)
        {
            var user = await _userService.LoginUserAsync(model);

            if (user.UserType == UserType.Student)
            {
                return RedirectToAction("Dashboard", "Student");
            }
            else
            {
                return RedirectToAction("AdminDashboard", "Student");
            }
        }


    }
}
