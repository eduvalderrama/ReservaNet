using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaNet.DTOs
{
    public class CreateServicioDTO
    {

        [Required]
        [MaxLength(50)]
        public string Nombre { get; init; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0.")]
        public decimal Precio { get; init; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La duración en minutos debe ser mayor a 0.")]
        public int DuracionMinutos { get; init; }
    }
}
