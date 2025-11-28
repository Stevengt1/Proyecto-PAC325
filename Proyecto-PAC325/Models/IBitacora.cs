using Microsoft.AspNetCore.Mvc;
using System;
using Proyecto_PAC325.Models;


namespace Proyecto_PAC325.Models
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

