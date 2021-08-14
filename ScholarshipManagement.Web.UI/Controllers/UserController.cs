using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagement.Data;
using ScholarshipManagement.Data.ApplicationContext;
using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Enums;
using ScholarshipManagement.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ScholarshipManagement.Web.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly SchoolDbContext _schoolDbContext;

        public UserController(SchoolDbContext context, IUserService userService)
        {
            _userService = userService;
            
            _schoolDbContext = context;
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
               // ViewBag.CreateError = e.Message;
               // _ = e.Message;
                ViewBag.Message = e.Message;
                
                return View();
            }
            ViewBag.Message = "User successfully created";
            //return View();
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
            if (user == null)
            {
                ViewBag.Message = "Invalid Username/Password";

                return View();
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, $"{user.UserFullName}"),
                    new Claim(ClaimTypes.HomePhone, $"{user.PhoneNumber}"),
                    new Claim(ClaimTypes.SerialNumber, $"{user.MemberCode}"),
                    new Claim(ClaimTypes.UserData, $"{user.UserType}"),
                    //new Claim(ClaimTypes.GivenName, $"{customer.FirstName} {customer.LastName}"),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    //new Claim(ClaimTypes.Email, user.Email),
                    new Claim("Email", user.Email),
                    new Claim(ClaimTypes.Role, "Student"),
                    new Claim(ClaimTypes.Role, "Admin"),
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authenticationProperties = new AuthenticationProperties();
                var principal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);

                //var currentUserType = User.FindFirst(ClaimTypes.UserData).Value;
                //var currentUser = User.Identity.Name;
                var currentUser = principal.FindFirst("Email");
                

                if (user.UserType == UserType.Secretariat)
                {
                    return RedirectToAction("SecretariatDashboard", "Secretariat");
                }

                if (user.UserType != UserType.Student)
                {
                    return RedirectToAction("AdminDashboard", "Student");
                }
                else
                {
                    var student = _schoolDbContext.Students.FirstOrDefault(p => p.UserId == user.Id);
                    if (student == null)
                    {

                        return RedirectToAction("Dashboard", "Student");
   
                    }
                    else
                    {
                       
                        return RedirectToAction("DashboardReturningStudent", "Student");
                    }

                }

            }
            
        }

        [HttpGet]
        public IActionResult Logout()
        {

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
       

    }
}
