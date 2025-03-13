using Company.Ali.BLL.Interfaces;
using Company.Ali.DAL.Models;
using Company.Ali.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.Ali.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        // ASK CLR Create Object From DepartmentRepository
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet] // GET :  /Department/Index
        public IActionResult Index()
        {

            var employees = _employeeRepository.GetAll();

            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid) // Server Side Validation
            {

                var employees = new Employee()
                {
                  Name = model.Name,
                  Address = model.Address,
                  Age = model.Age,
                  CreateAt = model.CreateAt,
                  HiringDate = model.HiringDate,
                  Email = model.Email,
                  IsActive = model.IsActive,
                  IsDeleted = model.IsDeleted,
                  Phone = model.Phone,
                  Salary = model.Salary
                };

                var Count = _employeeRepository.Add(employees);

                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult Details(int? Id, string viewName = "Details")
        {

            if (Id is null) return BadRequest("Invalid Id"); //400

            var employee = _employeeRepository.Get(Id.Value);

            if (employee is null) return NotFound(new { statusCode = 404, message = $"employee With Id : {Id} is not Found" });

            return View(viewName, employee);
        }

        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            //if (Id is null) return BadRequest("Invalid Id"); //400

            //var employee = _employeeRepository.Get(Id.Value);

            //if (employee is null) return NotFound(new { statusCode = 404, message = $"Department With Id : {Id} is not Found" });

            return Details(Id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int Id, Employee employee)
        {

            if (ModelState.IsValid)
            {
                if (Id != employee.Id) return BadRequest(); // 400

                var count = _employeeRepository.Update(employee);

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(employee);
        }

        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            //if (Id is null) return BadRequest("Invalid Id"); //400

            //var employee = _employeeRepository.Get(Id.Value);

            //if (employee is null) return NotFound(new { statusCode = 404, message = $"employee With Id : {Id} is not Found" });

            return Details(Id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int Id, Employee model)
        {

            if (ModelState.IsValid)
            {

                if (Id != model.Id) return BadRequest(); // 400

                var count = _employeeRepository.Delete(model);

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

    }
}
