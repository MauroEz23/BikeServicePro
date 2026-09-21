using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeServicePro.Models;
using BikeServicePro.Repositories.Interfaces;
using BikeServicePro.Services.Interfaces;
using BikeServicePro.ViewModels.Inventario;

namespace BikeServicePro.Services.Implementations
{
    public class InventarioService : IInventarioService
    {
        private readonly IInventarioRepository _inventarioRepository;

        public InventarioService(IInventarioRepository inventarioRepository)
        {
            _inventarioRepository = inventarioRepository;
        }

        public async Task<IEnumerable<InventarioListViewModel>> GetAllAsync()
        {
            var items = await _inventarioRepository.GetAllAsync();
            return items.Select(i => MapToInventarioListViewModel(i));
        }

        public async Task<InventarioListViewModel?> GetByIdAsync(int id)
        {
            var item = await _inventarioRepository.GetByIdAsync(id);
            return item != null ? MapToInventarioListViewModel(item) : null;
        }

        public async Task<InventarioListViewModel> CreateAsync(InventarioListViewModel viewModel)
        {
            var item = new Inventario
            {
                Nombre = viewModel.Nombre,
                Categoria = viewModel.Categoria,
                SubCategoria = viewModel.SubCategoria,
                CodigoInterno = viewModel.CodigoInterno,
                Cantidad = viewModel.Cantidad,
                StockMinimo = viewModel.StockMinimo,
                Proveedor = viewModel.Proveedor,
                Costo = viewModel.Costo,
                Precio = viewModel.Precio,
                CodigoBarras = viewModel.CodigoBarras,
                Ubicacion = viewModel.Ubicacion,
                Activo = true,
                FechaActualizacion = DateTime.Now
            };

            var created = await _inventarioRepository.AddAsync(item);
            return MapToInventarioListViewModel(created);
        }

        public async Task<InventarioCreateViewModel> CreateInventarioAsync(InventarioCreateViewModel viewModel)
        {
            var item = new Inventario
            {
                Nombre = viewModel.Nombre,
                Categoria = viewModel.Categoria,
                SubCategoria = viewModel.SubCategoria,
                CodigoInterno = viewModel.CodigoInterno,
                Cantidad = viewModel.Cantidad,
                StockMinimo = viewModel.StockMinimo,
                Proveedor = viewModel.Proveedor,
                Costo = viewModel.Costo,
                Precio = viewModel.Precio,
                CodigoBarras = viewModel.CodigoBarras,
                Ubicacion = viewModel.Ubicacion,
                Activo = true,
                FechaActualizacion = DateTime.Now
            };

            var created = await _inventarioRepository.AddAsync(item);
            
            return new InventarioCreateViewModel
            {
                Id = created.Id,
                Nombre = created.Nombre,
                Categoria = created.Categoria,
                SubCategoria = created.SubCategoria,
                CodigoInterno = created.CodigoInterno,
                Cantidad = created.Cantidad,
                StockMinimo = created.StockMinimo,
                Proveedor = created.Proveedor,
                Costo = created.Costo,
                Precio = created.Precio,
                CodigoBarras = created.CodigoBarras,
                Ubicacion = created.Ubicacion,
                Activo = created.Activo
            };
        }

        public async Task UpdateAsync(InventarioListViewModel viewModel)
        {
            var item = await _inventarioRepository.GetByIdAsync(viewModel.Id);
            if (item == null) throw new Exception($"Item con ID {viewModel.Id} no encontrado");

            item.Nombre = viewModel.Nombre;
            item.Categoria = viewModel.Categoria;
            item.SubCategoria = viewModel.SubCategoria;
            item.CodigoInterno = viewModel.CodigoInterno;
            item.Cantidad = viewModel.Cantidad;
            item.StockMinimo = viewModel.StockMinimo;
            item.Proveedor = viewModel.Proveedor;
            item.Costo = viewModel.Costo;
            item.Precio = viewModel.Precio;
            item.CodigoBarras = viewModel.CodigoBarras;
            item.Ubicacion = viewModel.Ubicacion;
            item.Activo = viewModel.Activo;
            item.FechaActualizacion = DateTime.Now;

            await _inventarioRepository.UpdateAsync(item);
        }

        /// <summary>
        /// CORREGIDO: Actualiza un ítem de inventario existente
        /// </summary>
        public async Task UpdateInventarioAsync(Inventario inventario)
        {
            await _inventarioRepository.UpdateAsync(inventario);
        }

        public async Task DeleteAsync(int id)
        {
            await _inventarioRepository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _inventarioRepository.ExistsAsync(id);
        }

        public async Task<IEnumerable<InventarioListViewModel>> GetByCategoriaAsync(string categoria)
        {
            var items = await _inventarioRepository.GetByCategoriaAsync(categoria);
            return items.Select(i => MapToInventarioListViewModel(i));
        }

        public async Task<IEnumerable<InventarioListViewModel>> GetStockBajoAsync()
        {
            var items = await _inventarioRepository.GetStockBajoAsync();
            return items.Select(i => MapToInventarioListViewModel(i));
        }

        public async Task<IEnumerable<InventarioListViewModel>> SearchAsync(string searchTerm)
        {
            var items = await _inventarioRepository.SearchAsync(searchTerm);
            return items.Select(i => MapToInventarioListViewModel(i));
        }

        public async Task<IEnumerable<InventarioListViewModel>> GetByProveedorAsync(string proveedor)
        {
            var items = await _inventarioRepository.GetByProveedorAsync(proveedor);
            return items.Select(i => MapToInventarioListViewModel(i));
        }

        public async Task UpdateStockAsync(int id, int cantidad)
        {
            await _inventarioRepository.UpdateStockAsync(id, cantidad);
        }

        public async Task<decimal> GetValorTotalInventarioAsync()
        {
            return await _inventarioRepository.GetValorTotalInventarioAsync();
        }

        public async Task<InventarioEstadisticas> GetEstadisticasAsync()
        {
            return await _inventarioRepository.GetEstadisticasAsync();
        }

        private InventarioListViewModel MapToInventarioListViewModel(Inventario item)
        {
            return new InventarioListViewModel
            {
                Id = item.Id,
                Nombre = item.Nombre,
                Categoria = item.Categoria,
                SubCategoria = item.SubCategoria,
                CodigoInterno = item.CodigoInterno,
                Cantidad = item.Cantidad,
                StockMinimo = item.StockMinimo,
                Proveedor = item.Proveedor,
                Costo = item.Costo,
                Precio = item.Precio,
                CodigoBarras = item.CodigoBarras,
                Ubicacion = item.Ubicacion,
                Activo = item.Activo,
                FechaActualizacion = item.FechaActualizacion,
                StockBajo = item.StockBajo,
                MargenGanancia = item.MargenGanancia,
                PorcentajeGanancia = item.PorcentajeGanancia
            };
        }
    }
}