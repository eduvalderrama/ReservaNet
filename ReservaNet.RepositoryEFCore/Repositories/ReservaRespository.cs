using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReservaNet.Entities.Interfaces;
using ReservaNet.Entities.POCOs;
using ReservaNet.RepositoryEFCore.DataContext;

namespace ReservaNet.RepositoryEFCore.Repositories
{
    public class ReservaRepository: IReservaRepository
    {
        readonly ReservaNetContext Context;
        public ReservaRepository(ReservaNetContext context) => Context = context;
        public void CrearReserva(Reserva reserva)
        {
            var servicio = Context.Servicios.Find(reserva.ServicioId);
            if (servicio == null)
            {
                throw new KeyNotFoundException("Servicio no encontrado.");
            }

            var FechaHoraFin = reserva.FechaHora.AddMinutes(servicio.DuracionMinutos);

            if (!ValidarDisponibilidad(reserva.ServicioId, reserva.FechaHora, FechaHoraFin))
            {
                throw new InvalidOperationException("El servicio no está disponible en el horario seleccionado.");
            }

            reserva.Estado = "Pendiente";
            Context.Reserva.Add(reserva);
            Context.SaveChanges();
        }

        public IEnumerable<Reserva> ObtenerReservasPorCliente(int usuarioId)
        {
            return Context.Reserva
                .Where(r => r.UsuarioId == usuarioId)
                .Include(r => r.Servicio)
                .ToList();
        }

        public IEnumerable<Reserva> ObtenerTodasReservas()
        {
            return Context.Reserva.Include(r => r.Servicio).Include(r => r.Usuario).ToList();
        }

        public void CancelarReserva(int reservaId, bool esAdmin)
        {
            var reserva = Context.Reserva.Find(reservaId);

            if (reserva == null) throw new KeyNotFoundException("Reserva no encontrada.");
            if (reserva.Estado != "Pendiente") throw new InvalidOperationException("Solo se pueden cancelar reservas pendientes.");

            reserva.Estado = esAdmin ? "Cancelada por admin" : "Cancelada por cliente";
            Context.SaveChanges();
        }

        public void ConfirmarReserva(int reservaId)
        {
            var reserva = Context.Reserva.Find(reservaId);

            if (reserva == null) throw new KeyNotFoundException("Reserva no encontrada.");
            if (reserva.Estado != "Pendiente") throw new InvalidOperationException("Solo se pueden confirmar reservas pendientes.");

            reserva.Estado = "Confirmada";
            Context.SaveChanges();
        }

        public bool ValidarDisponibilidad(Guid servicioId, DateTime fechaInicio, DateTime fechaFin)
        {
            return !Context.Reserva.Any(r =>
                r.ServicioId == servicioId &&
                r.Estado == "Pendiente" &&
                r.FechaHora < fechaFin &&
                r.FechaHora.AddMinutes(r.Servicio.DuracionMinutos) > fechaInicio);
        }
    }
}
