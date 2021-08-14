using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using ScholarshipManagement.Data;
using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Enums;
using ScholarshipManagement.Data.Interfaces;
using ScholarshipManagement.Data.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using System.Threading.Tasks;

namespace ScholarshipManagement.Web.UI.Controllers

{
    public class SecretariatController : Controller
    {
        public IActionResult SecretariatDashboard()
        {
            var currentUser = User.Identity.Name;
            ViewBag.Message = $"Assalam Alaikum:{currentUser}";
            return View();
        }

    }
}
