using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaNet.Entities.POCOs
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^(Admin|Cliente)$", ErrorMessage = "Rol debe ser 'Admin' o 'Cliente'.")]
        public string Rol { get; set; }

        [Required]
        public string Contraseña { get; set; }
    }
}
