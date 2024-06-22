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

        public async  Task<IActionResult> Edit (int? id)
        {
            if( id == null){
                return NotFound();
            }

            var ogr=await _context
                            .Ogrenciler
                            .Include(o=>o.KursKayitlari)
                            .ThenInclude(o=>o.Kurs)
                            .FirstOrDefaultAsync(o=>o.OgrenciId==id);

            if( ogr == null ){
                return NotFound();
            }

            return View(ogr);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit ( int id , Ogrenci model)
        {
            if( id != model.OgrenciId)
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
                    if(!_context.Ogrenciler.Any(o=> o.OgrenciId==model.OgrenciId)){
                        return NotFound();
                    }else{
                        throw;
                    }
                }

                return RedirectToAction("Index");
            }

            return View(model);
        }
   
   
        [HttpGet]
        public async Task<IActionResult> Delete ( int? id)
        {
            if( id == null){
                return NotFound();
            }

            var ogr =await _context.Ogrenciler.FindAsync(id);

            if (ogr== null){
                return NotFound();
            }

            return View(ogr);
        }

        [HttpPost]
         public async Task<IActionResult> Delete ([FromForm]int id)
         {
            var ogr= await _context.Ogrenciler.FindAsync(id);
            if(ogr==null) return NotFound();
            _context.Ogrenciler.Remove(ogr);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
         }

    }

    //FindAsync metodu, bir varlığı (entity) asenkron olarak birincil anahtar değerlerine göre bulmak için kullanılır. Bu metod, veritabanında belirli bir anahtar değerine sahip bir kaydı ararken kullanışlıdır ve eğer kayıt bulunursa, ilgili varlık nesnesini döndürür
    //var ogrenci = await _context.Ogrenciler.FindAsync(id);


    // FirstOrDefault metodu, bir dizinin ilk elemanını döndüren bir LINQ genişletme metodudur. Eğer dizide hiç eleman yoksa varsayılan bir değer döndürür. Bu metod, bir koşula uyan ilk elemanı almak veya eğer böyle bir eleman yoksa null (veya tipin varsayılan değeri) döndürmek için kullanılır. Örneğin:var ogrenci = _context.Ogrenciler.FirstOrDefault(o => o.Id == id);

    //Any metodu, bir dizideki herhangi bir elemanın belirli bir koşulu sağlayıp sağlamadığını kontrol etmek için kullanılan bir LINQ genişletme metodudur. Eğer dizide koşulu sağlayan en az bir eleman varsa true, yoksa false döndürür. Örneğin:
    //bool varMi = _context.Ogrenciler.Any(o => o.Id == id);

    //throw kullanıldığında mevcut yürütme durdurulur ve kontrol, uygun bir hata işleyiciye (exception handler) aktarılır. Eğer bir hata işleyici bulunamazsa, programın çalışması sona erer ve hata bilgisi gösterilir. Bu, programın beklenmeyen bir durumla karşılaştığında güvenli bir şekilde durmasını sağlar.

   //ValidateAntiForgeryToken özelliği, Cross-Site Request Forgery (CSRF) saldırılarını önlemek için kullanılır. Bu özellik, form gönderildiğinde sunucunun bir CSRF token’ı kontrol etmesini sağlar ve bu token, formun güvenilir bir kaynaktan geldiğini doğrular. Eğer token eşleşmezse veya eksikse, istek reddedilir. ders67

   //[FromForm] özelliği, bir parametrenin istek gövdesinden form verileri kullanılarak bağlanması gerektiğini belirtir. 




    
}