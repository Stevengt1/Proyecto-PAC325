using Microsoft.EntityFrameworkCore;
using Proyecto_PAC325.Data;
using Proyecto_PAC325.Models;

namespace Proyecto_PAC325.Repository
{
    public class CajaRepository
    {
        private readonly AppDbContext _context;
        private IBitacora _bitacora;
        public CajaRepository(AppDbContext context, BitacoraRepository bitacora)
        {
            _context = context;
            _bitacora = bitacora;
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
            try
            {
                _context.CAJAS.Add(caja);
                if (await _context.SaveChangesAsync() > 0)
                {
                    await _bitacora.RegistrarEvento("CAJAS", "Registrar", "Se registro una caja", caja);
                    return caja;
                }
                return null;
            }catch(Exception ex)
            {
                await _bitacora.RegistrarEvento("CAJAS", "Error", "Error", ex);
                return null;
            }
        }

        public async Task<CajaModel> Update(CajaModel caja)
        {
            try
            {
                var cajaAnterior = await _context.CAJAS.AsNoTracking().FirstOrDefaultAsync(c => c.IdCaja == caja.IdCaja);
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
                    await _bitacora.RegistrarEvento("CAJAS", "Editar", "Se actualizo una caja", cajaAnterior, existing);
                    return existing;
                }
                return null;
            }catch(Exception ex)
            {
                await _bitacora.RegistrarEvento("CAJAS", "Error", "Error", ex);
                return null;
            }
        }

        public async Task<CajaModel> FindByNombreAndComercio(string nombre, int idComercio)
        {
            if (string.IsNullOrWhiteSpace(nombre)) return null;
            var n = nombre.Trim().ToLower();
            return await _context.CAJAS
                .FirstOrDefaultAsync(c => c.IdComercio == idComercio && c.Nombre.Trim().ToLower() == n);
        }

        public async Task<CajaModel> FindActiveByTelefono(string telefonoSINPE)
        {
            if (string.IsNullOrWhiteSpace(telefonoSINPE)) return null;
            return await _context.CAJAS
                .FirstOrDefaultAsync(c => c.TelefonoSINPE == telefonoSINPE && c.Estado == 1);
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