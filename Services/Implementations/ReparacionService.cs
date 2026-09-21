using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeServicePro.Models;
using BikeServicePro.Repositories.Interfaces;
using BikeServicePro.Services.Interfaces;
using BikeServicePro.ViewModels.Reparaciones;

namespace BikeServicePro.Services.Implementations
{
    public class ReparacionService : IReparacionService
    {
        private readonly IReparacionRepository _reparacionRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IBicicletaRepository _bicicletaRepository;
        private readonly IMecanicoRepository _mecanicoRepository;

        public ReparacionService(
            IReparacionRepository reparacionRepository,
            IClienteRepository clienteRepository,
            IBicicletaRepository bicicletaRepository,
            IMecanicoRepository mecanicoRepository)
        {
            _reparacionRepository = reparacionRepository;
            _clienteRepository = clienteRepository;
            _bicicletaRepository = bicicletaRepository;
            _mecanicoRepository = mecanicoRepository;
        }

        public async Task<IEnumerable<ReparacionListViewModel>> GetAllAsync()
        {
            var reparaciones = await _reparacionRepository.GetAllFullReparacionesAsync();
            var result = new List<ReparacionListViewModel>();
            
            foreach (var r in reparaciones)
            {
                result.Add(await MapToReparacionListViewModel(r));
            }
            
            return result;
        }

        public async Task<ReparacionListViewModel?> GetByIdAsync(int id)
        {
            var reparacion = await _reparacionRepository.GetFullReparacionAsync(id);
            if (reparacion == null) return null;
            
            return await MapToReparacionListViewModel(reparacion);
        }

        public async Task<ReparacionDetailViewModel?> GetReparacionDetailAsync(int id)
        {
            var reparacion = await _reparacionRepository.GetFullReparacionAsync(id);
            if (reparacion == null) return null;

            var cliente = await _clienteRepository.GetByIdAsync(reparacion.ClienteId);
            var bicicleta = await _bicicletaRepository.GetByIdAsync(reparacion.BicicletaId);
            var mecanico = reparacion.MecanicoId.HasValue ? 
                await _mecanicoRepository.GetByIdAsync(reparacion.MecanicoId.Value) : null;

            return new ReparacionDetailViewModel
            {
                Id = reparacion.Id,
                Numero = reparacion.Numero,
                ClienteId = reparacion.ClienteId,
                BicicletaId = reparacion.BicicletaId,
                MecanicoId = reparacion.MecanicoId,
                FechaIngreso = reparacion.FechaIngreso,
                FechaEntrega = reparacion.FechaEntrega,
                Estado = reparacion.Estado,
                Diagnostico = reparacion.Diagnostico,
                Observaciones = reparacion.Observaciones,
                Prioridad = reparacion.Prioridad,
                CostoManoObra = reparacion.CostoManoObra,
                CostoRepuestos = reparacion.CostoRepuestos,
                EsGarantia = reparacion.EsGarantia,
                NotasInternas = reparacion.NotasInternas,
                FechaActualizacion = reparacion.FechaActualizacion,
                CostoTotal = reparacion.CostoTotal,
                DiasEnTaller = reparacion.DiasEnTaller,
                EstaAtrasada = reparacion.EstaAtrasada,
                ClienteNombre = cliente?.NombreCompleto ?? "Cliente no encontrado",
                BicicletaNombre = bicicleta?.NombreCompleto ?? "Bicicleta no encontrada",
                MecanicoNombre = mecanico?.NombreCompleto ?? "No asignado"
            };
        }

        public async Task<ReparacionListViewModel> CreateAsync(ReparacionListViewModel viewModel)
        {
            var reparacion = new Reparacion
            {
                Numero = await _reparacionRepository.GenerarNumeroReparacionAsync(),
                ClienteId = viewModel.ClienteId,
                BicicletaId = viewModel.BicicletaId,
                MecanicoId = viewModel.MecanicoId,
                FechaIngreso = DateTime.Now,
                Estado = EstadoReparacion.Recibida,
                Diagnostico = viewModel.Diagnostico,
                Observaciones = viewModel.Observaciones,
                Prioridad = viewModel.Prioridad,
                CostoManoObra = viewModel.CostoManoObra,
                CostoRepuestos = viewModel.CostoRepuestos,
                EsGarantia = viewModel.EsGarantia,
                FechaActualizacion = DateTime.Now
            };

            var created = await _reparacionRepository.AddAsync(reparacion);
            return await MapToReparacionListViewModel(created);
        }

