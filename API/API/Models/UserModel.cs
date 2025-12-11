using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class UserModel
    {
        /*
         NumUser INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
        Correo VARCHAR(50),
        Clave VARCHAR(50)
        */
        [Key]
        public int NumUser { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
    }
}
