using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ComercioModel> COMERCIOS { get; set; }

        public DbSet<CajaModel> CAJAS { get; set; }

        public DbSet<BitacoraModel> BitacoraEventos { get; set; }

        public DbSet<SinpeModel> SINPE { get; set; }
        public DbSet<UsuarioModel> USUARIOS { get; set; }


        public DbSet<ConfigComercioModel> CONFIGURACIONES_COMERCIOS { get; set; }

        public DbSet<ReporteMensualModel> REPORTES_MENSUALES { get; set; }

    }
}
