using Microsoft.EntityFrameworkCore;
using API_PAC3.Models;

namespace API_PAC3.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ComercioModel> COMERCIOS {  get; set; }
        public DbSet<SinpeModel> SINPE { get; set; }
        public DbSet<ConfigComercioModel> CONFIGURACIONES_COMERCIOS { get; set; }


    }
}
