using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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


        //obtener por comercio
        public async Task<List<CajaModel>> GetByComercioAsync(int idComercio)
        {
            return await _context.CAJAS
                .Where(c => c.IdComercio == idComercio)
                .OrderByDescending(c => c.FechaDeRegistro)
                .ToListAsync();
        }
        //obtener caja por id
        public async Task<CajaModel> GetCajaAsync(int id)
        {
            return await _context.CAJAS.FindAsync(id);
        }

        //registro de cajas
        public async Task<CajaModel> Registro(CajaModel caja)
        {
            _context.CAJAS.Add(caja);
            if (await _context.SaveChangesAsync() > 0) return caja;//el guardado no puede ser nulo
            return null;
        }

        //editar cajas
        public async Task<CajaModel> Editar(CajaModel caja)
        {
            _context.CAJAS.Update(caja);
            if (await _context.SaveChangesAsync() > 0) return caja;
            return null;
        }
        //eliminar misma logica
        public async Task<CajaModel> Eliminar(CajaModel caja)
        {
            _context.CAJAS.Remove(caja);
            if (await _context.SaveChangesAsync() > 0) return caja;
            return null;
        }

        //parametro por el enunciado
        //verifica si existe el nombre de la caja para un comercio
        public async Task<bool> ExistNombreAsync(int idComercio, string nombre, int? excludeId = null)
        {
            var q = _context.CAJAS.Where(c => c.IdComercio == idComercio && c.Nombre == nombre);
            if (excludeId.HasValue) q = q.Where(c => c.IdCaja != excludeId.Value);
            return await q.AnyAsync();
        }
        //verifica si existe un telefono sinpe activo
        //esto se configura cuado el modulo sinpe este desarrollado
        public async Task<bool> ExistActivoTelefonoAsync(string telefono, int? excludeId = null)
        {
            var q = _context.CAJAS.Where(c => c.TelefonoSINPE == telefono && c.Estado == 1);
            if (excludeId.HasValue) q = q.Where(c => c.IdCaja != excludeId.Value);
            return await q.AnyAsync();
        }

    }
}
