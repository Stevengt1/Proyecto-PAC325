using Microsoft.EntityFrameworkCore;
using Proyecto_PAC325.Data;
using Proyecto_PAC325.Models;

namespace Proyecto_PAC325.Repository
{
    public class ConfigComercioRepository
    {

        private readonly AppDbContext _context;
        private readonly BitacoraRepository _bitacora;

        public ConfigComercioRepository(AppDbContext context, BitacoraRepository bitacora)
        {
            _context = context;
            _bitacora = bitacora;
        }

        public async Task<List<ConfigComercioModel>> GetConfiguraciones()
        {
            return await _context.CONFIGURACIONES_COMERCIOS.Include(c => c.Comercio).ToListAsync();
        }

        public async Task<ConfigComercioModel> GetConfiguracion(int id)
        {
            return await _context.CONFIGURACIONES_COMERCIOS.FindAsync(id);
        }

        public async Task<ConfigComercioModel> Add(ConfigComercioModel config)
        {
            try
            {
                _context.CONFIGURACIONES_COMERCIOS.Add(config);
                if (await _context.SaveChangesAsync() > 0)
                {
                    await _bitacora.RegistrarEvento("CONFIGURACIONES_COMERCIOS", "Registrar", "Se registro una configuración de un comercio", config);
                    return config;
                }
                return null;
            }catch(Exception ex)
            {
                await _bitacora.RegistrarEvento("CONFIGURACIONES_COMERCIOS", "Error", "Error", ex);
                return null;
            }
        }

        public async Task<ConfigComercioModel> Update(ConfigComercioModel config)
        {
            try
            {
                var configAnterior = await _context.CONFIGURACIONES_COMERCIOS.AsNoTracking()
                        .FirstOrDefaultAsync(c => c.IdConfiguracion == config.IdConfiguracion);
                _context.CONFIGURACIONES_COMERCIOS.Update(config);
                if (await _context.SaveChangesAsync() > 0)
                {
                    await _bitacora.RegistrarEvento("CONFIGURACIONES_COMERCIOS", "Editar", "Se actualizo una configuración", 
                        configAnterior, config);
                    return config;
                }
                return null;
            }catch(Exception ex)
            {
                await _bitacora.RegistrarEvento("CONFIGURACIONES_COMERCIOS", "Error", "Error", ex);
                return null;
            }
        }

        public async Task<bool> Exist(int id)
        {
            if (await _context.CONFIGURACIONES_COMERCIOS.FindAsync(id) != null)
            {
                return true;
            }
            return false;
        }

        //Para el modulo de reportes
        //obtiene la configuracion activa del comercio
        public async Task<ConfigComercioModel?> GetActiveByComercioAsync(int idComercio, int tipoConfiguracion = 1)
        {
            return await _context.CONFIGURACIONES_COMERCIOS
                .Where(c => c.IdComercio == idComercio && c.TipoConfiguracion == tipoConfiguracion && c.Estado == 1)
                .OrderByDescending(c => c.FechaDeModificacion)
                .FirstOrDefaultAsync();
        }

    }
}
