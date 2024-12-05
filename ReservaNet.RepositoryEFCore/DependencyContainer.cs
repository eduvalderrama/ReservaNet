using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReservaNet.Entities.Interfaces;
using ReservaNet.RepositoryEFCore.DataContext;
using ReservaNet.RepositoryEFCore.Repositories;

namespace ReservaNet.RepositoryEFCore
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddRespositories(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ReservaNetContext>(options => options.UseSqlServer(configuration.GetConnectionString("Reserva")));
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IReservaRepository, ReservaRepository>();
            services.AddScoped<IServicioRepository, ServicioRepository>();

            return services;
        }

    }
}
