using Microsoft.EntityFrameworkCore;
using Proyecto_PAC325.Data;
using Proyecto_PAC325.Models;

namespace Proyecto_PAC325.Repository
{
    public class ConfigComercioRepository
    {

        private readonly AppDbContext _context;

        public ConfigComercioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ConfigComercioModel>> GetConfiguraciones()
        {
            return await _context.CONFIGURACIONES_COMERCIOS.ToListAsync();
        }

        public async Task<ConfigComercioModel> GetConfiguracion(int id)
        {
            return await _context.CONFIGURACIONES_COMERCIOS.FindAsync(id);
        }

        public async Task<ConfigComercioModel> Add(ConfigComercioModel config)
        {
            _context.CONFIGURACIONES_COMERCIOS.Add(config);
            if(await _context.SaveChangesAsync() > 0)
            {
                return config;
            }
            return null;
        }

        public async Task<ConfigComercioModel> Update(ConfigComercioModel config)
        {
            _context.CONFIGURACIONES_COMERCIOS.Update(config);
            if (await _context.SaveChangesAsync() > 0)
            {
                return config;
            }
            return null;
        }

        public async Task<bool> Exist(int id)
        {
            if (await _context.CONFIGURACIONES_COMERCIOS.FindAsync(id) != null)
            {
                return true;
            }
            return false;
        }

    }
}
