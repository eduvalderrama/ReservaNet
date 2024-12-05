using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReservaNet.Entities.POCOs;

namespace ReservaNet.RepositoryEFCore.DataContext
{
    public class ReservaNetContext: DbContext
    {
        public ReservaNetContext (
            DbContextOptions<ReservaNetContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Reserva> Reserva { get; set; }
    }
    
}
