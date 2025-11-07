using Microsoft.EntityFrameworkCore;
using Proyecto_PAC325.Data;
using Proyecto_PAC325.Models;

namespace Proyecto_PAC325.Repository
{
    public class CajaRepository
    {
        private readonly AppDbContext _context;

        public CajaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CajaModel>> GetCajasByComercio(int idComercio)
        {
            return await _context.CAJAS
                .Where(c => c.IdComercio == idComercio)
                .OrderByDescending(c => c.FechaDeRegistro)
                .ToListAsync();
        }
        public async Task<List<CajaModel>> GetAllCajas()
        {
            return await _context.CAJAS
                .OrderByDescending(c => c.FechaDeRegistro)
                .ToListAsync();
        }

        public async Task<CajaModel> GetCaja(int id)
        {
            return await _context.CAJAS.FindAsync(id);
        }

        public async Task<CajaModel> Add(CajaModel caja)
        {
            _context.CAJAS.Add(caja);
            if (await _context.SaveChangesAsync() > 0)
            {
                return caja;
            }
            return null;
        }

        public async Task<CajaModel> Update(CajaModel caja)
        {
            var existing = await _context.CAJAS.FindAsync(caja.IdCaja);
            if (existing == null) return null;

            existing.Nombre = caja.Nombre;
            existing.Descripcion = caja.Descripcion;
            existing.TelefonoSINPE = caja.TelefonoSINPE;
            existing.Estado = caja.Estado;
            existing.FechaDeModificacion = caja.FechaDeModificacion;
            existing.IdComercio = caja.IdComercio;

            _context.CAJAS.Update(existing);
            if (await _context.SaveChangesAsync() > 0)
            {
                return existing;
            }
            return null;
        }

        public async Task<CajaModel> FindByNombreAndComercio(string nombre, int idComercio)
        {
            if (string.IsNullOrWhiteSpace(nombre)) return null;
            var n = nombre.Trim().ToLower();
            return await _context.CAJAS
                .FirstOrDefaultAsync(c => c.IdComercio == idComercio && c.Nombre.Trim().ToLower() == n);
        }        
        public async Task<List<CajaModel>> ObtenerCajasActivasAsync()
        {
            return await _context.CAJAS
                .Where(c => c.Estado == 1)
                .OrderBy(c => c.Nombre)
                .ToListAsync();
        }
    }
}