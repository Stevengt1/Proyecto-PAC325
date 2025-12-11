using API.Data;
using API.Models;
using API.Services.Business;
using API.Services.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//El contexto de la base a la app
builder.Services.AddDbContext<AppDbContext>(
        options => options.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnection"))
    );

builder.Configuration.AddJsonFile("appsettings.json");
var secretKey = builder.Configuration.GetSection("settings").GetSection("secretKey").ToString();
var keyBytes = Encoding.UTF8.GetBytes(secretKey);

// Servicios de negocio y repositorios
builder.Services.AddScoped<IBitacora, BitacoraRepository>();
builder.Services.AddScoped<UsuarioBusiness>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<ComercioBusiness>();
builder.Services.AddScoped<ComercioRepository>();
builder.Services.AddScoped<CajaBusiness>();
builder.Services.AddScoped<CajaRepository>();
builder.Services.AddScoped<SinpeBusiness>();
builder.Services.AddScoped<ReporteBusiness>();
builder.Services.AddScoped<ConfigComercioBusiness>();
builder.Services.AddScoped<SinpeRepository>();
builder.Services.AddScoped<ReporteRepository>();
builder.Services.AddScoped<ConfigComercioRepository>();

// Configuración de autenticación JWT
builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Agregar controladores
builder.Services.AddControllers();

// Swagger con soporte para JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Proyecto", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Ingrese el token JWT en el campo: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // primero autenticación
app.UseAuthorization();  // luego autorización

app.MapControllers();

app.Run();

