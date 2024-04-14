using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using basics.Models;

namespace basics.Controllers
{

    public class CourseController : Controller
    {

        public IActionResult Index()
        {
            var kurs = new Course();
            kurs.Id = 1;
            kurs.Title = "Yazılım Kursları";
            kurs.Description = " ";
            kurs.image="4yazilim.jpg";
            return View(kurs);
        }

        public IActionResult List()
        {
            var kurslar = new List<Course>()
            {
                new Course() {Id = 1, Title = "asp net core", Description = "Güzel bir kurs", image="1aspnetcore.png" },
                new Course() {Id = 2, Title = "c#", Description = "Güzel bir kurs", image="2c.png" },
                new Course() {Id = 3, Title = "javascript", Description = "Güzel bir kurs", image="3javascript.jpg" }
            };

            return View("CourseList", kurslar);
        }

    }
}