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


        [HttpPost]
        public ActionResult AddEmployee(EmployeeModel model)
        {
            if(ModelState.IsValid)
            {
              int id =  repository.AddEmployee(model);
                if(id>0)
                {
                    ModelState.Clear();
                    ViewBag.EmployeeID = id;
                }
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

        [HttpPost]
        public ActionResult EmployeeDetails(EmployeeModel model)
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
