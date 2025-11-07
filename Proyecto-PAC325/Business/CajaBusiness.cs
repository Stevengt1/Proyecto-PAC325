using Proyecto_PAC325.Models;
using Proyecto_PAC325.Repository;

namespace Proyecto_PAC325.Business
{
    public class CajaBusiness
    {
        private readonly CajaRepository _cajaRepository;

        public CajaBusiness(CajaRepository cajaRepository)
        {
            _cajaRepository = cajaRepository;
        }

        public async Task<List<CajaModel>> GetCajasByComercio(int idComercio)
        {
            return await _cajaRepository.GetCajasByComercio(idComercio);
        }

        public async Task<List<CajaModel>> GetAllCajas()
        {
            return await _cajaRepository.GetAllCajas();
        }

        public async Task<CajaModel> GetCaja(int id)
        {
            return await _cajaRepository.GetCaja(id);
        }

        public async Task<CajaModel> Add(CajaModel caja)
        {
            caja.Estado = caja.Estado == 1 ? 1 : 0;
            caja.FechaDeRegistro = DateTime.Now;
            caja.FechaDeModificacion = DateTime.Now;

            var existeNombre = await _cajaRepository.FindByNombreAndComercio(caja.Nombre, caja.IdComercio);
            if (existeNombre != null)
            {
                caja.IdCaja = -1;
                return caja;
            }

            if (!string.IsNullOrWhiteSpace(caja.TelefonoSINPE))
            {
                var telefonoActivo = await _cajaRepository.FindActiveByTelefono(caja.TelefonoSINPE);
                if (telefonoActivo != null)
                {
                    caja.IdCaja = -2;
                    return caja;
                }
            }

            var created = await _cajaRepository.Add(caja);
            return created;
        }

        public async Task<CajaModel> Update(CajaModel caja)
        {
            caja.Estado = caja.Estado == 1 ? 1 : 0;
            caja.FechaDeModificacion = DateTime.Now;

            var existeNombre = await _cajaRepository.FindByNombreAndComercio(caja.Nombre, caja.IdComercio);
            if (existeNombre != null && existeNombre.IdCaja != caja.IdCaja)
            {
                caja.IdCaja = -1;
                return caja;
            }

            if (!string.IsNullOrWhiteSpace(caja.TelefonoSINPE))
            {
                var telefonoActivo = await _cajaRepository.FindActiveByTelefono(caja.TelefonoSINPE);
                if (telefonoActivo != null && telefonoActivo.IdCaja != caja.IdCaja)
                {
                    caja.IdCaja = -2;
                    return caja;
                }
            }

            var updated = await _cajaRepository.Update(caja);
            return updated;
        }
    }
}