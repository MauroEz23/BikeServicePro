using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeServicePro.Models;
using BikeServicePro.Repositories.Interfaces;

namespace BikeServicePro.Repositories.Implementations.JSON
{
    /// <summary>
    /// Implementación JSON del repositorio de usuarios
    /// </summary>
    public class JsonUsuarioRepository : JsonRepositoryBase<Usuario>, IUsuarioRepository
    {
        public JsonUsuarioRepository() : base("usuarios.json")
        {
        }

        /// <summary>
        /// Obtiene un usuario por su nombre de usuario
        /// </summary>
        public async Task<Usuario?> GetByUsernameAsync(string username)
        {
            return await Task.FromResult(_entities.FirstOrDefault(u => 
                u.Username.Equals(username, System.StringComparison.OrdinalIgnoreCase) && 
                u.Activo));
        }

        /// <summary>
        /// Obtiene un usuario por su email
        /// </summary>
        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            return await Task.FromResult(_entities.FirstOrDefault(u => 
                u.Email.Equals(email, System.StringComparison.OrdinalIgnoreCase) && 
                u.Activo));
        }

        /// <summary>
        /// Verifica si un nombre de usuario existe
        /// </summary>
        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await Task.FromResult(_entities.Any(u => 
                u.Username.Equals(username, System.StringComparison.OrdinalIgnoreCase)));
        }

        /// <summary>
        /// Verifica si un email existe
        /// </summary>
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await Task.FromResult(_entities.Any(u => 
                u.Email.Equals(email, System.StringComparison.OrdinalIgnoreCase)));
        }

        /// <summary>
        /// Actualiza el último acceso de un usuario
        /// </summary>
        public async Task UpdateLastAccessAsync(int id)
        {
            var usuario = await GetByIdAsync(id);
            if (usuario != null)
            {
                usuario.UltimoAcceso = DateTime.Now;
                await UpdateAsync(usuario);
            }
        }

        /// <summary>
        /// Obtiene usuarios por rol
        /// </summary>
        public async Task<IEnumerable<Usuario>> GetByRolAsync(RolUsuario rol)
        {
            return await Task.FromResult(_entities.Where(u => u.Rol == rol && u.Activo));
        }

        /// <summary>
        /// Autentica un usuario
        /// </summary>
        public async Task<Usuario?> AuthenticateAsync(string username, string passwordHash)
        {
            return await Task.FromResult(_entities.FirstOrDefault(u => 
                u.Username.Equals(username, System.StringComparison.OrdinalIgnoreCase) && 
                u.PasswordHash == passwordHash && 
                u.Activo));
        }
    }
}