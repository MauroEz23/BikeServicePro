using System.Collections.Generic;
using System.Threading.Tasks;

namespace BikeServicePro.Services.Interfaces
{
    /// <summary>
    /// Interfaz base para todos los servicios de negocio
    /// Define operaciones CRUD genéricas
    /// </summary>
    /// <typeparam name="T">Tipo de entidad</typeparam>
    /// <typeparam name="TViewModel">Tipo de ViewModel para la entidad</typeparam>
    public interface IService<T, TViewModel> where T : class where TViewModel : class
    {
        /// <summary>
        /// Obtiene todas las entidades como ViewModels
        /// </summary>
        Task<IEnumerable<TViewModel>> GetAllAsync();
        
        /// <summary>
        /// Obtiene una entidad por su ID
        /// </summary>
        Task<TViewModel?> GetByIdAsync(int id);
        
        /// <summary>
        /// Crea una nueva entidad desde un ViewModel
        /// </summary>
        Task<TViewModel> CreateAsync(TViewModel viewModel);
        
        /// <summary>
        /// Actualiza una entidad existente
        /// </summary>
        Task UpdateAsync(TViewModel viewModel);
        
        /// <summary>
        /// Elimina una entidad por su ID
        /// </summary>
        Task DeleteAsync(int id);
        
        /// <summary>
        /// Verifica si una entidad existe
        /// </summary>
        Task<bool> ExistsAsync(int id);
    }
}