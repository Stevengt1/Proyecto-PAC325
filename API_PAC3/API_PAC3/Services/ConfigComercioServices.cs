using Microsoft.EntityFrameworkCore;
using API_PAC3.Data;
using API_PAC3.Models;

namespace API_PAC3.Services
{
    public class ConfigComercioServices
    {
        private readonly AppDbContext _context;

        public ConfigComercioServices(AppDbContext contex)
        {
            _context = contex;
        }

        public async Task<ConfigComercioModel> GetConfiguracionPorComercio(int idComercio)
        {
            return await _context.CONFIGURACIONES_COMERCIOS.FirstOrDefaultAsync(c => c.IdComercio == idComercio);
        }
    }
}
