﻿using Microsoft.AspNetCore.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult StudentsList()
        {
            return View();
        }
    }
}
