using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
//using AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                listaNominas = db.Payroll.Include(n => n.Employee).ToList();
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
            using(var db = new nominaDBContext())
            {
                var viewModel = new PayrollViewModel()
                {
                    Payroll = new Payroll(),
                    employeeList = db.Employee.Include(e => e.Payroll).Where(e => e.Payroll.Count() == 0).ToList()
                    
                };
                return View(viewModel);
            }
            
        }

        // POST: Payroll/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Payroll payroll)
        {

            if (!ModelState.IsValid)
            {
                using(var db = new nominaDBContext())
                {
                    var viewModel = new PayrollViewModel()
                    {
                        employeeList = db.Employee.Include(e => e.Payroll).Where(e => e.Payroll.Count() == 0).ToList(),
                        Payroll = payroll
                    };
                return View("Edit",viewModel);
                }
              
            }


            using (var db = new nominaDBContext())
            {
                var employeeInDb = db.Employee.Find(payroll.EmployeeId);
                var afp = Convert.ToDouble(employeeInDb.GrossSalary) * 0.0287;
                var ars = Convert.ToDouble(employeeInDb.GrossSalary) * 0.0304;
                var taxableSalary = employeeInDb.GrossSalary - Convert.ToDecimal((afp + ars));
                var isr = calculateISR(Convert.ToDouble(taxableSalary));
                var retentionTotal = afp + ars + isr;

                var newPayroll = new Payroll()
                {
                    EmployeeId = payroll.EmployeeId,
                    GrossSalary = employeeInDb.GrossSalary,
                    RetentionAfp = afp,
                    RetentionArs = ars,
                    TaxableSalary = taxableSalary,
                    RetentionIsr = isr,
                    RetentionTotal = retentionTotal,
                    NetIncome = employeeInDb.GrossSalary - Convert.ToDecimal(retentionTotal)
                };

                db.Payroll.Add(newPayroll);

                db.SaveChanges();
            }

            return RedirectToAction("Index");
            
        }

        // GET: Payroll/Delete/5
        public ActionResult Delete(int id)
        {
            if(id != 0)
            {
                using(var db = new nominaDBContext())
                {
                    var nominaInDb = db.Payroll.Find(id);
                    db.Payroll.Remove(nominaInDb);

                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
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
        private double calculateISR(double sueldoNeto)
        {
            var sueldoAnual = sueldoNeto * 12;
            double isr = 0;
            double escala1 = 416220.01, escala2 = 624329.01, escala3 = 867123.01;

            if(sueldoAnual < escala1)
            {
                return isr;
            }
            else 
            {
                if(sueldoAnual >= escala1 && sueldoAnual < escala2)
                {
                    isr = (0.15 * (sueldoAnual - escala1)) / 12;
                }
                else if (sueldoAnual >= escala2 && sueldoAnual < escala3)
                {
                    isr =  (31216 + (0.20 * (sueldoAnual - escala2))) / 12;
                }
                else if (sueldoAnual >= escala3)
                {
                    isr = (79776 + (0.25 * (sueldoAnual - escala3))) / 12;
                }

                return isr;
            }
        }
    }
}