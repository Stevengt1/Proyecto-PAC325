using Microsoft.EntityFrameworkCore;
using Proyecto_PAC325.Business;
using Proyecto_PAC325.Data;
using Proyecto_PAC325.Models;
using Proyecto_PAC325.Repository;
using System.Text.Json.Serialization; // <-- necesario para ReferenceHandler

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        // Configuración para evitar ciclos en la serialización JSON
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

//El contexto de la base a la app
builder.Services.AddDbContext<AppDbContext>(
        options => options.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnection"))
    );

//Agregar los repos
builder.Services.AddScoped<IBitacora, BitacoraRepository>(); // interfaz + implementación
builder.Services.AddScoped<ComercioRepository>();
builder.Services.AddScoped<CajaRepository>();
builder.Services.AddScoped<SinpeRepository>();
builder.Services.AddScoped<UsuarioRepository>();

//Agregar los Business
builder.Services.AddScoped<ComercioBusiness>();
builder.Services.AddScoped<CajaBusiness>();
builder.Services.AddScoped<BitacoraBusiness>();
builder.Services.AddScoped<SinpeBusiness>();
builder.Services.AddScoped<UsuarioBusiness>();

//Lo anterior es para que se inyecten automaticamente en los constructores esto por la biblioteca de inyeccion que trae asp.net

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