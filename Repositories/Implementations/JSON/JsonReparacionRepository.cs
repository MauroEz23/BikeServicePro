using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeServicePro.Models;
using BikeServicePro.Repositories.Interfaces;

namespace BikeServicePro.Repositories.Implementations.JSON
{
    /// <summary>
    /// Implementación JSON del repositorio de reparaciones
    /// </summary>
    public class JsonReparacionRepository : JsonRepositoryBase<Reparacion>, IReparacionRepository
    {
        public JsonReparacionRepository() : base("reparaciones.json")
        {
        }

        /// <summary>
        /// Obtiene reparaciones por cliente
        /// </summary>
        public async Task<IEnumerable<Reparacion>> GetByClienteIdAsync(int clienteId)
        {
            return await Task.FromResult(_entities.Where(r => r.ClienteId == clienteId));
        }

        /// <summary>
        /// Obtiene reparaciones por bicicleta
        /// </summary>
        public async Task<IEnumerable<Reparacion>> GetByBicicletaIdAsync(int bicicletaId)
        {
            return await Task.FromResult(_entities.Where(r => r.BicicletaId == bicicletaId));
        }

        /// <summary>
        /// Obtiene reparaciones por estado
        /// </summary>
        public async Task<IEnumerable<Reparacion>> GetByEstadoAsync(EstadoReparacion estado)
        {
            return await Task.FromResult(_entities.Where(r => r.Estado == estado));
        }

        /// <summary>
        /// Obtiene reparaciones por mecánico
        /// </summary>
        public async Task<IEnumerable<Reparacion>> GetByMecanicoIdAsync(int mecanicoId)
        {
            return await Task.FromResult(_entities.Where(r => r.MecanicoId == mecanicoId));
        }

        /// <summary>
        /// Obtiene reparaciones por rango de fechas
        /// </summary>
        public async Task<IEnumerable<Reparacion>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await Task.FromResult(_entities.Where(r => 
                r.FechaIngreso >= startDate && r.FechaIngreso <= endDate));
        }

        /// <summary>
        /// Obtiene reparación completa (con todas las relaciones)
        /// </summary>
        public async Task<Reparacion?> GetFullReparacionAsync(int id)
        {
            return await GetByIdAsync(id);
        }

        /// <summary>
        /// Obtiene todas las reparaciones completas
        /// </summary>
        public async Task<IEnumerable<Reparacion>> GetAllFullReparacionesAsync()
        {
            return await GetAllAsync();
        }

        /// <summary>
        /// Actualiza el estado de una reparación
        /// </summary>
        public async Task UpdateEstadoAsync(int id, EstadoReparacion nuevoEstado)
        {
            var reparacion = await GetByIdAsync(id);
            if (reparacion != null)
            {
                reparacion.Estado = nuevoEstado;
                reparacion.FechaActualizacion = DateTime.Now;
                
                if (nuevoEstado == EstadoReparacion.Entregada)
                {
                    reparacion.FechaEntrega = DateTime.Now;
                }
                
                await UpdateAsync(reparacion);
            }
        }

        /// <summary>
        /// Obtiene estadísticas de reparaciones
        /// </summary>
        public async Task<ReparacionEstadisticas> GetEstadisticasAsync()
        {
            var stats = new ReparacionEstadisticas();
            
            stats.TotalReparaciones = _entities.Count;
            stats.ReparacionesActivas = _entities.Count(r => 
                r.Estado != EstadoReparacion.Entregada && 
                r.Estado != EstadoReparacion.Cancelada);
            stats.ReparacionesCompletadas = _entities.Count(r => r.Estado == EstadoReparacion.Entregada);
            stats.ReparacionesCanceladas = _entities.Count(r => r.Estado == EstadoReparacion.Cancelada);
            stats.IngresosTotales = _entities.Where(r => r.Estado == EstadoReparacion.Entregada)
                .Sum(r => r.CostoTotal);
            stats.PromedioCosto = stats.ReparacionesCompletadas > 0 ? 
                stats.IngresosTotales / stats.ReparacionesCompletadas : 0;
            
            // Reparaciones por estado
            foreach (EstadoReparacion estado in Enum.GetValues(typeof(EstadoReparacion)))
            {
                stats.ReparacionesPorEstado[estado] = _entities.Count(r => r.Estado == estado);
            }
            
            // Reparaciones por mes (últimos 12 meses)
            for (int i = 0; i < 12; i++)
            {
                var mes = DateTime.Now.AddMonths(-i);
                var key = mes.ToString("yyyy-MM");
                stats.ReparacionesPorMes[key] = _entities.Count(r => 
                    r.FechaIngreso.Year == mes.Year && 
                    r.FechaIngreso.Month == mes.Month);
            }
            
            // Tiempo promedio de reparación (solo entregadas)
            var reparacionesEntregadas = _entities.Where(r => 
                r.Estado == EstadoReparacion.Entregada && 
                r.FechaEntrega.HasValue);
            
            stats.TiempoPromedioReparacion = reparacionesEntregadas.Any() 
                ? (int)reparacionesEntregadas.Average(r => (r.FechaEntrega!.Value - r.FechaIngreso).TotalDays)
                : 0;
            
            return await Task.FromResult(stats);
        }

        /// <summary>
        /// Genera el siguiente número de reparación
        /// </summary>
        public async Task<string> GenerarNumeroReparacionAsync()
        {
            var year = DateTime.Now.Year;
            var count = _entities.Count(r => r.Numero.StartsWith($"REP-{year}")) + 1;
            return $"REP-{year}-{count:D3}";
        }
    }
}