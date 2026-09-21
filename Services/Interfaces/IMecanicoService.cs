using System.Collections.Generic;
using System.Threading.Tasks;
using BikeServicePro.ViewModels.Mecanicos;

namespace BikeServicePro.Services.Interfaces
{
    /// <summary>
    /// Interfaz para el servicio de mecánicos
    /// </summary>
    public interface IMecanicoService : IService<Models.Mecanico, MecanicoListViewModel>
    {
        /// <summary>
        /// Obtiene mecánicos activos
        /// </summary>
        Task<IEnumerable<MecanicoListViewModel>> GetActiveMecanicosAsync();
        
        /// <summary>
        /// Busca mecánicos por término de búsqueda
        /// </summary>
        Task<IEnumerable<MecanicoListViewModel>> SearchAsync(string searchTerm);
        
        /// <summary>
        /// Obtiene detalle de mecánico con sus reparaciones
        /// </summary>
        Task<MecanicoDetailViewModel?> GetMecanicoDetailAsync(int id);
        
        /// <summary>
        /// Crea un nuevo mecánico
        /// </summary>
        Task<MecanicoCreateViewModel> CreateMecanicoAsync(MecanicoCreateViewModel viewModel);
        
        /// <summary>
        /// Actualiza un mecánico existente
        /// </summary>
        Task UpdateMecanicoAsync(MecanicoCreateViewModel viewModel);
    }
}