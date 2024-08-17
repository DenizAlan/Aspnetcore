using IdentityApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<IdentityContext>(Options=> Options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnectionString"]));

builder.Services.AddIdentity<AppUser , AppRole>().AddEntityFrameworkStores<IdentityContext>();

//Parola-User ayarlarını değiştirmek 
builder.Services.Configure<IdentityOptions>(Options=> {
    Options.Password.RequiredLength=6;
    Options.Password.RequireNonAlphanumeric=false;
    Options.Password.RequireUppercase=false;
    Options.Password.RequireLowercase=false;
    Options.Password.RequireDigit=false;

    //Email tek olsun
    Options.User.RequireUniqueEmail=true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


IdentitySeedData.IdentityTestUser(app);

app.Run();
