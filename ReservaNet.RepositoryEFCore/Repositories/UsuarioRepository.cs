using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using ReservaNet.Entities.Interfaces;
using ReservaNet.Entities.POCOs;
using ReservaNet.RepositoryEFCore.DataContext;

namespace ReservaNet.RepositoryEFCore.Repositories
{
    public class UsuarioRepository: IUsuarioRepository
    {
        readonly ReservaNetContext Context;
        public UsuarioRepository(ReservaNetContext context) => Context = context;
        public void CreateUsuario(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Contraseña))
            {
                throw new ArgumentException("La contraseña no puede estar vacía.");
            }

            usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.Contraseña);

            Context.Add(usuario);
            Context.SaveChanges();
        }

        public IEnumerable<Usuario> GetAll(Usuario usuario)
        {
            if (usuario.Rol != "Admin")
            {
                throw new UnauthorizedAccessException("No tienes permisos para acceder a esta información.");
            }

            return Context.Usuarios;
        }

        public string Login(string email, string password)
        {
            var usuario = Context.Set<Usuario>().FirstOrDefault(u => u.Email == email);

            if (usuario == null)
            {
                throw new UnauthorizedAccessException("Usuario no encontrado.");
            }

            if (!BCrypt.Net.BCrypt.Verify(password, usuario.Contraseña))
            {
                throw new UnauthorizedAccessException("Contraseña incorrecta.");
            }

            return GenerateJwtToken(usuario);            
        }

        private string GenerateJwtToken(Usuario usuario)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
            new Claim(ClaimTypes.Role, usuario.Rol),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Secret"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "ReservaNet",
                audience: "",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(360),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
