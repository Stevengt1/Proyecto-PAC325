using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_PAC325.Models
{
    public class BitacoraModel
    {
        [Key]
        public int IdEvento { get; set; }

        [Required, MaxLength(20)]
        public string TablaDeEvento { get; set; }

        [Required, MaxLength(20)]
        public string TipoDeEvento { get; set; }

        [Required]
        public DateTime FechaDeEvento { get; set; }

        [Required]
        public string DescripcionDeEvento { get; set; }

        public string? StackTrace { get; set; }
        public string? DatosAnteriores { get; set; }
        public string? DatosPosteriores { get; set; }
    }
}
