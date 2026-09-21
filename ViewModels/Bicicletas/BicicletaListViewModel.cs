using System;

namespace BikeServicePro.ViewModels.Bicicletas
{
    /// <summary>
    /// ViewModel para la lista de bicicletas
    /// </summary>
    public class BicicletaListViewModel
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
        public bool Activo { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string ClienteNombre { get; set; } = string.Empty;
    }
}