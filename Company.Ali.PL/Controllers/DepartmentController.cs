using Company.Ali.BLL.Interfaces;
using Company.Ali.BLL.Repositories;
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
    }
}
