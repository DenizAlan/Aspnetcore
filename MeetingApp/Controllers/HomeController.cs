using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MeetingApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(){
             int saat=DateTime.Now.Hour;
                      //1.yol -->
            // var selamlama = saat > 12 ? "İyi Günler" : "Günaydın";
            // //view klasörünün altında aramaması için model oldugunu belirtmelisin
            // return View(model : selamlama);

            
                      // 2. Viewbaglı yol -->
            // ViewBag.Selamlama =saat > 12 ? "İyi Günler" : "Günaydın";
            // ViewBag.UserName="Deniz";
            // return View();

                    //3.ViewDatalı yol
            ViewData["Selamlama"]= saat > 12 ? "İyi Günler" : "Günaydın";
            ViewData["UserName"]="Deniz";
            return View();       
        }
    }
}