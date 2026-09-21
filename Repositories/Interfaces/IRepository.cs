using System.Collections.Generic;
using System.Threading.Tasks;

namespace BikeServicePro.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz genérica base para todos los repositorios
    /// Implementa el patrón Repository para abstraer la capa de datos
    /// </summary>
    /// <typeparam name="T">Tipo de entidad</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Obtiene todas las entidades
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync();
        
        /// <summary>
        /// Obtiene una entidad por su ID
        /// </summary>
        Task<T?> GetByIdAsync(int id);
        
        /// <summary>
        /// Agrega una nueva entidad
        /// </summary>
        Task<T> AddAsync(T entity);
        
        /// <summary>
        /// Actualiza una entidad existente
        /// </summary>
        Task UpdateAsync(T entity);
        
        /// <summary>
        /// Elimina una entidad por su ID
        /// </summary>
        Task DeleteAsync(int id);
        
        /// <summary>
        /// Verifica si una entidad existe
        /// </summary>
        Task<bool> ExistsAsync(int id);
        
        /// <summary>
        /// Guarda todos los cambios pendientes
        /// </summary>
        Task SaveChangesAsync();
        
        /// <summary>
        /// Obtiene el próximo ID disponible
        /// </summary>
        Task<int> GetNextIdAsync();
    }
}