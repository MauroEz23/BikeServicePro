using System;

namespace BikeServicePro.Models
{
    /// <summary>
    /// Modelo de dominio que representa un ítem del inventario
    /// </summary>
    public class Inventario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string SubCategoria { get; set; } = string.Empty;
        public string? CodigoInterno { get; set; }
        public int Cantidad { get; set; }
        public int StockMinimo { get; set; }
        public string? Proveedor { get; set; }
        public decimal Costo { get; set; }
        public decimal Precio { get; set; }
        public string? CodigoBarras { get; set; }
        public string? Ubicacion { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaActualizacion { get; set; }
        
        // Propiedades calculadas
        public bool StockBajo => Cantidad <= StockMinimo;
        public decimal MargenGanancia => Precio - Costo;
        public decimal PorcentajeGanancia => Costo > 0 ? ((Precio - Costo) / Costo) * 100 : 0;
    }
}