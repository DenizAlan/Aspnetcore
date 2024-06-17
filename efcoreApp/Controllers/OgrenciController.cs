using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class OgrenciController : Controller
    {
        private readonly DataContext _context;
        public OgrenciController(DataContext context)
        {
            _context=context;
        }


        public async Task<IActionResult> Index()
        {
            // 1.yol
            // var ogrenciler =await _context.Ogrenciler.ToListAsync();
            // return View(ogrenciler);

            //2.kısa yol 
            return View ( await _context.Ogrenciler.ToListAsync());

        }

        public IActionResult Create ()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ogrenci model )
        {
            _context.Ogrenciler.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

            //SaveChangesAsync metodu, context üzerinde yapılan tüm değişikliklerin asenkron bir şekilde veritabanına kaydedilmesi için kullanılır. Bu metod, veritabanı işlemlerinin uygulama performansını engellememesi ve kullanıcı arayüzünün donmasını önlemek için asenkron olarak çalışır. await anahtar kelimesi ile birlikte kullanıldığında, veritabanı işlemleri tamamlanana kadar kodun geri kalanının çalışmasını bekletir ve böylece uygulamanın yanıt vermesini sağlar.
         
        }
    }
}