using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeServicePro.Models;
using BikeServicePro.Repositories.Interfaces;
using BikeServicePro.Services.Interfaces;
using BikeServicePro.ViewModels.Mecanicos;

namespace BikeServicePro.Services.Implementations
{
    /// <summary>
    /// Implementación del servicio de mecánicos
    /// </summary>
    public class MecanicoService : IMecanicoService
    {
        private readonly IMecanicoRepository _mecanicoRepository;
        private readonly IReparacionRepository _reparacionRepository;

        public MecanicoService(IMecanicoRepository mecanicoRepository, IReparacionRepository reparacionRepository)
        {
            _mecanicoRepository = mecanicoRepository;
            _reparacionRepository = reparacionRepository;
        }

        public async Task<IEnumerable<MecanicoListViewModel>> GetAllAsync()
        {
            var mecanicos = await _mecanicoRepository.GetAllAsync();
            return mecanicos.Select(m => MapToMecanicoListViewModel(m));
        }

        public async Task<MecanicoListViewModel?> GetByIdAsync(int id)
        {
            var mecanico = await _mecanicoRepository.GetByIdAsync(id);
            return mecanico != null ? MapToMecanicoListViewModel(mecanico) : null;
        }

        public async Task<MecanicoDetailViewModel?> GetMecanicoDetailAsync(int id)
        {
            var mecanico = await _mecanicoRepository.GetByIdAsync(id);
            if (mecanico == null) return null;

            var reparaciones = await _reparacionRepository.GetByMecanicoIdAsync(id);
            
            return new MecanicoDetailViewModel
            {
                Id = mecanico.Id,
                Nombre = mecanico.Nombre,
                Apellidos = mecanico.Apellidos,
                Especialidad = mecanico.Especialidad,
                Telefono = mecanico.Telefono,
                Email = mecanico.Email,
                FechaContratacion = mecanico.FechaContratacion,
                FotoUrl = mecanico.FotoUrl,
                Certificaciones = mecanico.Certificaciones,
                HorarioTrabajo = mecanico.HorarioTrabajo,
                Activo = mecanico.Activo,
                NombreCompleto = mecanico.NombreCompleto,
                AñosExperiencia = mecanico.AñosExperiencia,
                ReparacionesAsignadas = reparaciones.Count(),
                ReparacionesActivas = reparaciones.Count(r => 
                    r.Estado != EstadoReparacion.Entregada && 
                    r.Estado != EstadoReparacion.Cancelada)
            };
        }

        public async Task<MecanicoListViewModel> CreateAsync(MecanicoListViewModel viewModel)
        {
            var mecanico = new Mecanico
            {
                Nombre = viewModel.Nombre,
                Apellidos = viewModel.Apellidos,
                Especialidad = viewModel.Especialidad,
                Telefono = viewModel.Telefono,
                Email = viewModel.Email,
                FechaContratacion = DateTime.Now,
                Activo = true
            };

            var created = await _mecanicoRepository.AddAsync(mecanico);
            return MapToMecanicoListViewModel(created);
        }

        public async Task<MecanicoCreateViewModel> CreateMecanicoAsync(MecanicoCreateViewModel viewModel)
        {
            var mecanico = new Mecanico
            {
                Nombre = viewModel.Nombre,
                Apellidos = viewModel.Apellidos,
                Especialidad = viewModel.Especialidad,
                Telefono = viewModel.Telefono,
                Email = viewModel.Email,
                FechaContratacion = viewModel.FechaContratacion,
                FotoUrl = viewModel.FotoUrl,
                Certificaciones = viewModel.Certificaciones,
                HorarioTrabajo = viewModel.HorarioTrabajo,
                Activo = viewModel.Activo
            };

            var created = await _mecanicoRepository.AddAsync(mecanico);
            
            return new MecanicoCreateViewModel
            {
                Id = created.Id,
                Nombre = created.Nombre,
                Apellidos = created.Apellidos,
                Especialidad = created.Especialidad,
                Telefono = created.Telefono,
                Email = created.Email,
                FechaContratacion = created.FechaContratacion,
                FotoUrl = created.FotoUrl,
                Certificaciones = created.Certificaciones,
                HorarioTrabajo = created.HorarioTrabajo,
                Activo = created.Activo
            };
        }

        public async Task UpdateAsync(MecanicoListViewModel viewModel)
        {
            var mecanico = await _mecanicoRepository.GetByIdAsync(viewModel.Id);
            if (mecanico == null) throw new Exception($"Mecánico con ID {viewModel.Id} no encontrado");

            mecanico.Nombre = viewModel.Nombre;
            mecanico.Apellidos = viewModel.Apellidos;
            mecanico.Especialidad = viewModel.Especialidad;
            mecanico.Telefono = viewModel.Telefono;
            mecanico.Email = viewModel.Email;
            mecanico.Activo = viewModel.Activo;

            await _mecanicoRepository.UpdateAsync(mecanico);
        }

        public async Task UpdateMecanicoAsync(MecanicoCreateViewModel viewModel)
        {
            var mecanico = await _mecanicoRepository.GetByIdAsync(viewModel.Id);
            if (mecanico == null) throw new Exception($"Mecánico con ID {viewModel.Id} no encontrado");

            mecanico.Nombre = viewModel.Nombre;
            mecanico.Apellidos = viewModel.Apellidos;
            mecanico.Especialidad = viewModel.Especialidad;
            mecanico.Telefono = viewModel.Telefono;
            mecanico.Email = viewModel.Email;
            mecanico.FechaContratacion = viewModel.FechaContratacion;
            mecanico.FotoUrl = viewModel.FotoUrl;
            mecanico.Certificaciones = viewModel.Certificaciones;
            mecanico.HorarioTrabajo = viewModel.HorarioTrabajo;
            mecanico.Activo = viewModel.Activo;

            await _mecanicoRepository.UpdateAsync(mecanico);
        }

        public async Task DeleteAsync(int id)
        {
            await _mecanicoRepository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _mecanicoRepository.ExistsAsync(id);
        }

        public async Task<IEnumerable<MecanicoListViewModel>> GetActiveMecanicosAsync()
        {
            var mecanicos = await _mecanicoRepository.GetActiveMecanicosAsync();
            return mecanicos.Select(m => MapToMecanicoListViewModel(m));
        }

        public async Task<IEnumerable<MecanicoListViewModel>> SearchAsync(string searchTerm)
        {
            var mecanicos = await _mecanicoRepository.SearchAsync(searchTerm);
            return mecanicos.Select(m => MapToMecanicoListViewModel(m));
        }

        private MecanicoListViewModel MapToMecanicoListViewModel(Mecanico mecanico)
        {
            return new MecanicoListViewModel
            {
                Id = mecanico.Id,
                Nombre = mecanico.Nombre,
                Apellidos = mecanico.Apellidos,
                Especialidad = mecanico.Especialidad,
                Telefono = mecanico.Telefono,
                Email = mecanico.Email,
                FechaContratacion = mecanico.FechaContratacion,
                Activo = mecanico.Activo,
                NombreCompleto = mecanico.NombreCompleto,
                AñosExperiencia = mecanico.AñosExperiencia
            };
        }
    }
}