using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace efcoreApp.Controllers
{
   
    public class OgretmenController : Controller
    {
       private readonly DataContext _context ;
        public OgretmenController(DataContext context)
        {
            _context=context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ogretmenler.ToListAsync());
        }

        
        public IActionResult Create ()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ogretmen model )
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
   
   
         public async  Task<IActionResult> Edit (int? id)
        {
            if( id == null){
                return NotFound();
            }

            var ogr=await _context
                            .Ogretmenler
                            .FirstOrDefaultAsync(o=>o.OgretmenId==id);

            if( ogr == null ){
                return NotFound();
            }

            return View(ogr);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit ( int id , Ogretmen model)
        {
            if( id != model.OgretmenId)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                try{
                    _context.Update(model);  //Güncelleme yapılıyor
                    await _context.SaveChangesAsync(); //Bu satırda Veri tabanına aktarılıyor,veri tabanı güncellenmiş oluyor
                }
                catch(DbUpdateConcurrencyException)
                {
                    //Bu ögrenci veri tabanında yoksa
                    if(!_context.Ogretmenler.Any(o=> o.OgretmenId==model.OgretmenId)){
                        return NotFound();
                    }else{
                        throw;
                    }
                }

                return RedirectToAction("Index");
            }

            return View(model);
        }
   
   
   
   
    }
}