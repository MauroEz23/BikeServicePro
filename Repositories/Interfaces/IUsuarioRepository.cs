using System.Threading.Tasks;
using BikeServicePro.Models;

namespace BikeServicePro.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz para el repositorio de usuarios
    /// Extiende IRepository con operaciones específicas para Usuario
    /// </summary>
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        /// <summary>
        /// Obtiene un usuario por su nombre de usuario
        /// </summary>
        Task<Usuario?> GetByUsernameAsync(string username);
        
        /// <summary>
        /// Obtiene un usuario por su email
        /// </summary>
        Task<Usuario?> GetByEmailAsync(string email);
        
        /// <summary>
        /// Verifica si un nombre de usuario existe
        /// </summary>
        Task<bool> UsernameExistsAsync(string username);
        
        /// <summary>
        /// Verifica si un email existe
        /// </summary>
        Task<bool> EmailExistsAsync(string email);
        
        /// <summary>
        /// Actualiza el último acceso de un usuario
        /// </summary>
        Task UpdateLastAccessAsync(int id);
        
        /// <summary>
        /// Obtiene usuarios por rol
        /// </summary>
        Task<IEnumerable<Usuario>> GetByRolAsync(RolUsuario rol);
        
        /// <summary>
        /// Autentica un usuario
        /// </summary>
        Task<Usuario?> AuthenticateAsync(string username, string passwordHash);
    }
}