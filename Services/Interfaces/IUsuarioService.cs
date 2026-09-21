using System.Collections.Generic;
using System.Threading.Tasks;
using BikeServicePro.Models;
using BikeServicePro.ViewModels.Auth;

namespace BikeServicePro.Services.Interfaces
{
    /// <summary>
    /// Interfaz para el servicio de usuarios
    /// </summary>
    public interface IUsuarioService : IService<Models.Usuario, UsuarioListViewModel>
    {
        /// <summary>
        /// Autentica un usuario
        /// </summary>
        Task<Usuario?> AuthenticateAsync(string username, string password);
        
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
        Task<IEnumerable<UsuarioListViewModel>> GetByRolAsync(RolUsuario rol);
        
        /// <summary>
        /// Cambia la contraseña de un usuario
        /// </summary>
        Task<bool> ChangePasswordAsync(int id, string oldPassword, string newPassword);
        
        /// <summary>
        /// Registra un nuevo usuario
        /// </summary>
        Task<Usuario> RegisterAsync(RegisterViewModel viewModel);
    }
}