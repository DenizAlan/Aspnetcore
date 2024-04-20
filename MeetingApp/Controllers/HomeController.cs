using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeetingApp.Models;
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
            // string bi bigi oldugundan view klasörünün altında aramaması için model oldugunu belirtmelisin
            // return View(model : selamlama);

            
                      // 2. Viewbaglı yol -->
            // ViewBag.Selamlama =saat > 12 ? "İyi Günler" : "Günaydın";
            // ViewBag.UserName="Deniz";
            // return View();

                    //3.ViewDatalı yol
            ViewData["Selamlama"]= saat > 12 ? "İyi Günler" : "Günaydın";
             int UserCount=Repository.Users.Where(info=>info.WillAttend==true).Count();
            //ViewData["UserName"]="Deniz";

            var meetingInfo= new MeetingInfo() {
                Id=1,
                Location="İstanbul Abc Kongre Merkezi",
                Date=new DateTime(2024,01,20,20,0,0),
                NumberOfPeople=UserCount
            };

            //string bi bilgi olmadıgı için model tanımlaması yapmana gerek yok
            return View(meetingInfo);       
        }
    }
}