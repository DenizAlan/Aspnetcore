using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
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

var app = builder.Build();

//wwwroot altındaki dosyalar için
app.UseStaticFiles();

SeedData.TestVerileriniDoldur(app);

app.MapDefaultControllerRoute();

app.Run();
