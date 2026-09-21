using System;
using BikeServicePro.Models;

namespace BikeServicePro.ViewModels.Auth
{
    /// <summary>
    /// ViewModel para la lista de usuarios
    /// </summary>
    public class UsuarioListViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public RolUsuario Rol { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? UltimoAcceso { get; set; }
        
        // Propiedades calculadas
        public string RolNombre { get; set; } = string.Empty;
        public bool EsAdmin { get; set; }
        public bool EsRecepcionista { get; set; }
        public bool EsMecanico { get; set; }
        
        // Para UI
        public string Password { get; set; } = string.Empty; // Solo para creación
        public string StatusColor => Activo ? "success" : "danger";
        public string StatusText => Activo ? "Activo" : "Inactivo";
    }
}