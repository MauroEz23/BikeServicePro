using System.Collections.Generic;
using System.Threading.Tasks;
using BikeServicePro.ViewModels.Clientes;

namespace BikeServicePro.Services.Interfaces
{
    /// <summary>
    /// Interfaz para el servicio de clientes
    /// </summary>
    public interface IClienteService : IService<Models.Cliente, ClienteListViewModel>
    {
        /// <summary>
        /// Obtiene el detalle completo de un cliente con sus bicicletas
        /// </summary>
        Task<ClienteDetailViewModel?> GetClienteDetailAsync(int id);
        
        /// <summary>
        /// Busca clientes por término de búsqueda
        /// </summary>
        Task<IEnumerable<ClienteListViewModel>> SearchAsync(string searchTerm);
        
        /// <summary>
        /// Obtiene el conteo total de clientes
        /// </summary>
        Task<int> GetTotalCountAsync();
        
        /// <summary>
        /// Obtiene clientes activos
        /// </summary>
        Task<IEnumerable<ClienteListViewModel>> GetActiveClientesAsync();
        
        /// <summary>
        /// Crea un nuevo cliente desde el ViewModel de creación
        /// </summary>
        Task<ClienteCreateViewModel> CreateClienteAsync(ClienteCreateViewModel viewModel);
        
        /// <summary>
        /// Actualiza un cliente existente
        /// </summary>
        Task UpdateClienteAsync(ClienteCreateViewModel viewModel);
    }
}