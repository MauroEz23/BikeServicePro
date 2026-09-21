using System.Collections.Generic;
using System.Threading.Tasks;
using BikeServicePro.Models;

namespace BikeServicePro.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz para el repositorio de mecánicos
    /// </summary>
    public interface IMecanicoRepository : IRepository<Mecanico>
    {
        /// <summary>
        /// Obtiene mecánicos activos
        /// </summary>
        Task<IEnumerable<Mecanico>> GetActiveMecanicosAsync();
        
        /// <summary>
        /// Busca mecánicos por nombre o especialidad
        /// </summary>
        Task<IEnumerable<Mecanico>> SearchAsync(string searchTerm);
        
        /// <summary>
        /// Obtiene mecánicos por especialidad
        /// </summary>
        Task<IEnumerable<Mecanico>> GetByEspecialidadAsync(string especialidad);
    }
}