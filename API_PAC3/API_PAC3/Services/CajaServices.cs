using API_PAC3.Data;
using API_PAC3.Models;
using Microsoft.EntityFrameworkCore;

namespace API_PAC3.Services
{
    public class CajaServices
    {

        private readonly AppDbContext _context;

        public CajaServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CajaModel> GetCajaByTelefono(string telefono)
        {
            try
            {
                return await _context.CAJAS.FirstOrDefaultAsync(c => c.TelefonoSINPE == telefono);
            }catch (Exception ex)
            {
                return null;
            }
        }
    }
}
