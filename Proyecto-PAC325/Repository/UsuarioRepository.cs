using Microsoft.EntityFrameworkCore;
using Proyecto_PAC325.Data;
using Proyecto_PAC325.Models;

namespace Proyecto_PAC325.Repository
{
    public class UsuarioRepository
    {
        private readonly AppDbContext _context;
        private readonly IBitacora _bitacora;

        public UsuarioRepository(AppDbContext context, IBitacora bitacora)
        {
            _context = context;
            _bitacora = bitacora;
        }

        public async Task<List<UsuarioModel>> GetAllUsuarios()
        {
            return await _context.USUARIOS.ToListAsync();
        }

        public async Task<UsuarioModel> GetUsuario(int id)
        {
            return await _context.USUARIOS.FindAsync(id);
        }

        public async Task<UsuarioModel> Add(UsuarioModel usuario)
        {
            try
            {
                var existe = await _context.USUARIOS
                    .FirstOrDefaultAsync(u => u.Identificacion == usuario.Identificacion);

                if (existe != null)
                {
                    Console.WriteLine("Usuario duplicado");
                    return null;
                }

                usuario.FechaDeRegistro = DateTime.Now;
                usuario.FechaDeModificacion = usuario.FechaDeRegistro;
                usuario.Estado = true;

                _context.USUARIOS.Add(usuario);
                var rows = await _context.SaveChangesAsync();
                Console.WriteLine($"Filas afectadas: {rows}");

                if (rows > 0)
                {
                    await _bitacora.RegistrarEvento("USUARIOS", "Registrar", "Se registró un usuario", usuario);
                    return usuario;
                }

                Console.WriteLine("No se insertó nada en la BD");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Add: {ex.Message}");
                await _bitacora.RegistrarEvento("USUARIOS", "Error", "Error al registrar usuario", null, null, ex);
                return null;
            }
        }

        public async Task<UsuarioModel> Update(UsuarioModel usuario)
        {
            try
            {
                var existente = await _context.USUARIOS.FindAsync(usuario.IdUsuario);
                if (existente == null) return null;
                                
                var datosAnteriores = new UsuarioModel
                {
                    IdUsuario = existente.IdUsuario,
                    Nombres = existente.Nombres,
                    PrimerApellido = existente.PrimerApellido,
                    SegundoApellido = existente.SegundoApellido,
                    Identificacion = existente.Identificacion,
                    CorreoElectronico = existente.CorreoElectronico,
                    IdComercio = existente.IdComercio,
                    Estado = existente.Estado,
                    FechaDeRegistro = existente.FechaDeRegistro,
                    FechaDeModificacion = existente.FechaDeModificacion
                };

                // Actualizar con los nuevos datos
                existente.Nombres = usuario.Nombres;
                existente.PrimerApellido = usuario.PrimerApellido;
                existente.SegundoApellido = usuario.SegundoApellido;
                existente.Identificacion = usuario.Identificacion;
                existente.CorreoElectronico = usuario.CorreoElectronico;
                existente.IdComercio = usuario.IdComercio;
                existente.Estado = usuario.Estado;
                existente.FechaDeModificacion = DateTime.Now;

                _context.USUARIOS.Update(existente);
                var rows = await _context.SaveChangesAsync();

                if (rows > 0)
                {                    
                    await _bitacora.RegistrarEvento(
                        "USUARIOS",
                        "Editar",
                        "Se editó un usuario",
                        datosAnteriores,
                        existente
                    );
                    return existente;
                }

                return null;
            }
            catch (Exception ex)
            {
                await _bitacora.RegistrarEvento("USUARIOS", "Error", "Error al editar usuario", null, null, ex);
                return null;
            }
        }


    }
}
