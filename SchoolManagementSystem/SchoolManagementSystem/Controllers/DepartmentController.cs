using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using System.Diagnostics;

namespace SchoolManagementSystem.Controllers
{
    public class DepartmentController : Controller
    {

        private readonly ApplicationDbContext applicationDbContext;

        public DepartmentController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Departments obj)
        {
            if (ModelState.IsValid)
            {
                this.applicationDbContext.Departments.Add(obj);
                this.applicationDbContext.SaveChanges();

                return RedirectToAction("Home", "Admin");
            }
                return View();
        }

        [HttpGet]
        public IActionResult View() {
            //var departments = applicationDbContext.DEPARTMENT.ToList();
            return View();  
        }
    }
}
