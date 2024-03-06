using Microsoft.EntityFrameworkCore;
using TestAPP;
using TestAPP.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 추가
builder.Services.AddDbContext<TestDataContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("TestDb")));

// 세션
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 레이저 뷰
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// 세션
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// 추가
//app.Seed();

app.Run();

//Add-Migration InitialDatabase
//Remove-Migration
//Update-Database