using API_PAC3.Data;
using API_PAC3.Models;
using Microsoft.EntityFrameworkCore;

namespace API_PAC3.Services
{
    public class ComercioServices
    {
        private readonly AppDbContext _context;

        public ComercioServices(AppDbContext contex)
        {
            _context = contex;
        }

        public async Task<ComercioModel> GetComercio(int id)
        {
            return await _context.COMERCIOS.FindAsync(id);
        }
    }
}
