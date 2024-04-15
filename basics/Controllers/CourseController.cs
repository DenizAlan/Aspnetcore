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


          public IActionResult Details(int id)
        {

            if(id==null){
                // Redirect farklı url ye yönlendirir
                return Redirect("/course/list");
            }
           var kurs=Repository.GetById(id);

            return View(kurs);
        }

        public IActionResult List()
        {
           
            return View("CourseList", Repository.Courses);
        }

    }
}