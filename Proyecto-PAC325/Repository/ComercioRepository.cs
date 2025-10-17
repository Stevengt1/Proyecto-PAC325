using Microsoft.EntityFrameworkCore;
using Proyecto_PAC325.Data;
using Proyecto_PAC325.Models;

namespace Proyecto_PAC325.Repository
{
    public class ComercioRepository
    {
        private readonly AppDbContext _context;

        public ComercioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ComercioModel>> GetAllComercio()
        {
            return await _context.COMERCIO.ToListAsync();
        }

        public async Task<ComercioModel> GetComercio(int id)
        {
            return await _context.COMERCIO.FindAsync(id); //Esa funcion lo busca por el Id y devuelve el objeto 
        }

        public async Task<ComercioModel> Add(ComercioModel comercio) {

            _context.COMERCIO.Add(comercio);
            if (await _context.SaveChangesAsync() > 0) //Se valida que realmente se hayan dado cambios, esto devuelve 
            { // la cantidad de  entidades afectadas 0, 1, 2,3 etc.. para que lo tengan en cuenta por si no entienden
                return comercio;
            }
            return null;
        }

        public async Task<ComercioModel> Update(ComercioModel comercio)
        {

            _context.COMERCIO.Update(comercio);
            if (await _context.SaveChangesAsync() > 0)
            {
                return comercio;
            }
            return null;
        }

        public async Task<ComercioModel> Delete(ComercioModel comercio)
        {

            _context.COMERCIO.Remove(comercio);
            if (await _context.SaveChangesAsync() > 0)
            {
                return comercio;
            }
            return null;
        }

    }
}
