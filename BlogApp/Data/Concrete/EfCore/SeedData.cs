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
                        new Tag{Text="Web Programlama"},
                        new Tag{Text="Backend"},
                        new Tag{Text="Frontend"},
                        new Tag{Text="Fullstack"},
                        new Tag{Text="C#"}
                    );
                    context.SaveChanges();
                }

                if(!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User {UserName= "DenizAlan"},
                        new User {UserName= "YaseminÖz"}
                    );
                    context.SaveChanges();
                }

                if(!context.Posts.Any()){
                    context.Posts.AddRange(
                        new Post {
                            Title="Asp.net core",
                            Content="Asp.net core dersleri",
                            IsActive=true,
                            PublishedOn=DateTime.Now.AddDays(-10),
                            Tags=context.Tags.Take(3).ToList(),
                            UserId=1,
                        },
                          new Post {
                            Title="C#",
                            Content="C# dersleri",
                            IsActive=true,
                            PublishedOn=DateTime.Now.AddDays(-20),
                            Tags=context.Tags.Take(2).ToList(),
                            UserId=1,
                        },
                          new Post {
                            Title="Javascript",
                            Content="Javascript dersleri",
                            IsActive=true,
                            PublishedOn=DateTime.Now.AddDays(-5),
                            Tags=context.Tags.Take(4).ToList(),
                            UserId=2,
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
