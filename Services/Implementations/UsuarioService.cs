using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeServicePro.Models;
using BikeServicePro.Repositories.Interfaces;
using BikeServicePro.Services.Interfaces;
using BikeServicePro.ViewModels.Auth;
using BikeServicePro.Helpers;

namespace BikeServicePro.Services.Implementations
{
    /// <summary>
    /// Implementación del servicio de usuarios
    /// </summary>
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Obtiene todos los usuarios como ViewModels
        /// </summary>
        public async Task<IEnumerable<UsuarioListViewModel>> GetAllAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return usuarios.Select(u => MapToUsuarioListViewModel(u));
        }

        /// <summary>
        /// Obtiene un usuario por su ID
        /// </summary>
        public async Task<UsuarioListViewModel?> GetByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            return usuario != null ? MapToUsuarioListViewModel(usuario) : null;
        }

        /// <summary>
        /// Crea un nuevo usuario
        /// </summary>
        public async Task<UsuarioListViewModel> CreateAsync(UsuarioListViewModel viewModel)
        {
            var usuario = new Usuario
            {
                Username = viewModel.Username,
                PasswordHash = SeedDataHelper.HashPassword(viewModel.Password),
                NombreCompleto = viewModel.NombreCompleto,
                Email = viewModel.Email,
                Rol = viewModel.Rol,
                Activo = true,
                FechaCreacion = DateTime.Now
            };

            var created = await _usuarioRepository.AddAsync(usuario);
            return MapToUsuarioListViewModel(created);
        }

        /// <summary>
        /// Registra un nuevo usuario
        /// </summary>
        public async Task<Usuario> RegisterAsync(RegisterViewModel viewModel)
        {
            var usuario = new Usuario
            {
                Username = viewModel.Username,
                PasswordHash = SeedDataHelper.HashPassword(viewModel.Password),
                NombreCompleto = viewModel.NombreCompleto,
                Email = viewModel.Email,
                Rol = viewModel.Rol,
                Activo = true,
                FechaCreacion = DateTime.Now
            };

            return await _usuarioRepository.AddAsync(usuario);
        }

        /// <summary>
        /// Actualiza un usuario existente
        /// </summary>
        public async Task UpdateAsync(UsuarioListViewModel viewModel)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(viewModel.Id);
            if (usuario == null) throw new Exception($"Usuario con ID {viewModel.Id} no encontrado");

            usuario.Username = viewModel.Username;
            usuario.NombreCompleto = viewModel.NombreCompleto;
            usuario.Email = viewModel.Email;
            usuario.Rol = viewModel.Rol;
            usuario.Activo = viewModel.Activo;

            await _usuarioRepository.UpdateAsync(usuario);
        }

        /// <summary>
        /// Elimina un usuario
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            await _usuarioRepository.DeleteAsync(id);
        }

        /// <summary>
        /// Verifica si un usuario existe
        /// </summary>
        public async Task<bool> ExistsAsync(int id)
        {
            return await _usuarioRepository.ExistsAsync(id);
        }

        /// <summary>
        /// Autentica un usuario
        /// </summary>
        public async Task<Usuario?> AuthenticateAsync(string username, string password)
        {
            var passwordHash = SeedDataHelper.HashPassword(password);
            return await _usuarioRepository.AuthenticateAsync(username, passwordHash);
        }

        /// <summary>
        /// Obtiene un usuario por su nombre de usuario
        /// </summary>
        public async Task<Usuario?> GetByUsernameAsync(string username)
        {
            return await _usuarioRepository.GetByUsernameAsync(username);
        }

        /// <summary>
        /// Obtiene un usuario por su email
        /// </summary>
        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            return await _usuarioRepository.GetByEmailAsync(email);
        }

        /// <summary>
        /// Verifica si un nombre de usuario existe
        /// </summary>
        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _usuarioRepository.UsernameExistsAsync(username);
        }

        /// <summary>
        /// Verifica si un email existe
        /// </summary>
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _usuarioRepository.EmailExistsAsync(email);
        }

        /// <summary>
        /// Actualiza el último acceso de un usuario
        /// </summary>
        public async Task UpdateLastAccessAsync(int id)
        {
            await _usuarioRepository.UpdateLastAccessAsync(id);
        }

        /// <summary>
        /// Obtiene usuarios por rol
        /// </summary>
        public async Task<IEnumerable<UsuarioListViewModel>> GetByRolAsync(RolUsuario rol)
        {
            var usuarios = await _usuarioRepository.GetByRolAsync(rol);
            return usuarios.Select(u => MapToUsuarioListViewModel(u));
        }

        /// <summary>
        /// Cambia la contraseña de un usuario
        /// </summary>
        public async Task<bool> ChangePasswordAsync(int id, string oldPassword, string newPassword)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null) return false;

            var oldPasswordHash = SeedDataHelper.HashPassword(oldPassword);
            if (usuario.PasswordHash != oldPasswordHash) return false;

            usuario.PasswordHash = SeedDataHelper.HashPassword(newPassword);
            await _usuarioRepository.UpdateAsync(usuario);
            return true;
        }

        /// <summary>
        /// Mapea un Usuario a UsuarioListViewModel
        /// </summary>
        private UsuarioListViewModel MapToUsuarioListViewModel(Usuario usuario)
        {
            return new UsuarioListViewModel
            {
                Id = usuario.Id,
                Username = usuario.Username,
                NombreCompleto = usuario.NombreCompleto,
                Email = usuario.Email,
                Rol = usuario.Rol,
                Activo = usuario.Activo,
                FechaCreacion = usuario.FechaCreacion,
                UltimoAcceso = usuario.UltimoAcceso,
                RolNombre = usuario.RolNombre,
                EsAdmin = usuario.EsAdmin,
                EsRecepcionista = usuario.EsRecepcionista,
                EsMecanico = usuario.EsMecanico
            };
        }
    }
}