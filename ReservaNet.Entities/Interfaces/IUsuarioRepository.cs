using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservaNet.Entities.POCOs;

namespace ReservaNet.Entities.Interfaces
{
    public interface IUsuarioRepository
    {
        void CreateUsuario(Usuario usuario);
        IEnumerable<Usuario> GetAll(Usuario usuario);

        string Login(string email, string password);
        //Task<bool> EmailExistsAsync(string email);
    }
}
