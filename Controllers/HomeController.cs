using System;
using System.Web.Mvc;
using EmployeeTestDatabase;
using EmployeeManagementModel;
using EmployeeTestDatabase.DbOperations;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        EmployeeRepo repository = null;

        public HomeController()
        {
            repository = new EmployeeRepo();
        }

        // GET: Home/AddEmployee
        public ActionResult AddEmployee()
        {

            return View();
        }


        //[HttpPost]
        //public ActionResult AddEmployee(EmployeeModel model)
        //{
        //    if(ModelState.IsValid)
        //    {
        //      int id =  repository.AddEmployee(model);
        //        if(id>0)
        //        {
        //            ModelState.Clear();
        //            ViewBag.EmployeeID = id;
        //        }
        //    }
        //    return RedirectToAction("GetAllEmployeeList");
        //}

        [HttpPost]

        public ActionResult AddEmployee(EmployeeModel model)
        {
            bool isEmailExist;
            int id = repository.AddEmployee(model, model.Email, out isEmailExist);
            if (id > 0)
            {
                // Employee added successfully
                ModelState.Clear();
                ViewBag.EmployeeID = id;
            }
            else if (isEmailExist)
            {
                // Email already exists, handle error message
                ModelState.AddModelError(string.Empty, "Email already exists.");
            }

            return RedirectToAction("GetAllEmployeeList");
        }


        public ActionResult GetAllEmployeeList()
        {
            var result = repository.GetEmployeeList();
            return View(result);
        }

        public ActionResult EmployeeDetails(int id)
        {
            var employeeDetail = repository.GetEmployeeDetails(id);
            return View(employeeDetail);
        }
   

        public ActionResult EmployeeUpdate(int id)
        {
            var employee = repository.GetEmployeeDetails(id);
            return View(employee);
        }

        [HttpPost]
        public ActionResult EmployeeUpdate(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateEmployee(model.EmployeeID, model);
                return RedirectToAction("GetAllEmployeeList");
            }
            return View();
        }

    }
}