        public async Task<ReparacionCreateViewModel> CreateReparacionAsync(ReparacionCreateViewModel viewModel)
        {
            var reparacion = new Reparacion
            {
                Numero = await _reparacionRepository.GenerarNumeroReparacionAsync(),
                ClienteId = viewModel.ClienteId,
                BicicletaId = viewModel.BicicletaId,
                MecanicoId = viewModel.MecanicoId,
                FechaIngreso = DateTime.Now,
                Estado = EstadoReparacion.Recibida,
                Diagnostico = viewModel.Diagnostico,
                Observaciones = viewModel.Observaciones,
                Prioridad = viewModel.Prioridad,
                CostoManoObra = viewModel.CostoManoObra,
                CostoRepuestos = viewModel.CostoRepuestos,
                EsGarantia = viewModel.EsGarantia,
                FechaActualizacion = DateTime.Now
            };

            var created = await _reparacionRepository.AddAsync(reparacion);
            
            return new ReparacionCreateViewModel
            {
                Id = created.Id,
                Numero = created.Numero,
                ClienteId = created.ClienteId,
                BicicletaId = created.BicicletaId,
                MecanicoId = created.MecanicoId,
                FechaIngreso = created.FechaIngreso,
                Estado = created.Estado,
                Diagnostico = created.Diagnostico,
                Observaciones = created.Observaciones,
                Prioridad = created.Prioridad,
                CostoManoObra = created.CostoManoObra,
                CostoRepuestos = created.CostoRepuestos,
                EsGarantia = created.EsGarantia,
                FechaActualizacion = created.FechaActualizacion
            };
        }

        public async Task UpdateAsync(ReparacionListViewModel viewModel)
        {
            var reparacion = await _reparacionRepository.GetByIdAsync(viewModel.Id);
            if (reparacion == null) throw new Exception($"Reparación con ID {viewModel.Id} no encontrada");

            reparacion.ClienteId = viewModel.ClienteId;
            reparacion.BicicletaId = viewModel.BicicletaId;
            reparacion.MecanicoId = viewModel.MecanicoId;
            reparacion.Diagnostico = viewModel.Diagnostico;
            reparacion.Observaciones = viewModel.Observaciones;
            reparacion.Prioridad = viewModel.Prioridad;
            reparacion.CostoManoObra = viewModel.CostoManoObra;
            reparacion.CostoRepuestos = viewModel.CostoRepuestos;
            reparacion.EsGarantia = viewModel.EsGarantia;
            reparacion.FechaActualizacion = DateTime.Now;

            await _reparacionRepository.UpdateAsync(reparacion);
        }

        /// <summary>
        /// CORREGIDO: Actualiza una reparación existente
        /// </summary>
        public async Task UpdateReparacionAsync(Reparacion reparacion)
        {
            await _reparacionRepository.UpdateAsync(reparacion);
        }

        public async Task UpdateAsync(Reparacion reparacion)
        {
            await _reparacionRepository.UpdateAsync(reparacion);
        }

        public async Task DeleteAsync(int id)
        {
            await _reparacionRepository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _reparacionRepository.ExistsAsync(id);
        }

        public async Task<IEnumerable<ReparacionListViewModel>> GetByClienteIdAsync(int clienteId)
        {
            var reparaciones = await _reparacionRepository.GetByClienteIdAsync(clienteId);
            var result = new List<ReparacionListViewModel>();
            
            foreach (var r in reparaciones)
            {
                result.Add(await MapToReparacionListViewModel(r));
            }
            
            return result;
        }

        public async Task<IEnumerable<ReparacionListViewModel>> GetByBicicletaIdAsync(int bicicletaId)
        {
            var reparaciones = await _reparacionRepository.GetByBicicletaIdAsync(bicicletaId);
            var result = new List<ReparacionListViewModel>();
            
            foreach (var r in reparaciones)
            {
                result.Add(await MapToReparacionListViewModel(r));
            }
            
            return result;
        }

