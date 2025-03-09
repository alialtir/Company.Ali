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
        public IActionResult Details(int Id)
        {

            var departments = _departmentrepository.Get(Id);



            return View(departments);
        }
    }
}
