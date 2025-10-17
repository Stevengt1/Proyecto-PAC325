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

    }
}
