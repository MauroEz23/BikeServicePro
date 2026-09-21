using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeServicePro.Models;
using BikeServicePro.Repositories.Interfaces;

namespace BikeServicePro.Repositories.Implementations.JSON
{
    /// <summary>
    /// Implementación JSON del repositorio de inventario
    /// </summary>
    public class JsonInventarioRepository : JsonRepositoryBase<Inventario>, IInventarioRepository
    {
        public JsonInventarioRepository() : base("inventario.json")
        {
        }

        /// <summary>
        /// Obtiene ítems por categoría
        /// </summary>
        public async Task<IEnumerable<Inventario>> GetByCategoriaAsync(string categoria)
        {
            return await Task.FromResult(_entities.Where(i => 
                i.Categoria.Equals(categoria, System.StringComparison.OrdinalIgnoreCase) && 
                i.Activo));
        }

        /// <summary>
        /// Obtiene ítems con stock bajo
        /// </summary>
        public async Task<IEnumerable<Inventario>> GetStockBajoAsync()
        {
            return await Task.FromResult(_entities.Where(i => 
                i.Activo && i.Cantidad <= i.StockMinimo));
        }

        /// <summary>
        /// Busca ítems por nombre o código
        /// </summary>
        public async Task<IEnumerable<Inventario>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync();

            return await Task.FromResult(_entities.Where(i =>
                i.Nombre.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase) ||
                (i.CodigoInterno != null && i.CodigoInterno.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase)) ||
                (i.CodigoBarras != null && i.CodigoBarras.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase))
            ));
        }

        /// <summary>
        /// Obtiene ítems por proveedor
        /// </summary>
        public async Task<IEnumerable<Inventario>> GetByProveedorAsync(string proveedor)
        {
            return await Task.FromResult(_entities.Where(i => 
                i.Proveedor != null && 
                i.Proveedor.Equals(proveedor, System.StringComparison.OrdinalIgnoreCase) &&
                i.Activo));
        }

        /// <summary>
        /// Actualiza el stock de un ítem
        /// </summary>
        public async Task UpdateStockAsync(int id, int cantidad)
        {
            var item = await GetByIdAsync(id);
            if (item != null)
            {
                item.Cantidad = cantidad;
                item.FechaActualizacion = DateTime.Now;
                await UpdateAsync(item);
            }
        }

        /// <summary>
        /// Obtiene el valor total del inventario
        /// </summary>
        public async Task<decimal> GetValorTotalInventarioAsync()
        {
            return await Task.FromResult(_entities.Where(i => i.Activo).Sum(i => i.Costo * i.Cantidad));
        }

        /// <summary>
        /// Obtiene estadísticas de inventario
        /// </summary>
        public async Task<InventarioEstadisticas> GetEstadisticasAsync()
        {
            var stats = new InventarioEstadisticas();
            
            stats.TotalItems = _entities.Count(i => i.Activo);
            stats.ItemsStockBajo = _entities.Count(i => i.Activo && i.Cantidad <= i.StockMinimo);
            stats.ItemsSinStock = _entities.Count(i => i.Activo && i.Cantidad == 0);
            stats.ValorTotalInventario = await GetValorTotalInventarioAsync();
            
            // Items por categoría
            stats.ItemsPorCategoria = _entities
                .Where(i => i.Activo)
                .GroupBy(i => i.Categoria)
                .ToDictionary(g => g.Key, g => g.Count());
            
            // Valor por categoría
            stats.ValorPorCategoria = _entities
                .Where(i => i.Activo)
                .GroupBy(i => i.Categoria)
                .ToDictionary(g => g.Key, g => g.Sum(i => i.Costo * i.Cantidad));
            
            return await Task.FromResult(stats);
        }
    }
}