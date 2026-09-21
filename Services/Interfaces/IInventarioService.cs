using System.Collections.Generic;
using System.Threading.Tasks;
using BikeServicePro.Models;
using BikeServicePro.Repositories.Interfaces;
using BikeServicePro.ViewModels.Inventario;

namespace BikeServicePro.Services.Interfaces
{
    public interface IInventarioService : IService<Models.Inventario, InventarioListViewModel>
    {
        Task<IEnumerable<InventarioListViewModel>> GetByCategoriaAsync(string categoria);
        Task<IEnumerable<InventarioListViewModel>> GetStockBajoAsync();
        Task<IEnumerable<InventarioListViewModel>> SearchAsync(string searchTerm);
        Task<IEnumerable<InventarioListViewModel>> GetByProveedorAsync(string proveedor);
        Task UpdateStockAsync(int id, int cantidad);
        Task<decimal> GetValorTotalInventarioAsync();
        Task<InventarioEstadisticas> GetEstadisticasAsync();
        Task<InventarioCreateViewModel> CreateInventarioAsync(InventarioCreateViewModel viewModel);
        
        // CORREGIDO: Agregar método de actualización
        Task UpdateInventarioAsync(Inventario inventario);
    }
}