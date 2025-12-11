using Microsoft.EntityFrameworkCore;
using Proyecto_PAC325.Data;
using Proyecto_PAC325.Models;
using System.Text.Json;

namespace Proyecto_PAC325.Repository
{
    public class BitacoraRepository : IBitacora
    {
        private readonly AppDbContext _context;

        public BitacoraRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BitacoraModel>> GetBitacoras()
        {
            return await _context.BitacoraEventos.ToListAsync();
        }

        public async Task<BitacoraModel?> Add(BitacoraModel bitacora)
        {
            try
            {
                _context.BitacoraEventos.Add(bitacora);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return bitacora;
                }
                return null;
            }
            catch (Exception ex)
            {
                // Si falla la bitácora, no queremos romper la app principal
                Console.WriteLine($"Error al guardar bitácora: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Registra un evento en la bitácora con datos anteriores y posteriores.
        /// </summary>
        public async Task<BitacoraModel?> RegistrarEvento(
            string tabla,
            string tipo,
            string descripcion,
            object? datosAnteriores = null,
            object? datosPosteriores = null,
            Exception? ex = null)
        {
            try
            {
                var bitacora = new BitacoraModel
                {
                    TablaDeEvento = tabla,
                    TipoDeEvento = tipo,
                    FechaDeEvento = DateTime.Now,
                    DescripcionDeEvento = ex != null ? ex.Message : descripcion,
                    DatosAnteriores = datosAnteriores != null ? JsonSerializer.Serialize(datosAnteriores) : null,
                    DatosPosteriores = datosPosteriores != null ? JsonSerializer.Serialize(datosPosteriores) : null,
                    StackTrace = ex?.StackTrace ?? string.Empty
                };

                _context.BitacoraEventos.Add(bitacora);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return bitacora;
                }
                return null;
            }
            catch (Exception e)
            {
                // Evitamos que un fallo en la bitácora detenga la aplicación
                Console.WriteLine($"Error al registrar evento en bitácora: {e.Message}");
                return null;
            }
        }
    }
}