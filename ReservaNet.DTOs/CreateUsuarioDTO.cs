using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaNet.DTOs
{
    public class CreateUsuarioDTO
    {
        [Required]
        [MaxLength(50)]
        public string Nombre { get; init; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; init; }

        [Required]
        [RegularExpression("^(Admin|Cliente)$", ErrorMessage = "Rol debe ser 'Admin' o 'Cliente'.")]
        public string Rol { get; init; }

        [Required]
        public string Contraseña { get; init; }
    }
}
