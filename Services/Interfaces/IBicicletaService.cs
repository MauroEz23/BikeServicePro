using System.Collections.Generic;
using System.Threading.Tasks;
using BikeServicePro.ViewModels.Bicicletas;

namespace BikeServicePro.Services.Interfaces
{
    public interface IBicicletaService : IService<Models.Bicicleta, BicicletaListViewModel>
    {
        Task<IEnumerable<BicicletaListViewModel>> GetByClienteIdAsync(int clienteId);
        Task<BicicletaDetailViewModel?> GetBicicletaDetailAsync(int id);
        Task<IEnumerable<BicicletaListViewModel>> SearchAsync(string searchTerm);
        Task<BicicletaListViewModel?> GetByNumeroSerieAsync(string numeroSerie);
        Task<Dictionary<int, int>> GetCountByClienteAsync();
        Task<BicicletaCreateViewModel> CreateBicicletaAsync(BicicletaCreateViewModel viewModel);
        
        // CORREGIDO: Agregar método de actualización
        Task UpdateBicicletaAsync(BicicletaCreateViewModel viewModel);
    }
}