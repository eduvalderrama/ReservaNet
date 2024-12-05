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
    public class ServicioRepository: IServicioRepository
    {
        readonly ReservaNetContext Context;
        public ServicioRepository(ReservaNetContext context) => Context = context;
        public void CreateServicio(Servicio servicio)
        {
            Context.Servicios.Add(servicio);
            Context.SaveChanges();
        }

        public IEnumerable<Servicio> GetAll()
        {
            return Context.Servicios.ToList();
        }

        public void DeleteServicio(Servicio servicio)
        {
            var existingServicio = Context.Servicios.Find(servicio.Id);
            if (existingServicio == null)
            {
                throw new KeyNotFoundException($"El servicio con ID {servicio.Id} no fue encontrado.");
            }

            Context.Servicios.Remove(existingServicio);
            Context.SaveChanges();
        }

        public void UpdateServicio(Servicio servicio)
        {
            var existingServicio = Context.Servicios.Find(servicio.Id);
            if (existingServicio == null)
            {
                throw new KeyNotFoundException($"El servicio con ID {servicio.Id} no fue encontrado.");
            }

            Context.Entry(existingServicio).CurrentValues.SetValues(servicio);
            Context.SaveChanges();
        }
    }
}
