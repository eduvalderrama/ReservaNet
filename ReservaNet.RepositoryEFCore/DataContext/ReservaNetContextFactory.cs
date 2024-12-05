using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ReservaNet.RepositoryEFCore.DataContext
{
    internal class ReservaNetContextFactory: IDesignTimeDbContextFactory<ReservaNetContext>
    {
        public ReservaNetContext CreateDbContext(string[] args)
        {
            var OptionsBuilder = new DbContextOptionsBuilder<ReservaNetContext>();
            OptionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;database=Reservas");

            return new ReservaNetContext(OptionsBuilder.Options);
        }   
    }
}
