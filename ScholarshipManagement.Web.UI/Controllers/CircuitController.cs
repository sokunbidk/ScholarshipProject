using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagement.Web.UI.Controllers
{
    public class CircuitController : Controller
    {
        // GET: CircuitController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CircuitController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CircuitController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CircuitController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CircuitController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CircuitController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CircuitController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CircuitController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
