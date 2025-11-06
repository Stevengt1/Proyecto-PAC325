using Microsoft.AspNetCore.Mvc;

using System;
using Proyecto_PAC325.Data;
using Proyecto_PAC325.Models;
using Newtonsoft.Json;

namespace Proyecto_PAC325.Models
{
    public static class BitacoraHelper
    {
        public static void RegistrarEvento(
            AppDbContext context,
            string tabla,
            string tipo,
            string descripcion,
            object? datosAnteriores = null,
            object? datosPosteriores = null,
            Exception? ex = null)
        {
            var evento = new BitacoraEvento
            {
                TablaDeEvento = tabla,
                TipoDeEvento = tipo,
                FechaDeEvento = DateTime.Now,
                DescripcionDeEvento = descripcion,
                StackTrace = ex?.StackTrace ?? string.Empty,
                DatosAnteriores = datosAnteriores != null ? JsonConvert.SerializeObject(datosAnteriores) : null,
                DatosPosteriores = datosPosteriores != null ? JsonConvert.SerializeObject(datosPosteriores) : null
            };

            context.BitacoraEventos.Add(evento);
            context.SaveChanges();
        }
    }
} 

