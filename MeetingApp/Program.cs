var builder = WebApplication.CreateBuilder(args);


//mvc şablonu ile çalışacagımızı haber verdik
builder.Services.AddControllersWithViews();
var app = builder.Build();

//static dosyaları dışarı aç
app.UseStaticFiles();
app.UseRouting();

//controller/action/id
app.MapDefaultControllerRoute();

app.Run();
