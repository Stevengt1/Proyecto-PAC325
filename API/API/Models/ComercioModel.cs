using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class ComercioModel
    {
        [Key]
        public int IdComercio { get; set; }
        public String Identificacion { get; set; }
        public int TipoIdentificacion { get; set; }
        public String Nombre { get; set; }
        public int TipoDeComercio { get; set; }
        public String Telefono { get; set; }
        public String CorreoElectronico { get; set; }
        public String Direccion { get; set; }
        public DateTime FechaDeRegistro { get; set; }
        public DateTime FechaDeModificacion { get; set; }
        public int Estado { get; set; }


        //▪ IdComercio – int, primary key, not null (identity) 
        //▪ Identificacion – varchar(30), not null 
        //▪ TipoIdentificacion – int, not null, (1 – Física, 2 – Jurídica) 
        //▪ Nombre – varchar(200), not null 
        //▪ TipoDeComercio – int, not null, (1 – Restaurantes, 2 - Supermercados, 3 – Ferreterías, 4 - Otros) 
        //▪ Telefono – varchar(20), not null 
        //▪ CorreoElectronico – varchar(200), not null 
        //▪ Direccion – varchar(500), not null 
        //▪ FechaDeRegistro – Datetime, not null 
        //▪ FechaDeModificacion – Datetime, null 
        //▪ Estado – bit, not null, (1 – Activo, 0 – Inactivo)
    }
}
