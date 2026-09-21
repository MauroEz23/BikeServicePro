using System;
using System.Collections.Generic;

namespace BikeServicePro.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        
        // CORREGIDO: Observaciones como nullable para que sea opcional
        public string? Observaciones { get; set; }
        
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; } = true;
        
        public List<Bicicleta> Bicicletas { get; set; } = new();
        public string NombreCompleto => $"{Nombre} {Apellidos}";
    }
}