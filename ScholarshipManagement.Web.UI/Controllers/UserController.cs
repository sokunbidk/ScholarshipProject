using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ScholarshipManagement.Data;
using ScholarshipManagement.Data.ApplicationContext;
using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Enums;
using ScholarshipManagement.Data.Services;
using ScholarshipManagement.Web.UI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ScholarshipManagement.Web.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly SchoolDbContext _schoolDbContext;
        private readonly IStudentService _studentService;
        private IWebHostEnvironment _env;
        private readonly ICircuitService _circuitService;
        private readonly IJamaatService _jamaatService;
        private readonly IUserService _userService;
        private readonly IApplicationService _applicationService;

        public UserController(SchoolDbContext context, IStudentService studentService, IWebHostEnvironment env, ICircuitService circuitService, IJamaatService jamaatService, IUserService userService, IApplicationService applicationService)
        {
            
            _schoolDbContext = context;
            _studentService = studentService;
            _env = env;
            _circuitService = circuitService;
            _jamaatService = jamaatService;
            _userService = userService;
            _applicationService = applicationService;
        }

        public IActionResult Index()
        {
            return View();
        }
        //Blank User input-Create User
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Create()
        {
            List<Circuit> circuits = _circuitService.GetCircuitList();
            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (Circuit circuit in circuits)
            {
                SelectListItem item = new SelectListItem
                (circuit.CircuitName, circuit.Id.ToString());
                listItems.Add(item);
            }
            ViewBag.Circuits = listItems;

            List<Jamaat> jamaats = _jamaatService.GetJamaatList();
            List<SelectListItem> jamaatList = new List<SelectListItem>();
            foreach (Jamaat jamaat in jamaats)
            {
                SelectListItem item = new SelectListItem
                    (jamaat.JamaatName, 
                    (jamaat.Id).ToString());

                jamaatList.Add(item);
            }
            ViewBag.Jamaats = jamaatList;
            return View();
            


        }
        //Create User
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequestModel model)
        {
            try
            {
                UserEntityResponseModel user =  await _userService.CreateUserAsync(model);
                ViewBag.Message = user.Message;
                return RedirectToAction("Login");  
            }
            catch (Exception e)
            {               
                ViewBag.Message = e.Message;                          
                return View();
            }
            //return RedirectToAction(nameof(Login));

        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserRequestModel model)
        {
            try
            {
                UserResponseModel logNew = await _userService.LoginUserAsync(model);
                ViewBag.Message = logNew.Message;
                var user = logNew.Data;
                if (user == null)
                {
                    ViewBag.Message = logNew.Message;

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
                    new Claim(ClaimTypes.Role,Enumerations.GetEnumDescription(user.UserType)),
                };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authenticationProperties = new AuthenticationProperties();
                    var principal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);

                    //var currentUserType = User.FindFirst(ClaimTypes.UserData).Value;
                    //var currentUserName = User.Identity.Name;
                    //var currentUserEmail = User.FindFirst("Email").Value;
                    //var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    var currentUser = principal.FindFirst("Email");

                    //if (user.UserType == UserType.Admin)
                    //{
                    //    return RedirectToAction("AdminDashboard", "Admin");
                    //}
                    //if (user.UserType == UserType.NaibAmir)
                    //{
                    //    return RedirectToAction("Dashboard_NaibAmir", "NaibAmir");
                    //}
                    //if (user.UserType == UserType.Amir)
                    //{
                    //    return RedirectToAction("Dashboard_Amir", "Amir");
                    //}
                    //if (user.UserType == UserType.Accounts)
                    //{
                    //    return RedirectToAction("AccountDashboard", "Account");
                    //}

                    //else if (user.UserType == UserType.Committee)
                    //{
                    //    return RedirectToAction("CommitteeDashboard", "Committee");
                    //}


                    if (user.UserType != UserType.Student)
                    {
                        return RedirectToAction("List", "Application");
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
            catch (Exception e)
            {
                ViewBag.Message = e.Message;

                return View();
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
