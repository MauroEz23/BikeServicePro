using System;

namespace BikeServicePro.Models
{
    /// <summary>
    /// Modelo de dominio que representa una bicicleta de ruta
    /// </summary>
    public class Bicicleta
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int? Anio { get; set; }
        public string NumeroSerie { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public double? Peso { get; set; }
        public string Talla { get; set; } = string.Empty;
        public string Grupo { get; set; } = string.Empty;
        public string TipoFrenos { get; set; } = string.Empty;
        public string? FotoUrl { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; } = true;
        
        // Relaciones
        public Cliente Cliente { get; set; } = null!;
        
        // Propiedad calculada
        public string NombreCompleto => $"{Marca} {Modelo} ({Anio})";
    }
}