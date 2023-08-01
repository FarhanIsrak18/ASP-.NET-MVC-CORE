using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.DAL;
using SchoolManagementSystem.Models;
using System.Data;

namespace SchoolManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        public const string SessionKeyName = "_Name";
        public const string SessionKeyAge = "_Age";

        private readonly ApplicationDbContext applicationDbContext;
        private readonly ILogger<AdminController> _logger;
        // private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminController(ILogger<AdminController> logger, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            this.applicationDbContext = applicationDbContext;
           
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var email = TempData["loginEmail"];
           
            var user = applicationDbContext.AllUsers.FirstOrDefault(u => u.Email == email);
           
            return View(user);
        }
        public IActionResult Home()
        {
            int avg = 0;
            int exc = 0;
            int bad = 0;

            var totalStudents = applicationDbContext.AllUsers.Count(u =>u.Role == "student");
            var totalTeachers= applicationDbContext.AllUsers.Count(u =>u.Role == "teacher");
            ViewBag.TotalStudents = totalStudents;
            ViewBag.TotalTeachers= totalTeachers;

            StudentResults gd = new StudentResults();
            DataTable dataTable = new DataTable();
            DALcrud dALCrud = new DALcrud();

            var results = dALCrud.GetResultsFromDatabase();

            foreach(var result in results)
            {
                if(result.Status == 1)
                {
                    if (result.Average >= 80)
                    {
                        exc++;
                    }
                    else if (result.Average < 80 && result.Average >=50)
                    {
                        avg++;
                    }
                    else
                    {
                        bad++;
                    }
                }
                else
                {
                    bad++;
                }
                

            }
            ViewBag.Avg = avg;
            ViewBag.Exc = exc;
            ViewBag.Bad = bad;

            avg = exc = bad = 0;

            return View();
        }

        [HttpGet]
        public IActionResult View(EditViewModel model) 
        {
            var user = applicationDbContext.AllUsers.FirstOrDefault(u => u.Id == model.Id);
            if (user != null) {

                HttpContext.Session.SetInt32("StudentId", user.Id);
                HttpContext.Session.SetString("StudentName", user.Name);
            }
            
            return View(user);
        }

        [HttpPost]
        public IActionResult View(UpdateStudentModel model)
        {
            
            var user = applicationDbContext.AllUsers.FirstOrDefault(u => u.Id == model.Id);
            if (user != null)
            {
                user.Name = model.Name;
                user.Email = model.Email;
                user.Role = model.Role;

            }

                this.applicationDbContext.SaveChanges();
                return RedirectToAction("StudentsList");
        }


        [HttpGet]
        public IActionResult Delete(EditViewModel model)
        {
            var user = applicationDbContext.AllUsers.FirstOrDefault(u => u.Id == model.Id);
            if (user != null)
            {
                applicationDbContext.AllUsers.Remove(user);
                this.applicationDbContext.SaveChanges();
                return RedirectToAction("StudentsList", "Admin");
            }
            return RedirectToAction("StudentsList", "Admin");
        }


        [HttpGet]
        public IActionResult StudentsList()
        {
            var students = applicationDbContext.AllUsers.Where(u => u.Role == "student").ToList();
            return View(students);
        }

        [HttpGet]
        public IActionResult TeachersList()
        {
            var teachers = applicationDbContext.AllUsers.Where(u => u.Role == "teacher").ToList();
            return View(teachers);
        }

        [HttpGet]
        public IActionResult ChangePassword() { 
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(EditViewModel model)
        {
            string email = HttpContext.Session.GetString("email").ToString();
            var user = applicationDbContext.AllUsers.FirstOrDefault(p => p.Email == email);
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

            if(user != null)
            {
                bool isValidPassword = BCrypt.Net.BCrypt.Verify(model.oldPassword, user.Password);
                if(!isValidPassword)
                {
                    TempData["Message"] = "You have given the wrong password!!!";
                    return RedirectToAction("ChangePassword");
                }
            }
            if (user != null)
            {
                if (model.Password != model.confirmPassword)
                {
                    TempData["matchPassword"] = "password does not match!!!";
                    return RedirectToAction("ChangePassword");
                }
            }


            
                if (user != null)
                {
                    user.Password = hashedPassword;
                    this.applicationDbContext.SaveChanges();
                }
            
            return View();
        }

        [HttpGet]
        public IActionResult AddTeacher() {
        
            return View();
        }

        [HttpPost]
        public IActionResult AddTeacher(Teachers obj)
        {
            if (ModelState.IsValid)
            {
                this.applicationDbContext.Teachers.Add(obj);
                this.applicationDbContext.SaveChanges();
                return RedirectToAction("Home", "Admin");
            }

                return View();
        }

        [HttpGet]
        public IActionResult AddDepartment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddDepartment(Departments obj)
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
        public IActionResult TeacherATDepartment() {
            List<Teachers> teachers = this.applicationDbContext.Teachers.ToList();

            // Retrieve data from Departments table
            List<Departments> departments = this.applicationDbContext.Departments.ToList();

            // Create the view model and pass the data
            TeacherAssignmentViewModel viewModel = new TeacherAssignmentViewModel
            {
                Teachers = teachers,
                Departments = departments
            };

            return View(viewModel);

        }

        [HttpPost]
        public IActionResult TeacherATDepartment(TeacherAssignments obj)
        {
            if (ModelState.IsValid)
            {
                this.applicationDbContext.TeacherAssignments.Add(obj);
                this.applicationDbContext.SaveChanges();

                return RedirectToAction("Home", "Admin");
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddGaurdian() {
            ViewBag.MSG = "insert Data";
            return View();

        }

       [HttpPost]
        public IActionResult AddGaurdian(Guardian guardian) {
            var studentName = HttpContext.Session.GetString("StudentName").ToString();
            var studentId= HttpContext.Session.GetInt32("StudentId");

            Guardian gd = new Guardian();
            DataTable dataTable = new DataTable();
            DALcrud dALCrud = new DALcrud();
            guardian.StudentName = studentName;
            guardian.StudentId = (int)studentId;
            dALCrud.InsertGuardian(guardian);
            dALCrud.updateGuardian(guardian);

            return View();
        }

        [HttpGet]
        public IActionResult UpdateGuardian(EditViewModel model) {
            var user = applicationDbContext.AllUsers.FirstOrDefault(u => u.Id == model.Id);
            HttpContext.Session.SetInt32("StudentId" , user.Id);

            return View();
        }

        [HttpPost]
        public IActionResult UpdateGuardian(Guardian guardian)
        {
           // var studentName = HttpContext.Session.GetString("StudentName").ToString();
            var studentId = HttpContext.Session.GetInt32("StudentId");

            Guardian gd = new Guardian();
            DataTable dataTable = new DataTable();
            DALcrud dALCrud = new DALcrud();
           // guardian.StudentName = studentName;
            guardian.StudentId = (int)studentId;
            //dALCrud.InsertGuardian(guardian);
            dALCrud.updateGuardian(guardian);

            return RedirectToAction("ViewGaurdian", "Admin");
        }

        [HttpGet]
        public IActionResult ViewGaurdian()
        {
            Guardian gd = new Guardian();
            DataTable dataTable = new DataTable();
            DALcrud dALCrud = new DALcrud();

           var guardians = dALCrud.GetAllGuardiansFromDatabase();

            return View(guardians);
        }

        /// <summary>
        /// [HttpGet]
        /// </summary>
        /// <param name="guardian"></param> 
        /// <returns></returns>
        [HttpGet]
        public IActionResult deleteGaurdian(Guardian guardian)
        {
            Guardian gd = new Guardian();
            DataTable dataTable = new DataTable();
            DALcrud dALCrud = new DALcrud();

            dALCrud.DeleteGuardian(guardian);

            return RedirectToAction("ViewGaurdian", "Admin");
        }

    }

}

