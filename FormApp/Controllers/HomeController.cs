using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FormApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FormApp.Controllers;

public class HomeController : Controller
{
    

    public HomeController()
    {
       
    }
    [HttpGet]
    public IActionResult Index(string searchString , string category)
    {
        var products= Repository.Products;
        //ısnullorempty bize null sa true döner o yüzden biz degilini aldık.Başa ! koyduk
        if(!string.IsNullOrEmpty(searchString))
        {

            ViewBag.searchString=searchString;
            // contains bir öğeyi içerip içermediğini kontrol etmek için kullanılan bir metottur.
           products = products.Where(p => p.Name.ToUpper().Contains(searchString.ToUpper())).ToList();
        }

        if(!string.IsNullOrEmpty(category) && category!="0")
        {
            products=products.Where(p=>p.CategoryId==int.Parse(category)).ToList();
        }

        // ViewBag.Categories= new SelectList(Repository.Categories, "CategoryId" , "Name" , category);

        var model = new ProductViewModel{
            Products=products,
            Categories=Repository.Categories,
            SelectedCategory=category
        };
        return View(model);
    }
    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Categories=Repository.Categories;
        return View();
    }

     [HttpPost]
    public IActionResult Create(Product model)
    {
        return View();
    }

    
}
