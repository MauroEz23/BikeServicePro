using System;
using BikeServicePro.Models;

namespace BikeServicePro.ViewModels.Servicios
{
    /// <summary>
    /// ViewModel para la lista de servicios
    /// </summary>
    public class ServicioListViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal PrecioBase { get; set; }
        public int TiempoEstimado { get; set; }
        public CategoriaServicio Categoria { get; set; }
        public string? Icono { get; set; }
        public string? RequiereRepuesto { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        
        public string CategoriaNombre { get; set; } = string.Empty;
        public string TiempoEstimadoString { get; set; } = string.Empty;
        public string StatusColor => Activo ? "success" : "danger";
        public string StatusText => Activo ? "Activo" : "Inactivo";
    }
}