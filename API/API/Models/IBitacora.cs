namespace API.Models
{
    public interface IBitacora
    {
        Task<BitacoraModel> RegistrarEvento(
            string tabla,
            string tipo,
            string descripcion,
            object? datosAnteriores = null,
            object? datosPosteriores = null,
            Exception? ex = null);
        Task<List<BitacoraModel>> GetBitacoras();
    }
}
