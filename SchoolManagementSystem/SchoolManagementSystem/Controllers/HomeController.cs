using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using BCrypt.Net;

namespace SchoolManagementSystem.Controllers
{
    public class HomeController : Controller
    {

        public const string SessionKeyName = "_Name";
        public const string SessionKeyAge = "_Age";

        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext applicationDbContext;
       // private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext applicationDbContext )
        {
            _logger = logger;
            this.applicationDbContext = applicationDbContext;
            
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                // Find the user with the specified email in the database.
                var user = applicationDbContext.AllUsers.FirstOrDefault(u => u.Email == model.Email);

                if (user != null)
                {
                    bool isValidPassword = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);

                    if (isValidPassword) {
                        if (user.Role == "admin")
                        {
                            HttpContext.Session.SetString("email", user.Email);
                            TempData["loginEmail"] = user.Email;
                            return RedirectToAction("Home", "Admin");
                        }
                    }
                    
                    return RedirectToAction("Registration");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(AllUsers obj)
        {
            if (ModelState.IsValid) {

                // Hash the password using bcrypt
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(obj.Password);

                // Store the hashed password in the user object
                obj.Password = hashedPassword;

                this.applicationDbContext.AllUsers.Add(obj);
                this.applicationDbContext.SaveChanges();



                return RedirectToAction("Home","Admin");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}