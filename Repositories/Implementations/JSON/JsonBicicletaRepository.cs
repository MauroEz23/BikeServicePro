using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeServicePro.Models;
using BikeServicePro.Repositories.Interfaces;

namespace BikeServicePro.Repositories.Implementations.JSON
{
    /// <summary>
    /// Implementación JSON del repositorio de bicicletas
    /// </summary>
    public class JsonBicicletaRepository : JsonRepositoryBase<Bicicleta>, IBicicletaRepository
    {
        public JsonBicicletaRepository() : base("bicicletas.json")
        {
        }

        /// <summary>
        /// Obtiene bicicletas por cliente
        /// </summary>
        public async Task<IEnumerable<Bicicleta>> GetByClienteIdAsync(int clienteId)
        {
            return await Task.FromResult(_entities.Where(b => b.ClienteId == clienteId && b.Activo));
        }

        /// <summary>
        /// Obtiene bicicleta con su cliente
        /// </summary>
        public async Task<Bicicleta?> GetWithClienteAsync(int id)
        {
            return await GetByIdAsync(id);
        }

        /// <summary>
        /// Busca bicicletas por marca o modelo
        /// </summary>
        public async Task<IEnumerable<Bicicleta>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync();

            return await Task.FromResult(_entities.Where(b =>
                b.Marca.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase) ||
                b.Modelo.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase) ||
                b.NumeroSerie.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase)
            ));
        }

        /// <summary>
        /// Obtiene bicicletas por número de serie
        /// </summary>
        public async Task<Bicicleta?> GetByNumeroSerieAsync(string numeroSerie)
        {
            return await Task.FromResult(_entities.FirstOrDefault(b => 
                b.NumeroSerie == numeroSerie && b.Activo));
        }

        /// <summary>
        /// Obtiene todas las bicicletas con su cliente
        /// </summary>
        public async Task<IEnumerable<Bicicleta>> GetAllWithClienteAsync()
        {
            return await GetAllAsync();
        }

        /// <summary>
        /// Obtiene el conteo de bicicletas por cliente
        /// </summary>
        public async Task<Dictionary<int, int>> GetCountByClienteAsync()
        {
            return await Task.FromResult(_entities
                .Where(b => b.Activo)
                .GroupBy(b => b.ClienteId)
                .ToDictionary(g => g.Key, g => g.Count()));
        }
    }
}