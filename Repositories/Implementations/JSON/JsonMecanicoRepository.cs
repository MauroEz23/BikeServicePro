using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeServicePro.Models;
using BikeServicePro.Repositories.Interfaces;

namespace BikeServicePro.Repositories.Implementations.JSON
{
    /// <summary>
    /// Implementación JSON del repositorio de mecánicos
    /// </summary>
    public class JsonMecanicoRepository : JsonRepositoryBase<Mecanico>, IMecanicoRepository
    {
        public JsonMecanicoRepository() : base("mecanicos.json")
        {
        }

        public async Task<IEnumerable<Mecanico>> GetActiveMecanicosAsync()
        {
            return await Task.FromResult(_entities.Where(m => m.Activo));
        }

        public async Task<IEnumerable<Mecanico>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync();

            return await Task.FromResult(_entities.Where(m =>
                m.Nombre.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase) ||
                m.Apellidos.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase) ||
                m.Especialidad.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase) ||
                m.Email.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase)
            ));
        }

        public async Task<IEnumerable<Mecanico>> GetByEspecialidadAsync(string especialidad)
        {
            return await Task.FromResult(_entities.Where(m =>
                m.Especialidad.Equals(especialidad, System.StringComparison.OrdinalIgnoreCase) &&
                m.Activo));
        }
    }
}