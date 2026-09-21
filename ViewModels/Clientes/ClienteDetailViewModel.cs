using System;
using System.Collections.Generic;
using BikeServicePro.ViewModels.Bicicletas;

namespace BikeServicePro.ViewModels.Clientes
{
    public class ClienteDetailViewModel
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
        
        public List<BicicletaBasicViewModel> Bicicletas { get; set; } = new();
        
        public int TotalBicicletas => Bicicletas?.Count ?? 0;
        public int TotalReparaciones { get; set; }
        public decimal TotalGastado { get; set; }
    }

    public class BicicletaBasicViewModel
    {
        public int Id { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int? Anio { get; set; }
        public string Color { get; set; } = string.Empty;
        public string NumeroSerie { get; set; } = string.Empty;
        public string NombreCompleto => $"{Marca} {Modelo} ({Anio})";
    }
}