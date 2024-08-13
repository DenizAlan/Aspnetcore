using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<BlogContext>(options=>{
    var config=builder.Configuration;
    var connectionString= config.GetConnectionString("DefaultConnectionString");
    options.UseSqlServer(connectionString);
});

// sanal versiyonu verdiğimde gerçek versiyonu bana gönder demek
builder.Services.AddScoped<IPostRepository , EfPostRepository>();
builder.Services.AddScoped<ITagRepository , EfTagRepository>();
builder.Services.AddScoped<ICommentRepository , EfCommentRepository>();


var app = builder.Build();

//wwwroot altındaki dosyalar için
app.UseStaticFiles();

SeedData.TestVerileriniDoldur(app);

app.MapControllerRoute(
    name:"post_details",
    pattern:"posts/details/{url}",
    defaults: new {controller = "Posts", action ="Details" }
);

app.MapControllerRoute(
    name:"posts_by_tag",
    pattern:"posts/tag/{tag}",
    defaults: new {Controller="Posts", action="Index"} 
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    
);

app.Run();
