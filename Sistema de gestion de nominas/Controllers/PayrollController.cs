using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema_de_gestion_de_nominas.Models;

namespace Sistema_de_gestion_de_nominas.Controllers
{
    public class PayrollController : Controller
    {
        // GET: Payroll
        public ActionResult Index()
        {
            var listaNominas = new List<Payroll>();
            using(var db = new nominaDBContext())
            {
                listaNominas = db.Payroll.ToList();
            }
            return View(listaNominas);
        }

        // GET: Payroll/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Payroll/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Payroll/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Payroll/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Payroll/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Payroll/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Payroll/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}