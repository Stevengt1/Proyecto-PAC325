using System;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_PAC325.Models
{
    public class CajaModel
    {
        [Key]
        public int IdCaja { get; set; }
        public int IdComercio { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }
        public String TelefonoSINPE { get; set; }
        public DateTime FechaDeRegistro { get; set; }

        public DateTime FechaDeModificacion { get; set; }

        public int Estado { get; set; }



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
