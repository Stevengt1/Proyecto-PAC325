using Microsoft.EntityFrameworkCore;
using Proyecto_PAC325.Business;
using Proyecto_PAC325.Data;
using Proyecto_PAC325.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//El contexto de la base a la app
builder.Services.AddDbContext<AppDbContext>(
        options=>options.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnection"))
    );

//Agregar los repos
builder.Services.AddScoped<ComercioRepository>();
builder.Services.AddScoped<CajaRepository>();

//Agregar los Business
builder.Services.AddScoped<ComercioBusiness>();
builder.Services.AddScoped<CajaBusiness>();


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
