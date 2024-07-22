using EmployeeProject_MVC.Data;
using EmployeeProject_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web.Mvc;

namespace EmployeeProject_MVC.Controllers
{
	public class EmployeesController : Controller
	{
		private readonly ApplicationDbContext _context;
        public EmployeesController(ApplicationDbContext context)
        {
			 _context = context ;		
        }
		// Read
        public IActionResult Index()
		{
			var result = _context.Employees.Include(x=>x.Department)
				.OrderBy(x=>x.EmployeeName).ToList();
			return View(result);
		}
		// Create
		[HttpGet]
		public IActionResult Create()
		{
			ViewBag.Deparments = _context.Departments.OrderBy(x=>x.DepartmentName).ToList();
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Employee model)
        {
            UploadImage(model);

            if (ModelState.IsValid)
            {
                _context.Employees.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Deparments = _context.Departments.OrderBy(x => x.DepartmentName).ToList();
            return View();
        }




        // Edit 
        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            ViewBag.Deparments = _context.Departments.OrderBy(x => x.DepartmentName).ToList();
			var result = _context.Employees.Find(Id);
            return View("Create", result); // return to Create page and the id of spacific employee
        }

		[HttpPost]
		[ValidateAntiForgeryToken]

		public IActionResult Edit(Employee model)
		{
            UploadImage(model);
			if (ModelState.IsValid) 
			{
				_context.Employees.Update(model);
				_context.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
            ViewBag.Deparments = _context.Departments.OrderBy(x => x.DepartmentName).ToList();
			return View(model);
        }

        // Delete 

        public IActionResult Delete(int? Id)
        {
           // ViewBag.Deparments = _context.Departments.OrderBy(x => x.DepartmentName).ToList();
            var result = _context.Employees.Find(Id);
            if(result != null) 
            {
                _context.Employees.Remove(result);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index)); // return to Create page and the id of spacific employee
        }

        private void UploadImage(Employee model)
        {
            var file = HttpContext.Request.Form.Files;
            if (file.Count > 0)
            {
                //upload new Image
                // create id from Guid form abd it's is unique + GetExtension mean photeName.any extension 
                // FileName is IFormfile
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var fileStream = new FileStream(Path.Combine(@"wwwroot/", "images", ImageName), FileMode.Create);
                file[0].CopyTo(fileStream);
                // to save the image send to database will set that 
                // the image that send to database from the model of employee named "ImageName" 
                // then  model.ImageUser will be changed to  the new image name that send to database
                model.ImageUser = ImageName;
            }
            else if (model.ImageUser == null && model.EmployeeId == null) //in cause model.ImageUser return to database where it's empty and Id == null it's mean the new model is new then set the defult image
            {
                // not upload image and new employee
                model.ImageUser = "Defult.png";
            }
            else
            {
                //Edit
                model.ImageUser = model.ImageUser;
            }
        }
    }
}
