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

        
    }
}
