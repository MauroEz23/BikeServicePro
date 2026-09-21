using System.Collections.Generic;
using System.Threading.Tasks;
using BikeServicePro.Models;

namespace BikeServicePro.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz para el repositorio de servicios
    /// </summary>
    public interface IServicioRepository : IRepository<Servicio>
    {
        /// <summary>
        /// Obtiene servicios por categoría
        /// </summary>
        Task<IEnumerable<Servicio>> GetByCategoriaAsync(CategoriaServicio categoria);
        
        /// <summary>
        /// Obtiene servicios activos
        /// </summary>
        Task<IEnumerable<Servicio>> GetActiveServiciosAsync();
        
        /// <summary>
        /// Busca servicios por nombre
        /// </summary>
        Task<IEnumerable<Servicio>> SearchAsync(string searchTerm);
    }
}