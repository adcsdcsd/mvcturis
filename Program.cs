using Microsoft.EntityFrameworkCore;
using Kempery.Data; // Asegúrate que sea el namespace correcto donde está tu BaseContext
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.InteropServices;

var dllPath = Path.Combine(Directory.GetCurrentDirectory(), "wkhtmltox", "libwkhtmltox.dll");
NativeLibrary.Load(dllPath);


var builder = WebApplication.CreateBuilder(args);
// Obtener la cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DBConnection");
// pdf
builder.Services.AddSingleton<IConverter>(new SynchronizedConverter(new PdfTools()));
builder.Services.AddScoped<PdfService>();
// Registrar el DbContext con SQL Server
builder.Services.AddDbContext<BaseContext>(options =>
    options.UseSqlServer(connectionString));
//add pdf 
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
builder.Services.AddScoped<PdfService>();
builder.Services.AddScoped<ContratoService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.Run();
