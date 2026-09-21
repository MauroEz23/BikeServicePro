using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeServicePro.Models;
using BikeServicePro.Repositories.Interfaces;
using BikeServicePro.Services.Interfaces;
using BikeServicePro.ViewModels.Dashboard;

namespace BikeServicePro.Services.Implementations
{
    /// <summary>
    /// Implementación del servicio de reportes y dashboard
    /// </summary>
    public class ReporteService : IReporteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IBicicletaRepository _bicicletaRepository;
        private readonly IReparacionRepository _reparacionRepository;
        private readonly IInventarioRepository _inventarioRepository;
        private readonly IMecanicoRepository _mecanicoRepository;

        public ReporteService(
            IClienteRepository clienteRepository,
            IBicicletaRepository bicicletaRepository,
            IReparacionRepository reparacionRepository,
            IInventarioRepository inventarioRepository,
            IMecanicoRepository mecanicoRepository)
        {
            _clienteRepository = clienteRepository;
            _bicicletaRepository = bicicletaRepository;
            _reparacionRepository = reparacionRepository;
            _inventarioRepository = inventarioRepository;
            _mecanicoRepository = mecanicoRepository;
        }

        public async Task<DashboardViewModel> GetDashboardDataAsync()
        {
            var dashboard = new DashboardViewModel();
            
            // Estadísticas de clientes
            dashboard.TotalClientes = await _clienteRepository.GetTotalCountAsync();
            dashboard.ClientesActivos = (await _clienteRepository.GetActiveClientesAsync()).Count();
            
            // Estadísticas de bicicletas
            var bicicletas = await _bicicletaRepository.GetAllAsync();
            dashboard.TotalBicicletas = bicicletas.Count();
            
            // Estadísticas de reparaciones
            var reparacionesStats = await _reparacionRepository.GetEstadisticasAsync();
            dashboard.TotalReparaciones = reparacionesStats.TotalReparaciones;
            dashboard.ReparacionesActivas = reparacionesStats.ReparacionesActivas;
            dashboard.ReparacionesCompletadas = reparacionesStats.ReparacionesCompletadas;
            dashboard.IngresosTotales = reparacionesStats.IngresosTotales;
            dashboard.ReparacionesPorEstado = reparacionesStats.ReparacionesPorEstado;
            dashboard.ReparacionesPorMes = reparacionesStats.ReparacionesPorMes;
            dashboard.TiempoPromedioReparacion = reparacionesStats.TiempoPromedioReparacion;
            dashboard.PromedioCostoReparacion = reparacionesStats.PromedioCosto;
            
            // Estadísticas de inventario
            var inventarioStats = await _inventarioRepository.GetEstadisticasAsync();
            dashboard.TotalItemsInventario = inventarioStats.TotalItems;
            dashboard.ItemsStockBajo = inventarioStats.ItemsStockBajo;
            dashboard.ValorTotalInventario = inventarioStats.ValorTotalInventario;
            dashboard.ItemsPorCategoria = inventarioStats.ItemsPorCategoria;
            
            // Mecánicos activos
            var mecanicos = await _mecanicoRepository.GetActiveMecanicosAsync();
            dashboard.TotalMecanicos = mecanicos.Count();
            dashboard.ReparacionesPorMecanico = new Dictionary<string, int>();
            
            foreach (var mecanico in mecanicos)
            {
                var reparacionesMecanico = await _reparacionRepository.GetByMecanicoIdAsync(mecanico.Id);
                dashboard.ReparacionesPorMecanico[mecanico.NombreCompleto] = reparacionesMecanico.Count();
            }
            
            // Porcentaje de reparaciones completadas
            dashboard.PorcentajeCompletadas = dashboard.TotalReparaciones > 0
                ? Math.Round((double)dashboard.ReparacionesCompletadas / dashboard.TotalReparaciones * 100, 1)
                : 0;
            
            // Resumen
            dashboard.Resumen = $"{dashboard.TotalReparaciones} reparaciones, {dashboard.TotalClientes} clientes, {dashboard.TotalBicicletas} bicicletas";
            dashboard.EstadoGeneral = dashboard.ItemsStockBajo > 0 ? "Atención: Hay ítems con stock bajo" : "Todo en orden";
            
            return dashboard;
        }

        public async Task<Dictionary<string, int>> GetReparacionesPorMesAsync(int meses = 12)
        {
            var stats = await _reparacionRepository.GetEstadisticasAsync();
            return stats.ReparacionesPorMes
                .OrderByDescending(x => x.Key)
                .Take(meses)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public async Task<Dictionary<string, decimal>> GetIngresosPorMesAsync(int meses = 12)
        {
            var reparaciones = await _reparacionRepository.GetAllFullReparacionesAsync();
            var ingresosPorMes = new Dictionary<string, decimal>();
            
            for (int i = 0; i < meses; i++)
            {
                var mes = DateTime.Now.AddMonths(-i);
                var key = mes.ToString("yyyy-MM");
                
                var ingresos = reparaciones
                    .Where(r => r.Estado == EstadoReparacion.Entregada &&
                                r.FechaIngreso.Year == mes.Year &&
                                r.FechaIngreso.Month == mes.Month)
                    .Sum(r => r.CostoTotal);
                
                ingresosPorMes[key] = ingresos;
            }
            
            return ingresosPorMes.OrderByDescending(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        }

        public async Task<Dictionary<string, int>> GetTopServiciosAsync(int top = 10)
        {
            var reparaciones = await _reparacionRepository.GetAllAsync();
            var servicios = reparaciones
                .Where(r => !string.IsNullOrEmpty(r.Diagnostico))
                .GroupBy(r => r.Diagnostico)
                .Select(g => new { Servicio = g.Key, Cantidad = g.Count() })
                .OrderByDescending(x => x.Cantidad)
                .Take(top)
                .ToDictionary(x => x.Servicio ?? "Sin diagnóstico", x => x.Cantidad);
            
            return servicios;
        }

        public async Task<Dictionary<string, int>> GetEstadisticasClientesAsync()
        {
            var stats = new Dictionary<string, int>
            {
                ["Total"] = await _clienteRepository.GetTotalCountAsync(),
                ["Activos"] = (await _clienteRepository.GetActiveClientesAsync()).Count()
            };
            
            return stats;
        }

        public async Task<Dictionary<string, object>> GetEstadisticasInventarioAsync()
        {
            var stats = await _inventarioRepository.GetEstadisticasAsync();
            
            return new Dictionary<string, object>
            {
                ["TotalItems"] = stats.TotalItems,
                ["ItemsStockBajo"] = stats.ItemsStockBajo,
                ["ItemsSinStock"] = stats.ItemsSinStock,
                ["ValorTotal"] = stats.ValorTotalInventario,
                ["ItemsPorCategoria"] = stats.ItemsPorCategoria,
                ["ValorPorCategoria"] = stats.ValorPorCategoria
            };
        }

        public async Task<IEnumerable<object>> GetActividadesRecientesAsync(int cantidad = 10)
        {
            var actividades = new List<object>();
            
            var reparaciones = await _reparacionRepository.GetAllFullReparacionesAsync();
            var ultimasReparaciones = reparaciones
                .OrderByDescending(r => r.FechaIngreso)
                .Take(cantidad)
                .Select(r => new
                {
                    Tipo = "Reparación",
                    r.Numero,
                    r.FechaIngreso,
                    r.Estado,
                    r.Diagnostico
                });
            
            actividades.AddRange(ultimasReparaciones);
            
            return actividades.Take(cantidad);
        }
    }
}