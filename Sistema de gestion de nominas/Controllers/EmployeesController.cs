using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sistema_de_gestion_de_nominas.Models;

namespace Sistema_de_gestion_de_nominas.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees
        public ActionResult Index(string active = "", bool isReport = false)
        {
            
            var employeeList = new List<Employee>();

            using(var db = new nominaDBContext())
            {
                if(active != "" && active != null)
                {
                    employeeList = db.Employee.Where(e => e.Active == (active == "1" ? true : false)).ToList();
                }
            
                else
                {
                employeeList = db.Employee.ToList();
            }
        }
                

            return View(new EmployeeViewModel() { employeeList = employeeList, isReport = isReport});
        }

        // GET: Employees/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
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

        // GET: Employees/Edit/5
        public ActionResult Edit(int id)
        {
            Employee employee = new Employee();
            if (id != 0)
            {
                
                using (var db = new nominaDBContext())
                {
                    employee = db.Employee.Find(id);
                }
            }
           
            
            return View(employee);
        }

        // POST: Employees/Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(int id,Employee employee)
        {

            if (!ModelState.IsValid)
            {
                return View("Edit",employee);
            }
            if(id != employee.Id)
            {
                return BadRequest();
            }
            using(var db = new nominaDBContext())
            {
                if (id == 0)
                {
                    db.Employee.Add(employee);
                }
                else
                {
                    var employeeInDb = db.Employee.Find(id);
                    employeeInDb.Name = employee.Name;
                    employeeInDb.LastName = employee.LastName;
                    employeeInDb.Genre = employee.Genre;
                    employeeInDb.GrossSalary = employee.GrossSalary;
                    employeeInDb.Active = employee.Active;

                }
                db.SaveChanges();
            }
            return RedirectToAction("Index");
           
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int id)
        {
            if(id != 0)
            {
                using(var db = new nominaDBContext())
                {
                    var employee = db.Employee.Find(id);
                    db.Employee.Remove(employee);

                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        // POST: Employees/Delete/5
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