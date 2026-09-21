using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BikeServicePro.Models;
using BikeServicePro.Repositories.Interfaces;
using BikeServicePro.ViewModels.Reparaciones;

namespace BikeServicePro.Services.Interfaces
{
    public interface IReparacionService : IService<Models.Reparacion, ReparacionListViewModel>
    {
        Task<ReparacionDetailViewModel?> GetReparacionDetailAsync(int id);
        Task<IEnumerable<ReparacionListViewModel>> GetByClienteIdAsync(int clienteId);
        Task<IEnumerable<ReparacionListViewModel>> GetByBicicletaIdAsync(int bicicletaId);
        Task<IEnumerable<ReparacionListViewModel>> GetByEstadoAsync(EstadoReparacion estado);
        Task<IEnumerable<ReparacionListViewModel>> GetByMecanicoIdAsync(int mecanicoId);
        Task<IEnumerable<ReparacionListViewModel>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task UpdateEstadoAsync(int id, EstadoReparacion nuevoEstado);
        Task<ReparacionEstadisticas> GetEstadisticasAsync();
        Task<ReparacionCreateViewModel> CreateReparacionAsync(ReparacionCreateViewModel viewModel);
        Task AsignarMecanicoAsync(int reparacionId, int mecanicoId);
        
        // CORREGIDO: Métodos para actualizar
        Task UpdateReparacionAsync(Models.Reparacion reparacion);
        Task UpdateAsync(Models.Reparacion reparacion);
    }
}