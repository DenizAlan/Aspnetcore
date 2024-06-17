using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class KursKayitController : Controller
    {

        private readonly DataContext _context;

        public KursKayitController(DataContext context)
        {
            _context=context;
        }

        public async Task<IActionResult> Index()
        {
            var kursKayitleri=await _context
                                    .KursKayitlari
                                    .Include(x=>x.Ogrenci)
                                    .Include(x=>x.Kurs)
                                    .ToListAsync();
            return View(kursKayitleri);
        }

        public async Task<IActionResult> Create()
        {
            //Select kurusunun anlatacagı dile çevirdik
            ViewBag.Ogrenciler=new SelectList( await _context.Ogrenciler.ToListAsync() , "OgrenciId" ,"AdSoyad");

            ViewBag.Kurslar=new SelectList(await _context.Kurslar.ToListAsync(), "KursId" , "Baslik");
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
         public async Task<IActionResult> Create(KursKayit model)
        {
            model.KayitTarihi=DateTime.Now;
           _context.KursKayitlari.Add(model);
           await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
    }
}



// Include metodu, sorgu sonuçlarına dahil edilecek ilişkili verileri belirtmek için kullanılır. Bu metod, bir varlıkla ilişkili başka bir varlığın veya koleksiyonun yüklenmesini sağlar, böylece tek bir sorgu ile ilişkili verilere erişilebilir.