using Microsoft.EntityFrameworkCore;
using Proyecto_PAC325.Business;
using Proyecto_PAC325.Data;
using Proyecto_PAC325.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//El contexto de la base a la app
builder.Services.AddDbContext<AppDbContext>(
        options => options.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnection"))
    );

//Agregar los repos
builder.Services.AddScoped<ComercioRepository>();
builder.Services.AddScoped<CajaRepository>();
builder.Services.AddScoped<ISinpeRepository, SinpeRepository>();

//Agregar los Business
builder.Services.AddScoped<ComercioBusiness>();
builder.Services.AddScoped<CajaBusiness>();
builder.Services.AddScoped<SinpeBusiness>();