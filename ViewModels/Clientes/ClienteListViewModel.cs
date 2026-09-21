using System;

namespace BikeServicePro.ViewModels.Clientes
{
    public class ClienteListViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        
        // CORREGIDO: Observaciones como nullable
        public string? Observaciones { get; set; }
        
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
    }
}