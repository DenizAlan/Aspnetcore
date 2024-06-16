using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FormApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FormApp.Controllers;

public class HomeController : Controller
{


    public HomeController()
    {

    }

    [HttpGet]
    public IActionResult Index(string searchString, string category)
    {
        var products = Repository.Products;

        //ısnullorempty bize null sa true döner o yüzden biz degilini aldık.Başa ! koyduk
        if (!string.IsNullOrEmpty(searchString))
        {

            ViewBag.searchString = searchString;
            // contains bir öğeyi içerip içermediğini kontrol etmek için kullanılan bir metottur.
            products = products.Where(p => p.Name.ToUpper().Contains(searchString.ToUpper())).ToList();
        }

        if (!string.IsNullOrEmpty(category) && category != "0")
        {
            products = products.Where(p => p.CategoryId == int.Parse(category)).ToList();
        }

        // ViewBag.Categories= new SelectList(Repository.Categories, "CategoryId" , "Name" , category);

        var model = new ProductViewModel
        {
            Products = products,
            Categories = Repository.Categories,
            SelectedCategory = category
        };

        return View(model);
    }


    [HttpGet]
    public IActionResult Create()
    {
        // ViewBag.Categories=Repository.Categories;
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
        return View();
    }

    [HttpPost]
    //"IFormFile",  HttpRequest ile gönderilen bir dosyayı temsil eden bir arayüzdür. Bu arayüz, dosya yüklemelerini işlemek için kullanılır ve yüklenen dosyanın içeriğine, adına ve boyutuna erişim sağlar.
    public async Task<IActionResult> Create(Product model, IFormFile imageFile)
    {

        var extension = "";
        if (imageFile != null)
        {
            var allowedExtension = new[] { ".jpg", ".jpeg", ".png" };
            extension = Path.GetExtension(imageFile.FileName);

            if (!allowedExtension.Contains(extension))
            {
                ModelState.AddModelError("", "Geçerli bir resim seçiniz");
            }
        }

        //“ModelState”, bir modelin durumunu ve model bağlama doğrulamasını içeren bir özelliktir. Bir form gönderildiğinde, ModelState doğrulama hatalarını saklar ve bu hataları görünüme geri göndermek için kullanılabilir.
        //Isvalid Bir modelin veya form alanının doğrulama kurallarını geçip geçmediğini belirtir.
        if (ModelState.IsValid)
        {
            
            if (imageFile != null)
            {
                var randomFileName = string.Format($"{Guid.NewGuid()}{extension}");
                   //Path.Combine ve Directory.GetCurrentDirectory() metotları,  dosya ve klasör yolları ile çalışırken kullanılır. Directory.GetCurrentDirectory() metodu, uygulamanın çalıştığı mevcut dizini döndürür. Path.Combine metodu ise, birden fazla dizeyi birleştirerek bir dosya yolu oluşturmak için kullanılır. İşte bir örnek 
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image", randomFileName);

                //FileStream sınıfı,  dosyalar üzerinde okuma, yazma ve diğer giriş/çıkış işlemleri için kullanılır. FileMode.Create parametresi, belirtilen yolda bir dosya oluşturulmasını sağlar. Eğer dosya zaten varsa, üzerine yazılır.using ifadesi, FileStream nesnesinin işi bittiğinde otomatik olarak kapatılmasını sağlar. FileMode.OpenOrCreate modu, dosyanın var olup olmadığını kontrol eder; eğer yoksa yeni bir dosya oluşturur, varsa mevcut dosyayı açar.
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                model.Image = randomFileName;
                model.ProductId = Repository.Products.Count + 1;
                Repository.CreateProduct(model);
                // return View();
                //başka bir view e dönebilmek için redirectToAction
                return RedirectToAction("Index");
            }


        }
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");

        return View(model);


    }

    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = Repository.Products.FirstOrDefault(p => p.ProductId == id);
        if (entity == null)
        {
            return NotFound();
        }
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");

        return View(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Product model, IFormFile? imageFile)
    {
        if (id != model.ProductId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            if (imageFile != null)
            {

                var extension = Path.GetExtension(imageFile.FileName);
                var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image", randomFileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                model.Image = randomFileName;
            }
            Repository.EditProduct(model);
            return RedirectToAction("Index");
        }
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
        return View(model);
    }


}
