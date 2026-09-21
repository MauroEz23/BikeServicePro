using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeServicePro.Models;
using BikeServicePro.Repositories.Interfaces;

namespace BikeServicePro.Repositories.Implementations.JSON
{
    /// <summary>
    /// Implementación JSON del repositorio de clientes
    /// </summary>
    public class JsonClienteRepository : JsonRepositoryBase<Cliente>, IClienteRepository
    {
        public JsonClienteRepository() : base("clientes.json")
        {
        }

        /// <summary>
        /// Busca clientes por nombre o apellidos
        /// </summary>
        public async Task<IEnumerable<Cliente>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync();

            return await Task.FromResult(_entities.Where(c =>
                c.Nombre.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase) ||
                c.Apellidos.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase) ||
                c.Email.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase) ||
                c.Telefono.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase)
            ));
        }

        /// <summary>
        /// Obtiene clientes con sus bicicletas incluidas
        /// </summary>
        public async Task<IEnumerable<Cliente>> GetClientesWithBicicletasAsync()
        {
            // Como estamos usando JSON, las relaciones se manejan en el servicio
            return await GetAllAsync();
        }

        /// <summary>
        /// Obtiene un cliente con sus bicicletas por ID
        /// </summary>
        public async Task<Cliente?> GetClienteWithBicicletasAsync(int id)
        {
            return await GetByIdAsync(id);
        }

        /// <summary>
        /// Obtiene clientes con historial de reparaciones
        /// </summary>
        public async Task<IEnumerable<Cliente>> GetClientesWithHistorialAsync()
        {
            return await GetAllAsync();
        }

        /// <summary>
        /// Obtiene el conteo total de clientes
        /// </summary>
        public async Task<int> GetTotalCountAsync()
        {
            return await Task.FromResult(_entities.Count);
        }

        /// <summary>
        /// Obtiene clientes activos
        /// </summary>
        public async Task<IEnumerable<Cliente>> GetActiveClientesAsync()
        {
            return await Task.FromResult(_entities.Where(c => c.Activo));
        }
    }
}