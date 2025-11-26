using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Proyecto_PAC325.Models
{
    public class CajaModel
    {
        [Key]
        public int IdCaja { get; set; }

        public int IdComercio { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public string TelefonoSINPE { get; set; } = string.Empty;

        public DateTime FechaDeRegistro { get; set; }

        public DateTime? FechaDeModificacion { get; set; }
        public int Estado { get; set; }

        //esto es para que funcione el checkbox del frontend
        [NotMapped]
        public bool EstadoBool
        {
            get => Estado == 1;
            set => Estado = value ? 1 : 0;
        }
        // Propiedades de navegación
        [JsonIgnore]
        public ComercioModel Comercio { get; set; }

        [JsonIgnore]
        public List<SinpeModel> Sinpes { get; set; }

        //        `IdCaja` int (11) NOT NULL AUTO_INCREMENT,
        //`IdComercio` int (11) NOT NULL,
        //`Nombre` varchar(100) NOT NULL,
        //`Descripcion` varchar(150) NOT NULL,
        //`TelefonoSINPE` varchar(10) NOT NULL,
        //`FechaDeRegistro` datetime NOT NULL,
        //`FechaDeModificacion` datetime NOT NULL,
        //`Estado` bit(1) NOT NULL,

    }
}
