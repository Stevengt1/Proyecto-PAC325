using Microsoft.EntityFrameworkCore;
using Proyecto_PAC325.Models;
using Proyecto_PAC325.Repository;

namespace Proyecto_PAC325.Business
{
    public class ComercioBusiness
    {

        private readonly ComercioRepository _comercioRepository;

        public ComercioBusiness (ComercioRepository comercioRepository)
        {
            _comercioRepository = comercioRepository;
        }

        public async Task<List<ComercioModel>> GetAllComercio()
        {
            return await _comercioRepository.GetAllComercio();
        }

        public async Task<ComercioModel> GetComercio(int id)
        {
            return await _comercioRepository.GetComercio(id);
        }

        public async Task<List<ComercioModel>> GetComerciosActivos()
        {
            return await _comercioRepository.GetComerciosActivos();
        }
        public async Task<ComercioModel> Add(ComercioModel comercio)
        {
            if (await _comercioRepository.ExistIdentification(comercio.Identificacion)) //Esta es la funcion que cree
            { // para ver que no exista ell comercio con esa identidad
                comercio.IdComercio = -1;
                return comercio;
            }
            comercio.FechaDeModificacion = DateTime.Now; //Se asigna la fecha en que se realizo
            comercio.FechaDeRegistro = DateTime.Now; //Se asigna la fecha en que se realizo
            comercio.Estado = 1; //Este es el activo
            return await _comercioRepository.Add(comercio);
        }

        public async Task<ComercioModel> Update(ComercioModel comercio)
        {
            comercio.FechaDeModificacion = DateTime.Now;
            return await _comercioRepository.Update(comercio);
        }

    }
}
