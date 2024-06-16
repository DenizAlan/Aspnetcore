using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormApp.Models
{

    // Şimdilik veri tabanı yerine 
    public class Repository
    {
        //readonly sadece okunur olur
        private static readonly List<Product> _products=new();
        private static readonly List<Category> _categories=new();
       
       static Repository(){
        _categories.Add(new Category{CategoryId=1, Name="Telefon"});
        _categories.Add(new Category{CategoryId=2, Name="Bilgisayar"});

         _products.Add(new Product {ProductId = 1, Name = "Samsung 14", Price = 40000, IsActive = true,Image = "1.jpg",CategoryId = 1 });
        _products.Add(new Product {ProductId = 2, Name = "Samsung 15", Price = 50000, IsActive = false,Image = "2.jpg",CategoryId = 1 });
         _products.Add(new Product {ProductId = 3, Name = "Samsung 16", Price = 60000, IsActive = true,Image = "3.jpg",CategoryId = 1 });
         _products.Add(new Product {ProductId = 4, Name = "Xiaomi 14", Price = 70000, IsActive = false,Image = "4.jpg",CategoryId = 1 });

        _products.Add(new Product{ProductId=5,Name="Lenova",Price=80000, IsActive=true, Image="5.jpg", CategoryId=2});
        _products.Add(new Product{ProductId=6,Name="Macbook Pro",Price=50000, IsActive=true, Image="6.jpg", CategoryId=2});

       }
        public static List<Product> Products{
            get{
                return _products;
            }
        }

        public static void CreateProduct(Product entity){
            _products.Add(entity);
        }

        public static void EditProduct(Product updatedProduct){
            var entity=_products.FirstOrDefault(p => p.ProductId== updatedProduct.ProductId);
            if(entity!=null){
                entity.Name=updatedProduct.Name;
                entity.Price=updatedProduct.Price;
                entity.Image=updatedProduct.Image;
                entity.CategoryId=updatedProduct.CategoryId;
                entity.IsActive=updatedProduct.IsActive;

            }
        }

        public static List<Category> Categories{
            get{
                return _categories;
            }
        }
    }
}