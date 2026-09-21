using System.Collections.Generic;
using System.Threading.Tasks;
using BikeServicePro.Models;

namespace BikeServicePro.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz para el repositorio de clientes
    /// Extiende IRepository con operaciones específicas para Cliente
    /// </summary>
    public interface IClienteRepository : IRepository<Cliente>
    {
        /// <summary>
        /// Busca clientes por nombre o apellidos
        /// </summary>
        Task<IEnumerable<Cliente>> SearchAsync(string searchTerm);
        
        /// <summary>
        /// Obtiene clientes con sus bicicletas incluidas
        /// </summary>
        Task<IEnumerable<Cliente>> GetClientesWithBicicletasAsync();
        
        /// <summary>
        /// Obtiene un cliente con sus bicicletas por ID
        /// </summary>
        Task<Cliente?> GetClienteWithBicicletasAsync(int id);
        
        /// <summary>
        /// Obtiene clientes con historial de reparaciones
        /// </summary>
        Task<IEnumerable<Cliente>> GetClientesWithHistorialAsync();
        
        /// <summary>
        /// Obtiene el conteo total de clientes
        /// </summary>
        Task<int> GetTotalCountAsync();
        
        /// <summary>
        /// Obtiene clientes activos
        /// </summary>
        Task<IEnumerable<Cliente>> GetActiveClientesAsync();
    }
}