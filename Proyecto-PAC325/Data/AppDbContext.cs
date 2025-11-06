using Microsoft.EntityFrameworkCore;
using Proyecto_PAC325.Models;

namespace Proyecto_PAC325.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ComercioModel> COMERCIOS {  get; set; }

        // NUEVA LÍNEA: agrega la tabla de bitácora
        public DbSet<BitacoraEvento> BitacoraEventos { get; set; }


    }
}
     