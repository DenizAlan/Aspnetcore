using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormApp.Models
{
    public class Repository
    {
        private static readonly List<Product> _products=new();
        private static readonly List<Category> _categories=new();
       
       static Repository(){
        _categories.Add(new Category{CategoryId=1, Name="Telefon"});
        _categories.Add(new Category{CategoryId=2, Name="Bilgisayar"});

        _products.Add(new Product{ProductId=1,Name="IPhone 18",Price=20000, IsActive=true, Image="1.jpg", CategoryId=1});
        _products.Add(new Product{ProductId=2,Name="IPhone 19",Price=30000, IsActive=true, Image="2.jpg", CategoryId=1});
        _products.Add(new Product{ProductId=3,Name="IPhone 20",Price=40000, IsActive=true, Image="3.jpg", CategoryId=1});
        _products.Add(new Product{ProductId=3,Name="IPhone 30",Price=50000, IsActive=true, Image="4.jpg", CategoryId=1});

        _products.Add(new Product{ProductId=4,Name="Lenova",Price=80000, IsActive=true, Image="5.jpg", CategoryId=2});
        _products.Add(new Product{ProductId=5,Name="Macbook Pro",Price=50000, IsActive=true, Image="6.jpg", CategoryId=2});

       }
        public static List<Product> Products{
            get{
                return _products;
            }
        }

        public static List<Category> Categories{
            get{
                return _categories;
            }
        }
    }
}