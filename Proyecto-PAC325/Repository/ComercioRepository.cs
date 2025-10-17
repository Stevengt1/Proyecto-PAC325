using Microsoft.EntityFrameworkCore;
using Proyecto_PAC325.Data;
using Proyecto_PAC325.Models;

namespace Proyecto_PAC325.Repository
{
    public class ComercioRepository
    {
        private readonly AppDbContext _context;

        public ComercioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ComercioModel>> GetAllComercio()
        {
            return await _context.COMERCIO.ToListAsync();
        }

    }
}
