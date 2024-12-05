using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservaNet.Entities.POCOs;

namespace ReservaNet.Entities.Interfaces
{
    public interface IServicioRepository
    {
        void CreateServicio(Servicio servicio);
        IEnumerable<Servicio> GetAll();
        void DeleteServicio(Servicio servicio);
        void UpdateServicio(Servicio servicio);
    }
}
