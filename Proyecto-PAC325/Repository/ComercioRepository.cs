using Microsoft.EntityFrameworkCore;
using Proyecto_PAC325.Data;
using Proyecto_PAC325.Models;

namespace Proyecto_PAC325.Repository
{
    public class ComercioRepository
    {
        private readonly AppDbContext _context;
        private IBitacora _bitacora;

        public ComercioRepository(AppDbContext context, BitacoraRepository bitacora, CajaRepository cajaRepository)
        {
            _context = context;
            _bitacora = bitacora;
        }

        public async Task<List<ComercioModel>> GetAllComercio()
        {
            return await _context.COMERCIOS.ToListAsync();
        }

        public async Task<ComercioModel> GetComercio(int id)
        {
            return await _context.COMERCIOS.FindAsync(id); //Esa funcion lo busca por el Id y devuelve el objeto 
        }

        public async Task<List<ComercioModel>> GetComerciosActivos()
        {
            return await _context.COMERCIOS.Where(c => c.Estado == 1).ToListAsync();
        }

        public async Task<ComercioModel> Add(ComercioModel comercio) {
            try
            {
                _context.COMERCIOS.Add(comercio);
                if (await _context.SaveChangesAsync() > 0) //Se valida que realmente se hayan dado cambios, esto devuelve 
                { // la cantidad de  entidades afectadas 0, 1, 2,3 etc.. para que lo tengan en cuenta por si no entienden
                    await _bitacora.RegistrarEvento("COMERCIOS", "Registrar", "Se registro un comercio", comercio);
                    return comercio;
                }
                return null;
            }catch(Exception ex)
            {
                await _bitacora.RegistrarEvento("COMERCIOS", "Error", "Error", ex);
                return null;
            }
        }

        public async Task<ComercioModel> Update(ComercioModel comercio)
        {
            try
            {
                var comercioAnterior = await _context.COMERCIOS.AsNoTracking().FirstOrDefaultAsync(c => c.IdComercio == comercio.IdComercio);
                _context.COMERCIOS.Update(comercio);
                if (await _context.SaveChangesAsync() > 0)
                {
                    await _bitacora.RegistrarEvento("COMERCIOS", "Editar", "Se actualizo un comercio", comercioAnterior, comercio);
                    return comercio;
                }
                return null;
            }catch(Exception ex)
            {
                await _bitacora.RegistrarEvento("COMERCIOS", "Error", "Error", ex);
                return null;
            }
        }

        public async Task<bool> ExistIdentification(String Identificacion)
        {
            if (await _context.COMERCIOS.FirstOrDefaultAsync(c => c.Identificacion == Identificacion) != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Exist(int id)
        {
            if (await _context.COMERCIOS.FindAsync(id) != null)
            {
                return true;
            }
            return false;
        }

    }
}
