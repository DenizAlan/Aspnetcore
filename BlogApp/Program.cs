using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Builder;
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


var app = builder.Build();

//wwwroot altındaki dosyalar için
app.UseStaticFiles();

SeedData.TestVerileriniDoldur(app);

app.MapControllerRoute(
    name:"post_details",
    pattern:"posts/{url}",
    defaults: new {controller = "Posts", action ="Details" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    
);

app.Run();
