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

        //todas las cajas de un comercio
        public async Task<List<CajaModel>> GetCajasByComercio(int idComercio)
        {
            return await _cajaRepository.GetByComercioAsync(idComercio);
        }
        //obtener caja por id
        public async Task<CajaModel> GetCaja(int id)
        {
            return await _cajaRepository.GetCajaAsync(id);
        }

        //devuelve null / IdCaja == -1 / IdCaja == -2 / entidad creada
        public async Task<CajaModel> Registro(CajaModel caja)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(caja.Nombre)) return null;// Nombre obligatorio
                if (string.IsNullOrWhiteSpace(caja.TelefonoSINPE)) return null;// Teléfono obligatorio
                if (caja.TelefonoSINPE.Length < 8 || caja.TelefonoSINPE.Length > 10) return null;//telefono segun el script de la base de datos

                //verifica si el nombre de la caja ya existe para ese comercio
                if (await _cajaRepository.ExistNombreAsync(caja.IdComercio, caja.Nombre))
                {
                    return new CajaModel { IdCaja = -1 };
                }
                //verifica si el telefono sinpe ya existe y esta activo
                //de nuevo falta desarrollar sinpe
                if (caja.Estado == 1 && await _cajaRepository.ExistActivoTelefonoAsync(caja.TelefonoSINPE))
                {
                    return new CajaModel { IdCaja = -2 };
                }
                //PARAMETRO POR DEFECTO DE FECHAS PARA EVITAR ERRORES en la cireacion
                var now = DateTime.Now;
                caja.FechaDeRegistro = now;
                caja.FechaDeModificacion = now;
                if (caja.Estado != 0 && caja.Estado != 1) caja.Estado = 1;

                return await _cajaRepository.Registro(caja);
            }
            catch
            {
                return null;
            }
        }

        public async Task<CajaModel> Editar(CajaModel caja)
        {
            //en el enunciado decia lo de control de errores entonces se desarrollo en metodos try and catch
            try
            {
                var existing = await _cajaRepository.GetCajaAsync(caja.IdCaja);
                if (existing == null) return null;
                // Validaciones
                if (string.IsNullOrWhiteSpace(caja.Nombre)) return null;
                if (string.IsNullOrWhiteSpace(caja.TelefonoSINPE)) return null;
                if (caja.TelefonoSINPE.Length < 8 || caja.TelefonoSINPE.Length > 10) return null;

                if (await _cajaRepository.ExistNombreAsync(caja.IdComercio, caja.Nombre, caja.IdCaja))
                {
                    return new CajaModel { IdCaja = -1 };
                }

                if (caja.Estado == 1 && await _cajaRepository.ExistActivoTelefonoAsync(caja.TelefonoSINPE, caja.IdCaja))
                {
                    return new CajaModel { IdCaja = -2 };
                }

                //solo me permite editar lo estipulado en el enunciado del proyectl
                existing.Nombre = caja.Nombre;
                existing.Descripcion = caja.Descripcion;
                existing.TelefonoSINPE = caja.TelefonoSINPE;
                existing.Estado = caja.Estado;
                existing.FechaDeModificacion = DateTime.Now;
                return await _cajaRepository.Editar(existing);
            }
            catch
            {
                return null;
            }
        }
    }
}
