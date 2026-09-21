using System.Collections.Generic;
using System.Threading.Tasks;
using BikeServicePro.Models;

namespace BikeServicePro.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz para el repositorio de inventario
    /// </summary>
    public interface IInventarioRepository : IRepository<Inventario>
    {
        /// <summary>
        /// Obtiene ítems por categoría
        /// </summary>
        Task<IEnumerable<Inventario>> GetByCategoriaAsync(string categoria);
        
        /// <summary>
        /// Obtiene ítems con stock bajo
        /// </summary>
        Task<IEnumerable<Inventario>> GetStockBajoAsync();
        
        /// <summary>
        /// Busca ítems por nombre o código
        /// </summary>
        Task<IEnumerable<Inventario>> SearchAsync(string searchTerm);
        
        /// <summary>
        /// Obtiene ítems por proveedor
        /// </summary>
        Task<IEnumerable<Inventario>> GetByProveedorAsync(string proveedor);
        
        /// <summary>
        /// Actualiza el stock de un ítem
        /// </summary>
        Task UpdateStockAsync(int id, int cantidad);
        
        /// <summary>
        /// Obtiene el valor total del inventario
        /// </summary>
        Task<decimal> GetValorTotalInventarioAsync();
        
        /// <summary>
        /// Obtiene estadísticas de inventario
        /// </summary>
        Task<InventarioEstadisticas> GetEstadisticasAsync();
    }

    /// <summary>
    /// Estadísticas agregadas de inventario
    /// </summary>
    public class InventarioEstadisticas
    {
        public int TotalItems { get; set; }
        public int ItemsStockBajo { get; set; }
        public int ItemsSinStock { get; set; }
        public decimal ValorTotalInventario { get; set; }
        public Dictionary<string, int> ItemsPorCategoria { get; set; } = new();
        public Dictionary<string, decimal> ValorPorCategoria { get; set; } = new();
    }
}