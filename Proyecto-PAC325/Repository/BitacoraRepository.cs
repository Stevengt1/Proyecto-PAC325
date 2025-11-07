using Microsoft.EntityFrameworkCore;
using Proyecto_PAC325.Data;
using Proyecto_PAC325.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Proyecto_PAC325.Repository
{
    public class BitacoraRepository : IBitacora
    {

        private readonly AppDbContext _context;

        public BitacoraRepository (AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BitacoraModel>> GetBitacoras()
        {
            return await _context.BitacoraEventos.ToListAsync();
        }

        public async Task<BitacoraModel> Add(BitacoraModel bitacora)
        {
            _context.BitacoraEventos.Add(bitacora);
            if (_context.SaveChanges() > 0)
            {
                return bitacora;
            }
            return null;
        }

        public async Task<BitacoraModel> RegistrarEvento(string tabla, 
            string tipo, string descripcion, object? datosAnteriores = null, 
            object? datosPosteriores = null, Exception? ex = null)
        {
            var bitacora = new BitacoraModel();
            bitacora.TablaDeEvento = tabla;
            bitacora.TipoDeEvento = tipo;
            bitacora.FechaDeEvento = DateTime.Now;
            bitacora.DescripcionDeEvento = ex != null ? ex.Message : descripcion;
            bitacora.DatosAnteriores = datosAnteriores != null ? JsonSerializer.Serialize(datosAnteriores) : null;
            bitacora.DatosPosteriores = datosPosteriores != null ? JsonSerializer.Serialize(datosPosteriores) : null;
            bitacora.StackTrace = ex != null ? ex.StackTrace : string.Empty;
            _context.BitacoraEventos.Add(bitacora);
            if (_context.SaveChanges()>0)
            {
                return bitacora;
            }
            return null;
        }
    }
}
