using System.Collections.Generic;
using System.Threading.Tasks;
using BikeServicePro.ViewModels.Dashboard;

namespace BikeServicePro.Services.Interfaces
{
    /// <summary>
    /// Interfaz para el servicio de reportes y dashboard
    /// </summary>
    public interface IReporteService
    {
        /// <summary>
        /// Obtiene los datos para el dashboard principal
        /// </summary>
        Task<DashboardViewModel> GetDashboardDataAsync();
        
        /// <summary>
        /// Obtiene estadísticas de reparaciones por mes
        /// </summary>
        Task<Dictionary<string, int>> GetReparacionesPorMesAsync(int meses = 12);
        
        /// <summary>
        /// Obtiene estadísticas de ingresos por mes
        /// </summary>
        Task<Dictionary<string, decimal>> GetIngresosPorMesAsync(int meses = 12);
        
        /// <summary>
        /// Obtiene el top de servicios más realizados
        /// </summary>
        Task<Dictionary<string, int>> GetTopServiciosAsync(int top = 10);
        
        /// <summary>
        /// Obtiene estadísticas de clientes
        /// </summary>
        Task<Dictionary<string, int>> GetEstadisticasClientesAsync();
        
        /// <summary>
        /// Obtiene estadísticas de inventario
        /// </summary>
        Task<Dictionary<string, object>> GetEstadisticasInventarioAsync();
        
        /// <summary>
        /// Obtiene el resumen de actividades recientes
        /// </summary>
        Task<IEnumerable<object>> GetActividadesRecientesAsync(int cantidad = 10);
    }
}