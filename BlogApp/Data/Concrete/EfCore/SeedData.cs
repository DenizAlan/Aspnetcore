using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;


namespace BlogApp.Data.Concrete.EfCore
{
    public static class SeedData
    {
        public static void TestVerileriniDoldur(IApplicationBuilder app)
        {
            var context=app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();
            if(context !=null )
            {
              
                if(context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                if(!context.Tags.Any())
                {
                    context.Tags.AddRange(
                        new Tag{Text="İç Mekan Bitkileri" , Url="İç Mekan Bitkileri" , Color=TagColors.primary },
                        new Tag{Text="Dış Mekan bitkileri" , Url="Dış Mekan Bitkileri" , Color=TagColors.danger},
                        new Tag{Text="Orkide" , Url="Orkide", Color=TagColors.warning},
                        new Tag{Text="Çiçekli Bitkiler" , Url="Çiçekli Bitkiler" , Color=TagColors.secondary},
                        new Tag{Text="Çiçeksiz Bitkiler" , Url="Çiçeksiz Bitkiler" , Color=TagColors.success}
                    );
                    context.SaveChanges();
                }

                if(!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User {UserName= "DenizAlan" , Image="1.png"},
                        new User {UserName= "YaseminÖz" , Image="2.png"}
                    );
                    context.SaveChanges();
                }

                if(!context.Posts.Any()){
                    context.Posts.AddRange(
                        new Post {
                            Title="İç Mekan ",
                            Content="İç Mekan Bitkileri",
                            Url="iç-mekan-bitkileri",
                            IsActive=true,
                            PublishedOn=DateTime.Now.AddDays(-10),
                            Tags=context.Tags.Take(1).ToList(),
                            Image="icmekan.jpg",
                            UserId=1,
                            Comments= new List<Comment>
                                    {
                                        new Comment { Text= "Çok güzeller" , PublishedOn=new DateTime(), UserId=1},
                                        new Comment { Text= "Harika" , PublishedOn=new DateTime(), UserId=2}
                                    }
                        },
                          new Post {
                            Title="Dış Mekan",
                            Content="Dış Mekan bitkileri",
                            Url="Dıs-mekan-bitkileri",
                            IsActive=true,
                            PublishedOn=DateTime.Now.AddDays(-20),
                            Tags=context.Tags.Take(2).ToList(),
                            Image="dismekan.jpg",
                            UserId=2,
                        },
                         new Post {
                            Title="Orkide",
                            Content="Orkide",
                            Url="orkide",
                            IsActive=true,
                            PublishedOn=DateTime.Now.AddDays(-8),
                            Tags=context.Tags.Take(3).ToList(),
                            Image="orkide.jpg",
                            UserId=1,                         
                        },
                        new Post {
                            Title="Çiçekli bitkiler",
                            Content="Çiçekli bitkiler",
                            Url="cicekli-bitkiler",
                            IsActive=true,
                            PublishedOn=DateTime.Now.AddDays(-23),
                            Tags=context.Tags.Take(1).ToList(),
                            Image="dismekan.jpg",
                            UserId=2                       
                         },
                          new Post {
                            Title="Çiçeksiz bitkiler",
                            Content="Çiçeksiz bitkiler",
                            Url="ciceksiz-bitkiler",
                            IsActive=true,
                            PublishedOn=DateTime.Now.AddDays(-3),
                            Tags=context.Tags.Take(5).ToList(),
                            Image="dismekan.jpg",
                            UserId=2                       
                         }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}








// app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>` ifadesi, ASP.NET Core uygulamalarında veritabanı işlemleri için kullanılan bir `BlogContext` nesnesini almak için kullanılır. İşte bu ifadenin anlamı:
// - `app.ApplicationServices`: Bu, ASP.NET Core uygulamasının hizmet sağlayıcısını temsil eder. Hizmet sağlayıcısı, uygulama içindeki hizmetleri (servisleri) yönetir.
// - `.CreateScope()`: Bu, yeni bir kapsam (scope) oluşturur. Kapsam, bir işlem veya istek süresince geçerli olan bir hizmetler koleksiyonunu temsil eder.
// - `.ServiceProvider`: Bu, kapsamın hizmet sağlayıcısını temsil eder. Yani, bu kapsam içindeki hizmetleri almak için kullanılır.
// - `.GetService<BlogContext>()`: Bu, `BlogContext` türündeki bir hizmeti alır. `BlogContext`, veritabanı işlemleri için kullanılan bir sınıf veya hizmet olabilir.
// Bu ifade, veritabanı işlemleri yapmak için `BlogContext` nesnesini almak üzere kullanılır. Örneğin, veritabanından veri çekmek veya veri eklemek gibi işlemlerde bu nesneyi kullanabilirsiniz.

//GetPendingMigrations, Entity Framework Core’da bir veritabanında henüz uygulanmamış olan bekleyen (pending) göçleri (migrations) almak için kullanılır
