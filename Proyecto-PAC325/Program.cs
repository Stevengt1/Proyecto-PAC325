using Microsoft.AspNetCore.Identity;
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
        // Configuraci�n para evitar ciclos en la serializaci�n JSON
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

//El contexto de la base a la app
builder.Services.AddDbContext<AppDbContext>(
        options => options.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnection"))
    );
//Para usuarios
builder.Services.AddHttpContextAccessor();

//Agregar los repos
builder.Services.AddScoped<IBitacora, BitacoraRepository>(); // interfaz + implementaci�n
builder.Services.AddScoped<ComercioRepository>();
builder.Services.AddScoped<CajaRepository>();
builder.Services.AddScoped<SinpeRepository>();
builder.Services.AddScoped<ConfigComercioRepository>();
builder.Services.AddScoped<ReporteRepository>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<AuthRepository>();

//Agregar los Business
builder.Services.AddScoped<ComercioBusiness>();
builder.Services.AddScoped<CajaBusiness>();
builder.Services.AddScoped<BitacoraBusiness>();
builder.Services.AddScoped<SinpeBusiness>();
builder.Services.AddScoped<ConfigComercioBusiness>();
builder.Services.AddScoped<ReporteBusiness>();
builder.Services.AddScoped<UsuarioBusiness>();
builder.Services.AddScoped<AuthBusiness>();

//Requerido para la autenticación
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


//Lo anterior es para que se inyecten automaticamente en los constructores esto por la biblioteca de inyeccion que trae asp.net

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;

//    var context = services.GetRequiredService<AppDbContext>();
//    context.Database.Migrate();

//    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
//    await DataSeeder.SeedRoles(roleManager);
//}

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
    pattern: "{controller=Auth}/{action=Index}/{id?}");

await app.RunAsync();