        public async Task<IEnumerable<ReparacionListViewModel>> GetByEstadoAsync(EstadoReparacion estado)
        {
            var reparaciones = await _reparacionRepository.GetByEstadoAsync(estado);
            var result = new List<ReparacionListViewModel>();
            
            foreach (var r in reparaciones)
            {
                result.Add(await MapToReparacionListViewModel(r));
            }
            
            return result;
        }

        public async Task<IEnumerable<ReparacionListViewModel>> GetByMecanicoIdAsync(int mecanicoId)
        {
            var reparaciones = await _reparacionRepository.GetByMecanicoIdAsync(mecanicoId);
            var result = new List<ReparacionListViewModel>();
            
            foreach (var r in reparaciones)
            {
                result.Add(await MapToReparacionListViewModel(r));
            }
            
            return result;
        }

        public async Task<IEnumerable<ReparacionListViewModel>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var reparaciones = await _reparacionRepository.GetByDateRangeAsync(startDate, endDate);
            var result = new List<ReparacionListViewModel>();
            
            foreach (var r in reparaciones)
            {
                result.Add(await MapToReparacionListViewModel(r));
            }
            
            return result;
        }

        public async Task UpdateEstadoAsync(int id, EstadoReparacion nuevoEstado)
        {
            await _reparacionRepository.UpdateEstadoAsync(id, nuevoEstado);
        }

        public async Task<ReparacionEstadisticas> GetEstadisticasAsync()
        {
            return await _reparacionRepository.GetEstadisticasAsync();
        }

        public async Task AsignarMecanicoAsync(int reparacionId, int mecanicoId)
        {
            var reparacion = await _reparacionRepository.GetByIdAsync(reparacionId);
            if (reparacion == null) throw new Exception($"Reparación con ID {reparacionId} no encontrada");
            
            var mecanico = await _mecanicoRepository.GetByIdAsync(mecanicoId);
            if (mecanico == null) throw new Exception($"Mecánico con ID {mecanicoId} no encontrado");

            reparacion.MecanicoId = mecanicoId;
            reparacion.FechaActualizacion = DateTime.Now;
            
            await _reparacionRepository.UpdateAsync(reparacion);
        }

        private async Task<ReparacionListViewModel> MapToReparacionListViewModel(Reparacion reparacion)
        {
            var cliente = await _clienteRepository.GetByIdAsync(reparacion.ClienteId);
            var bicicleta = await _bicicletaRepository.GetByIdAsync(reparacion.BicicletaId);
            var mecanico = reparacion.MecanicoId.HasValue ? 
                await _mecanicoRepository.GetByIdAsync(reparacion.MecanicoId.Value) : null;

            return new ReparacionListViewModel
            {
                Id = reparacion.Id,
                Numero = reparacion.Numero,
                ClienteId = reparacion.ClienteId,
                BicicletaId = reparacion.BicicletaId,
                MecanicoId = reparacion.MecanicoId,
                FechaIngreso = reparacion.FechaIngreso,
                FechaEntrega = reparacion.FechaEntrega,
                Estado = reparacion.Estado,
                Diagnostico = reparacion.Diagnostico,
                Observaciones = reparacion.Observaciones,
                Prioridad = reparacion.Prioridad,
                CostoManoObra = reparacion.CostoManoObra,
                CostoRepuestos = reparacion.CostoRepuestos,
                EsGarantia = reparacion.EsGarantia,
                FechaActualizacion = reparacion.FechaActualizacion,
                CostoTotal = reparacion.CostoTotal,
                DiasEnTaller = reparacion.DiasEnTaller,
                EstaAtrasada = reparacion.EstaAtrasada,
                ClienteNombre = cliente?.NombreCompleto ?? "Cliente no encontrado",
                BicicletaNombre = bicicleta?.NombreCompleto ?? "Bicicleta no encontrada",
                MecanicoNombre = mecanico?.NombreCompleto ?? "No asignado"
            };
        }
    }
}