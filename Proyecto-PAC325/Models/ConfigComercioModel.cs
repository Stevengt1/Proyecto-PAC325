using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace Proyecto_PAC325.Models
{
    public class ConfigComercioModel
    {
        [Key]
        public int IdConfiguracion { get; set; }
        public int IdComercio { get; set; }
        public int TipoConfiguracion { get; set; }
        public int Comision { get; set; }
        public DateTime FechaDeRegistro { get; set; }
        public DateTime FechaDeModificacion { get; set; }
        public int Estado {  get; set; }
        [ForeignKey(nameof(IdComercio))]
        public ComercioModel Comercio { get; set; }

        //IdConfiguracion – int, primary key, not null (identity)
        //IdComercio – int, not null (llave foránea de la tabla Comercio)
        //TipoConfiguracion – int, not null, (1 – Plataforma, 2 – Externa, 3 – Ambas)
        //Comision – int, not null
        //FechaDeRegistro – Datetime, not null
        //FechaDeModificacion – Datetime, null
        //Estado – bit, not null
    }
}
