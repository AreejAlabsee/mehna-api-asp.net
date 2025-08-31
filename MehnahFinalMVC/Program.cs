using MehnahFinalApi.Data;
using MehnahFinalMVC.Data;
using MehnahFinalMVC.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<AppicatDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddHttpClient<WorksApiService>(c =>
{
    c.BaseAddress = new Uri("http://localhost:7232/api/");
});

// تسجيل WorkerApiService مع HttpClient
builder.Services.AddHttpClient<WorkerApiService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:7232/api/"); // رابط API الصحيح
});

builder.Services.AddHttpClient<RatingsApiService>(c =>
{
    c.BaseAddress = new Uri("http://localhost:7232/api/"); // رابط الـ API
});

// CORS (اختياري حسب احتياجك)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// إذا تريد CORS مفعل:
app.UseCors("AllowAll");

// Map Controller Routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
