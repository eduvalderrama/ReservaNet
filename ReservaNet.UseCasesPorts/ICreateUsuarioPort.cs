using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservaNet.DTOs;

namespace ReservaNet.UseCasesPorts
{
    public interface ICreateUsuarioPort
    {
        Task Handle(CreateUsuarioDTO usuario);
    }
}
