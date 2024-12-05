using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservaNet.Entities.POCOs;

namespace ReservaNet.Entities.Interfaces
{
    public interface IReservaRepository
    {
        void CrearReserva(Reserva reserva);
        IEnumerable<Reserva> ObtenerReservasPorCliente(int usuarioId);
        IEnumerable<Reserva> ObtenerTodasReservas();
        void CancelarReserva(int reservaId, bool esAdmin);
        void ConfirmarReserva(int reservaId);
        bool ValidarDisponibilidad(Guid servicioId, DateTime fechaInicio, DateTime fechaFin);
    }
}
