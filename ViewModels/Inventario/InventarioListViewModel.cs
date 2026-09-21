using System;

namespace BikeServicePro.ViewModels.Inventario
{
    /// <summary>
    /// ViewModel para la lista de inventario
    /// </summary>
    public class InventarioListViewModel
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
        public bool Activo { get; set; }
        public DateTime FechaActualizacion { get; set; }
        
        // Propiedades calculadas
        public bool StockBajo { get; set; }
        public decimal MargenGanancia { get; set; }
        public decimal PorcentajeGanancia { get; set; }
        
        // Propiedad para UI
        public string StockStatusColor => StockBajo ? "danger" : Cantidad == 0 ? "warning" : "success";
        public string StockStatusText => StockBajo ? "Stock Bajo" : Cantidad == 0 ? "Sin Stock" : "En Stock";
    }
}