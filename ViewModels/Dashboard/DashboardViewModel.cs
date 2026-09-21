using System.Collections.Generic;
using BikeServicePro.Models;

namespace BikeServicePro.ViewModels.Dashboard
{
    /// <summary>
    /// ViewModel para el dashboard principal
    /// </summary>
    public class DashboardViewModel
    {
        // Estadísticas de clientes
        public int TotalClientes { get; set; }
        public int ClientesActivos { get; set; }
        
        // Estadísticas de bicicletas
        public int TotalBicicletas { get; set; }
        public double PromedioBicicletasPorCliente { get; set; }
        
        // Estadísticas de reparaciones
        public int TotalReparaciones { get; set; }
        public int ReparacionesActivas { get; set; }
        public int ReparacionesCompletadas { get; set; }
        public decimal IngresosTotales { get; set; }
        public int TiempoPromedioReparacion { get; set; }
        public decimal PromedioCostoReparacion { get; set; }
        public double PorcentajeCompletadas { get; set; }
        
        // Estadísticas de inventario
        public int TotalItemsInventario { get; set; }
        public int ItemsStockBajo { get; set; }
        public decimal ValorTotalInventario { get; set; }
        
        // Estadísticas de mecánicos
        public int TotalMecanicos { get; set; }
        public Dictionary<string, int> ReparacionesPorMecanico { get; set; } = new();
        
        // Datos para gráficos
        public Dictionary<EstadoReparacion, int> ReparacionesPorEstado { get; set; } = new();
        public Dictionary<string, int> ReparacionesPorMes { get; set; } = new();
        public Dictionary<string, int> ItemsPorCategoria { get; set; } = new();
        
        // Resumen
        public string Resumen { get; set; } = string.Empty;
        public string EstadoGeneral { get; set; } = string.Empty;
    }
}