using System;

namespace BikeServicePro.Models
{
    /// <summary>
    /// Modelo de dominio que representa un servicio ofrecido
    /// </summary>
    public class Servicio
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal PrecioBase { get; set; }
        public int TiempoEstimado { get; set; }
        public bool Activo { get; set; } = true;
        public CategoriaServicio Categoria { get; set; }
        public string? Icono { get; set; }
        public string? RequiereRepuesto { get; set; }
        public DateTime FechaCreacion { get; set; }
        
        // Propiedad calculada
        public string TiempoEstimadoString => 
            TiempoEstimado >= 60 
                ? $"{TiempoEstimado / 60}h {(TiempoEstimado % 60 > 0 ? $"{TiempoEstimado % 60}min" : "")}" 
                : $"{TiempoEstimado}min";
    }

    /// <summary>
    /// Categorías de servicios
    /// </summary>
    public enum CategoriaServicio
    {
        Mantenimiento = 1,
        Reparacion = 2,
        Instalacion = 3,
        Limpieza = 4,
        Diagnostico = 5,
        Ajuste = 6
    }
}