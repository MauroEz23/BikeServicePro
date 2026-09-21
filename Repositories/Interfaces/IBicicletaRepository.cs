using System.Collections.Generic;
using System.Threading.Tasks;
using BikeServicePro.Models;

namespace BikeServicePro.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz para el repositorio de bicicletas
    /// Extiende IRepository con operaciones específicas para Bicicleta
    /// </summary>
    public interface IBicicletaRepository : IRepository<Bicicleta>
    {
        /// <summary>
        /// Obtiene bicicletas por cliente
        /// </summary>
        Task<IEnumerable<Bicicleta>> GetByClienteIdAsync(int clienteId);
        
        /// <summary>
        /// Obtiene bicicleta con su cliente
        /// </summary>
        Task<Bicicleta?> GetWithClienteAsync(int id);
        
        /// <summary>
        /// Busca bicicletas por marca o modelo
        /// </summary>
        Task<IEnumerable<Bicicleta>> SearchAsync(string searchTerm);
        
        /// <summary>
        /// Obtiene bicicletas por número de serie
        /// </summary>
        Task<Bicicleta?> GetByNumeroSerieAsync(string numeroSerie);
        
        /// <summary>
        /// Obtiene todas las bicicletas con su cliente
        /// </summary>
        Task<IEnumerable<Bicicleta>> GetAllWithClienteAsync();
        
        /// <summary>
        /// Obtiene el conteo de bicicletas por cliente
        /// </summary>
        Task<Dictionary<int, int>> GetCountByClienteAsync();
    }
}