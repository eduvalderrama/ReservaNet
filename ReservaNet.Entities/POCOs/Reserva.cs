using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaNet.Entities.POCOs
{
    public class Reserva
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public DateTime FechaHora { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }

        [Required]
        [ForeignKey("Servicio")]
        public Guid ServicioId { get; set; }

        public Servicio Servicio { get; set; }

        [Required]
        [RegularExpression("^(Pendiente|Confirmada|Cancelada)$", ErrorMessage = "El estado debe ser 'Pendiente', 'Confirmada' o 'Cancelada'.")]
        public string Estado { get; set; }
    }
}
