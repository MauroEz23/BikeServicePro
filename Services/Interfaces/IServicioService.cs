using System.Collections.Generic;
using System.Threading.Tasks;
using BikeServicePro.Models;
using BikeServicePro.ViewModels.Servicios;

namespace BikeServicePro.Services.Interfaces
{
    /// <summary>
    /// Interfaz para el servicio de servicios
    /// </summary>
    public interface IServicioService : IService<Models.Servicio, ServicioListViewModel>
    {
        /// <summary>
        /// Obtiene servicios por categoría
        /// </summary>
        Task<IEnumerable<ServicioListViewModel>> GetByCategoriaAsync(string categoria);
        
        /// <summary>
        /// Obtiene servicios activos
        /// </summary>
        Task<IEnumerable<ServicioListViewModel>> GetActiveServiciosAsync();
        
        /// <summary>
        /// Busca servicios por término de búsqueda
        /// </summary>
        Task<IEnumerable<ServicioListViewModel>> SearchAsync(string searchTerm);
        
        /// <summary>
        /// Crea un nuevo servicio
        /// </summary>
        Task<ServicioCreateViewModel> CreateServicioAsync(ServicioCreateViewModel viewModel);
        
        /// <summary>
        /// Actualiza un servicio existente
        /// </summary>
        Task UpdateServicioAsync(ServicioCreateViewModel viewModel);
    }
}