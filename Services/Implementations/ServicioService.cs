using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeServicePro.Models;
using BikeServicePro.Repositories.Interfaces;
using BikeServicePro.Services.Interfaces;
using BikeServicePro.ViewModels.Servicios;

namespace BikeServicePro.Services.Implementations
{
    /// <summary>
    /// Implementación del servicio de servicios
    /// </summary>
    public class ServicioService : IServicioService
    {
        private readonly IServicioRepository _servicioRepository;

        public ServicioService(IServicioRepository servicioRepository)
        {
            _servicioRepository = servicioRepository;
        }

        public async Task<IEnumerable<ServicioListViewModel>> GetAllAsync()
        {
            var servicios = await _servicioRepository.GetAllAsync();
            return servicios.Select(s => MapToServicioListViewModel(s));
        }

        public async Task<ServicioListViewModel?> GetByIdAsync(int id)
        {
            var servicio = await _servicioRepository.GetByIdAsync(id);
            return servicio != null ? MapToServicioListViewModel(servicio) : null;
        }

        public async Task<ServicioListViewModel> CreateAsync(ServicioListViewModel viewModel)
        {
            var servicio = new Servicio
            {
                Nombre = viewModel.Nombre,
                Descripcion = viewModel.Descripcion,
                PrecioBase = viewModel.PrecioBase,
                TiempoEstimado = viewModel.TiempoEstimado,
                Categoria = viewModel.Categoria,
                Icono = viewModel.Icono,
                RequiereRepuesto = viewModel.RequiereRepuesto,
                Activo = true,
                FechaCreacion = DateTime.Now
            };

            var created = await _servicioRepository.AddAsync(servicio);
            return MapToServicioListViewModel(created);
        }

        public async Task<ServicioCreateViewModel> CreateServicioAsync(ServicioCreateViewModel viewModel)
        {
            var servicio = new Servicio
            {
                Nombre = viewModel.Nombre,
                Descripcion = viewModel.Descripcion,
                PrecioBase = viewModel.PrecioBase,
                TiempoEstimado = viewModel.TiempoEstimado,
                Categoria = viewModel.Categoria,
                Icono = viewModel.Icono,
                RequiereRepuesto = viewModel.RequiereRepuesto,
                Activo = viewModel.Activo,
                FechaCreacion = DateTime.Now
            };

            var created = await _servicioRepository.AddAsync(servicio);
            
            return new ServicioCreateViewModel
            {
                Id = created.Id,
                Nombre = created.Nombre,
                Descripcion = created.Descripcion,
                PrecioBase = created.PrecioBase,
                TiempoEstimado = created.TiempoEstimado,
                Categoria = created.Categoria,
                Icono = created.Icono,
                RequiereRepuesto = created.RequiereRepuesto,
                Activo = created.Activo
            };
        }

        public async Task UpdateAsync(ServicioListViewModel viewModel)
        {
            var servicio = await _servicioRepository.GetByIdAsync(viewModel.Id);
            if (servicio == null) throw new Exception($"Servicio con ID {viewModel.Id} no encontrado");

            servicio.Nombre = viewModel.Nombre;
            servicio.Descripcion = viewModel.Descripcion;
            servicio.PrecioBase = viewModel.PrecioBase;
            servicio.TiempoEstimado = viewModel.TiempoEstimado;
            servicio.Categoria = viewModel.Categoria;
            servicio.Icono = viewModel.Icono;
            servicio.RequiereRepuesto = viewModel.RequiereRepuesto;
            servicio.Activo = viewModel.Activo;

            await _servicioRepository.UpdateAsync(servicio);
        }

        public async Task UpdateServicioAsync(ServicioCreateViewModel viewModel)
        {
            var servicio = await _servicioRepository.GetByIdAsync(viewModel.Id);
            if (servicio == null) throw new Exception($"Servicio con ID {viewModel.Id} no encontrado");

            servicio.Nombre = viewModel.Nombre;
            servicio.Descripcion = viewModel.Descripcion;
            servicio.PrecioBase = viewModel.PrecioBase;
            servicio.TiempoEstimado = viewModel.TiempoEstimado;
            servicio.Categoria = viewModel.Categoria;
            servicio.Icono = viewModel.Icono;
            servicio.RequiereRepuesto = viewModel.RequiereRepuesto;
            servicio.Activo = viewModel.Activo;

            await _servicioRepository.UpdateAsync(servicio);
        }

        public async Task DeleteAsync(int id)
        {
            await _servicioRepository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _servicioRepository.ExistsAsync(id);
        }

        public async Task<IEnumerable<ServicioListViewModel>> GetByCategoriaAsync(string categoria)
        {
            if (!Enum.TryParse<CategoriaServicio>(categoria, true, out var categoriaEnum))
            {
                return new List<ServicioListViewModel>();
            }
            
            var servicios = await _servicioRepository.GetByCategoriaAsync(categoriaEnum);
            return servicios.Select(s => MapToServicioListViewModel(s));
        }

        public async Task<IEnumerable<ServicioListViewModel>> GetActiveServiciosAsync()
        {
            var servicios = await _servicioRepository.GetActiveServiciosAsync();
            return servicios.Select(s => MapToServicioListViewModel(s));
        }

        public async Task<IEnumerable<ServicioListViewModel>> SearchAsync(string searchTerm)
        {
            var servicios = await _servicioRepository.SearchAsync(searchTerm);
            return servicios.Select(s => MapToServicioListViewModel(s));
        }

        private ServicioListViewModel MapToServicioListViewModel(Servicio servicio)
        {
            return new ServicioListViewModel
            {
                Id = servicio.Id,
                Nombre = servicio.Nombre,
                Descripcion = servicio.Descripcion,
                PrecioBase = servicio.PrecioBase,
                TiempoEstimado = servicio.TiempoEstimado,
                Categoria = servicio.Categoria,
                Icono = servicio.Icono,
                RequiereRepuesto = servicio.RequiereRepuesto,
                Activo = servicio.Activo,
                FechaCreacion = servicio.FechaCreacion,
                CategoriaNombre = servicio.Categoria.ToString(),
                TiempoEstimadoString = servicio.TiempoEstimadoString
            };
        }
    }
}