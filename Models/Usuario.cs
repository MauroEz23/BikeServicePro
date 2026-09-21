using System;

namespace BikeServicePro.Models
{
    /// <summary>
    /// Modelo de dominio que representa un usuario del sistema
    /// </summary>
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public RolUsuario Rol { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaCreacion { get; set; }
        public DateTime? UltimoAcceso { get; set; }
        public string? TokenRecuperacion { get; set; }
        public DateTime? TokenExpiracion { get; set; }
        
        // Propiedades calculadas
        public string RolNombre => Rol.ToString();
        public bool EsAdmin => Rol == RolUsuario.Administrador;
        public bool EsRecepcionista => Rol == RolUsuario.Recepcionista;
        public bool EsMecanico => Rol == RolUsuario.Mecanico;
    }

    /// <summary>
    /// Roles disponibles en el sistema
    /// </summary>
    public enum RolUsuario
    {
        Administrador = 1,
        Recepcionista = 2,
        Mecanico = 3
    }
}