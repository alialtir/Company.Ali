using Company.Ali.BLL.Interfaces;
using Company.Ali.BLL.Repositories;
using Company.Ali.DAL.Models;
using Company.Ali.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.Ali.PL.Controllers
{
    // MVC Controller

    public class DepartmentController : Controller
    {

        private readonly IDepartmentRepository _departmentrepository;

        // ASK CLR Create Object From DepartmentRepository
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentrepository = departmentRepository;
        }

        [HttpGet] // GET :  /Department/Index
        public IActionResult Index()
        {

         var departments = _departmentrepository.GetAll();

            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid) // Server Side Validation
            {

                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt
                };

              var Count = _departmentrepository.Add(department);

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

            var departments = _departmentrepository.Get(Id.Value);

            if(departments is null) return NotFound( new { statusCode = 404, message = $"Department With Id : {Id} is not Found" });

            return View(viewName,departments);
        }

        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            //if (Id is null) return BadRequest("Invalid Id"); //400

            //var departments = _departmentrepository.Get(Id.Value);

            //if (departments is null) return NotFound(new { statusCode = 404, message = $"Department With Id : {Id} is not Found" });

            return Details(Id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int Id,Department department)
        {

            if (ModelState.IsValid)
            {
                if (Id != department.Id) return BadRequest(); // 400

                var count = _departmentrepository.Update(department);

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(department);
        }

        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            //if (Id is null) return BadRequest("Invalid Id"); //400

            //var departments = _departmentrepository.Get(Id.Value);

            //if (departments is null) return NotFound(new { statusCode = 404, message = $"Department With Id : {Id} is not Found" });

            return Details(Id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int Id, Department department )
        {

            if (ModelState.IsValid)
            {

                if (Id != department.Id) return BadRequest(); // 400

                var count = _departmentrepository.Delete(department);

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(department);
        }

    }
}
