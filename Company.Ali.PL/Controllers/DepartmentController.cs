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

        //private readonly IDepartmentRepository _departmentrepository;
        private readonly IUnitOfWork _unitOfWork;

        // ASK CLR Create Object From DepartmentRepository
        public DepartmentController(/*IDepartmentRepository departmentRepository*/ IUnitOfWork unitOfWork)
        {
            //_departmentrepository = departmentRepository;
          _unitOfWork = unitOfWork;
        }

        [HttpGet] // GET :  /Department/Index
        public IActionResult Index()
        {

         var departments = _unitOfWork.DepartmentRepository.GetAll();

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

               _unitOfWork.DepartmentRepository.Add(department);

                var Count = _unitOfWork.Complete();

                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int? Id)
        {

            if (Id is null) return BadRequest("Invalid Id"); //400

            var departments = _unitOfWork.DepartmentRepository.Get(Id.Value);

            if(departments is null) return NotFound( new { statusCode = 404, message = $"Department With Id : {Id} is not Found" });

            return View(departments);
        }

        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if (Id is null) return BadRequest("Invalid Id"); //400

            var departments = _unitOfWork.DepartmentRepository.Get(Id.Value);

            if (departments is null) return NotFound(new { statusCode = 404, message = $"Department With Id : {Id} is not Found" });

            var dto = new CreateDepartmentDto()
            {
                Name = departments.Name,
                Code = departments.Code,
                CreateAt = departments.CreateAt
            };

            return View(dto);
        }

        [HttpPost]
    
        public IActionResult Edit([FromRoute] int Id,CreateDepartmentDto model)
        {

            if (ModelState.IsValid)
            {

                var department = new Department()
                {
                    Id = Id,
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt
                };

               _unitOfWork.DepartmentRepository.Update(department);

                var Count = _unitOfWork.Complete();

                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            if (Id is null) return BadRequest("Invalid Id"); //400

            var departments = _unitOfWork.DepartmentRepository.Get(Id.Value);

            if (departments is null) return NotFound(new { statusCode = 404, message = $"Department With Id : {Id} is not Found" });

            var dto = new CreateDepartmentDto()
            {
                Name = departments.Name,
                Code = departments.Code,
                CreateAt = departments.CreateAt
            };

            return View(dto);
        }

        [HttpPost]
      
        public IActionResult Delete([FromRoute] int Id, CreateDepartmentDto model )
        {

            if (ModelState.IsValid)
            {

                var department = new Department()
                {
                    Id = Id,
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt
                };


              _unitOfWork.DepartmentRepository.Delete(department);

                var Count = _unitOfWork.Complete();

                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

    }
}
