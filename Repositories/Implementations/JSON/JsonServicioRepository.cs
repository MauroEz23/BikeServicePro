using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeServicePro.Models;
using BikeServicePro.Repositories.Interfaces;

namespace BikeServicePro.Repositories.Implementations.JSON
{
    /// <summary>
    /// Implementación JSON del repositorio de servicios
    /// </summary>
    public class JsonServicioRepository : JsonRepositoryBase<Servicio>, IServicioRepository
    {
        public JsonServicioRepository() : base("servicios.json")
        {
        }

        public async Task<IEnumerable<Servicio>> GetByCategoriaAsync(CategoriaServicio categoria)
        {
            return await Task.FromResult(_entities.Where(s => s.Categoria == categoria && s.Activo));
        }

        public async Task<IEnumerable<Servicio>> GetActiveServiciosAsync()
        {
            return await Task.FromResult(_entities.Where(s => s.Activo));
        }

        public async Task<IEnumerable<Servicio>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync();

            return await Task.FromResult(_entities.Where(s =>
                s.Nombre.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase) ||
                s.Descripcion.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase)
            ));
        }
    }
}