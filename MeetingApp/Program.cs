var builder = WebApplication.CreateBuilder(args);


//mvc şablonu ile çalışacagımızı haber verdik
builder.Services.AddControllersWithViews();
var app = builder.Build();

//controller/action/id
app.MapDefaultControllerRoute();

app.Run();
