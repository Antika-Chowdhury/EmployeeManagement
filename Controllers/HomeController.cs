using System;
using System.Web.Mvc;
using EmployeeDatabase;
using EmployeeManagementModel;
using EmployeeDatabase.DbOperations;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        EmployeeRepository repository = null;

        public HomeController()
        {
            repository = new EmployeeRepository();
        }

        // GET: Home/AddEmployee
        public ActionResult AddEmployee()
        {
            var viewEmployeeSalaryModel = new EmployeeSalaryModel()
            {
                Employee = new EmployeeModel(),
                Salary = new SalaryModel()
            };

            return View(viewEmployeeSalaryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployee(EmployeeSalaryModel model)
        {
            if(ModelState.IsValid)
            {
              int id =  repository.AddEmployee(model);
                if(id>0)
                {
                    ModelState.Clear();
                }
            }
            return View(model);
        }

        public ActionResult GetAllEmployeeList()
        {
            var result = repository.GetEmployeeList();
            return View(result);
        }


    }
}
