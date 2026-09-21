using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BikeServicePro.Models;

namespace BikeServicePro.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz para el repositorio de reparaciones
    /// </summary>
    public interface IReparacionRepository : IRepository<Reparacion>
    {
        /// <summary>
        /// Obtiene reparaciones por cliente
        /// </summary>
        Task<IEnumerable<Reparacion>> GetByClienteIdAsync(int clienteId);
        
        /// <summary>
        /// Obtiene reparaciones por bicicleta
        /// </summary>
        Task<IEnumerable<Reparacion>> GetByBicicletaIdAsync(int bicicletaId);
        
        /// <summary>
        /// Obtiene reparaciones por estado
        /// </summary>
        Task<IEnumerable<Reparacion>> GetByEstadoAsync(EstadoReparacion estado);
        
        /// <summary>
        /// Obtiene reparaciones por mecánico
        /// </summary>
        Task<IEnumerable<Reparacion>> GetByMecanicoIdAsync(int mecanicoId);
        
        /// <summary>
        /// Obtiene reparaciones por rango de fechas
        /// </summary>
        Task<IEnumerable<Reparacion>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// Obtiene reparación completa (con todas las relaciones)
        /// </summary>
        Task<Reparacion?> GetFullReparacionAsync(int id);
        
        /// <summary>
        /// Obtiene todas las reparaciones completas
        /// </summary>
        Task<IEnumerable<Reparacion>> GetAllFullReparacionesAsync();
        
        /// <summary>
        /// Actualiza el estado de una reparación
        /// </summary>
        Task UpdateEstadoAsync(int id, EstadoReparacion nuevoEstado);
        
        /// <summary>
        /// Obtiene estadísticas de reparaciones
        /// </summary>
        Task<ReparacionEstadisticas> GetEstadisticasAsync();
        
        /// <summary>
        /// Genera el siguiente número de reparación
        /// </summary>
        Task<string> GenerarNumeroReparacionAsync();
    }

    /// <summary>
    /// Estadísticas agregadas de reparaciones
    /// </summary>
    public class ReparacionEstadisticas
    {
        public int TotalReparaciones { get; set; }
        public int ReparacionesActivas { get; set; }
        public int ReparacionesCompletadas { get; set; }
        public int ReparacionesCanceladas { get; set; }
        public decimal IngresosTotales { get; set; }
        public decimal PromedioCosto { get; set; }
        public Dictionary<EstadoReparacion, int> ReparacionesPorEstado { get; set; } = new();
        public Dictionary<string, int> ReparacionesPorMes { get; set; } = new();
        public int TiempoPromedioReparacion { get; set; }
    }
}