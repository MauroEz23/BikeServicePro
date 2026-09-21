using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeServicePro.Models;
using BikeServicePro.Repositories.Interfaces;
using BikeServicePro.Services.Interfaces;
using BikeServicePro.ViewModels.Bicicletas;

namespace BikeServicePro.Services.Implementations
{
    public class BicicletaService : IBicicletaService
    {
        private readonly IBicicletaRepository _bicicletaRepository;
        private readonly IClienteRepository _clienteRepository;

        public BicicletaService(IBicicletaRepository bicicletaRepository, IClienteRepository clienteRepository)
        {
            _bicicletaRepository = bicicletaRepository;
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<BicicletaListViewModel>> GetAllAsync()
        {
            var bicicletas = await _bicicletaRepository.GetAllAsync();
            var result = new List<BicicletaListViewModel>();
            
            foreach (var b in bicicletas)
            {
                var cliente = await _clienteRepository.GetByIdAsync(b.ClienteId);
                result.Add(MapToBicicletaListViewModel(b, cliente));
            }
            
            return result;
        }

        public async Task<BicicletaListViewModel?> GetByIdAsync(int id)
        {
            var bicicleta = await _bicicletaRepository.GetByIdAsync(id);
            if (bicicleta == null) return null;
            
            var cliente = await _clienteRepository.GetByIdAsync(bicicleta.ClienteId);
            return MapToBicicletaListViewModel(bicicleta, cliente);
        }

        public async Task<BicicletaDetailViewModel?> GetBicicletaDetailAsync(int id)
        {
            var bicicleta = await _bicicletaRepository.GetWithClienteAsync(id);
            if (bicicleta == null) return null;

            var cliente = await _clienteRepository.GetByIdAsync(bicicleta.ClienteId);
            
            return new BicicletaDetailViewModel
            {
                Id = bicicleta.Id,
                ClienteId = bicicleta.ClienteId,
                Marca = bicicleta.Marca,
                Modelo = bicicleta.Modelo,
                Anio = bicicleta.Anio,
                NumeroSerie = bicicleta.NumeroSerie,
                Color = bicicleta.Color,
                Material = bicicleta.Material,
                Peso = bicicleta.Peso,
                Talla = bicicleta.Talla,
                Grupo = bicicleta.Grupo,
                TipoFrenos = bicicleta.TipoFrenos,
                FotoUrl = bicicleta.FotoUrl,
                FechaRegistro = bicicleta.FechaRegistro,
                Activo = bicicleta.Activo,
                NombreCompleto = bicicleta.NombreCompleto,
                ClienteNombre = cliente?.NombreCompleto ?? "Cliente no encontrado"
            };
        }

        public async Task<BicicletaListViewModel> CreateAsync(BicicletaListViewModel viewModel)
        {
            var bicicleta = new Bicicleta
            {
                ClienteId = viewModel.ClienteId,
                Marca = viewModel.Marca,
                Modelo = viewModel.Modelo,
                Anio = viewModel.Anio,
                NumeroSerie = viewModel.NumeroSerie,
                Color = viewModel.Color,
                Material = viewModel.Material,
                Peso = viewModel.Peso,
                Talla = viewModel.Talla,
                Grupo = viewModel.Grupo,
                TipoFrenos = viewModel.TipoFrenos,
                FotoUrl = viewModel.FotoUrl,
                FechaRegistro = DateTime.Now,
                Activo = true
            };

            var created = await _bicicletaRepository.AddAsync(bicicleta);
            var cliente = await _clienteRepository.GetByIdAsync(created.ClienteId);
            return MapToBicicletaListViewModel(created, cliente);
        }

        public async Task<BicicletaCreateViewModel> CreateBicicletaAsync(BicicletaCreateViewModel viewModel)
        {
            var bicicleta = new Bicicleta
            {
                ClienteId = viewModel.ClienteId,
                Marca = viewModel.Marca,
                Modelo = viewModel.Modelo,
                Anio = viewModel.Anio,
                NumeroSerie = viewModel.NumeroSerie,
                Color = viewModel.Color,
                Material = viewModel.Material,
                Peso = viewModel.Peso,
                Talla = viewModel.Talla,
                Grupo = viewModel.Grupo,
                TipoFrenos = viewModel.TipoFrenos,
                FotoUrl = viewModel.FotoUrl,
                FechaRegistro = DateTime.Now,
                Activo = true
            };

            var created = await _bicicletaRepository.AddAsync(bicicleta);
            
            return new BicicletaCreateViewModel
            {
                Id = created.Id,
                ClienteId = created.ClienteId,
                Marca = created.Marca,
                Modelo = created.Modelo,
                Anio = created.Anio,
                NumeroSerie = created.NumeroSerie,
                Color = created.Color,
                Material = created.Material,
                Peso = created.Peso,
                Talla = created.Talla,
                Grupo = created.Grupo,
                TipoFrenos = created.TipoFrenos,
                FotoUrl = created.FotoUrl,
                Activo = created.Activo
            };
        }

        public async Task UpdateAsync(BicicletaListViewModel viewModel)
        {
            var bicicleta = await _bicicletaRepository.GetByIdAsync(viewModel.Id);
            if (bicicleta == null) throw new Exception($"Bicicleta con ID {viewModel.Id} no encontrada");

            bicicleta.ClienteId = viewModel.ClienteId;
            bicicleta.Marca = viewModel.Marca;
            bicicleta.Modelo = viewModel.Modelo;
            bicicleta.Anio = viewModel.Anio;
            bicicleta.NumeroSerie = viewModel.NumeroSerie;
            bicicleta.Color = viewModel.Color;
            bicicleta.Material = viewModel.Material;
            bicicleta.Peso = viewModel.Peso;
            bicicleta.Talla = viewModel.Talla;
            bicicleta.Grupo = viewModel.Grupo;
            bicicleta.TipoFrenos = viewModel.TipoFrenos;
            bicicleta.FotoUrl = viewModel.FotoUrl;
            bicicleta.Activo = viewModel.Activo;

            await _bicicletaRepository.UpdateAsync(bicicleta);
        }

        /// <summary>
        /// CORREGIDO: Actualiza una bicicleta existente desde el ViewModel de creación
        /// </summary>
        public async Task UpdateBicicletaAsync(BicicletaCreateViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));
            if (viewModel.Id <= 0) throw new Exception("ID de bicicleta inválido");

            var bicicleta = await _bicicletaRepository.GetByIdAsync(viewModel.Id);
            if (bicicleta == null) 
                throw new Exception($"Bicicleta con ID {viewModel.Id} no encontrada");

            bicicleta.ClienteId = viewModel.ClienteId;
            bicicleta.Marca = viewModel.Marca;
            bicicleta.Modelo = viewModel.Modelo;
            bicicleta.Anio = viewModel.Anio;
            bicicleta.NumeroSerie = viewModel.NumeroSerie;
            bicicleta.Color = viewModel.Color;
            bicicleta.Material = viewModel.Material;
            bicicleta.Peso = viewModel.Peso;
            bicicleta.Talla = viewModel.Talla;
            bicicleta.Grupo = viewModel.Grupo;
            bicicleta.TipoFrenos = viewModel.TipoFrenos;
            bicicleta.FotoUrl = viewModel.FotoUrl;
            bicicleta.Activo = viewModel.Activo;

            await _bicicletaRepository.UpdateAsync(bicicleta);
        }

        public async Task DeleteAsync(int id)
        {
            await _bicicletaRepository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _bicicletaRepository.ExistsAsync(id);
        }

        public async Task<IEnumerable<BicicletaListViewModel>> GetByClienteIdAsync(int clienteId)
        {
            var bicicletas = await _bicicletaRepository.GetByClienteIdAsync(clienteId);
            var cliente = await _clienteRepository.GetByIdAsync(clienteId);
            
            return bicicletas.Select(b => MapToBicicletaListViewModel(b, cliente));
        }

        public async Task<IEnumerable<BicicletaListViewModel>> SearchAsync(string searchTerm)
        {
            var bicicletas = await _bicicletaRepository.SearchAsync(searchTerm);
            var result = new List<BicicletaListViewModel>();
            
            foreach (var b in bicicletas)
            {
                var cliente = await _clienteRepository.GetByIdAsync(b.ClienteId);
                result.Add(MapToBicicletaListViewModel(b, cliente));
            }
            
            return result;
        }

        public async Task<BicicletaListViewModel?> GetByNumeroSerieAsync(string numeroSerie)
        {
            var bicicleta = await _bicicletaRepository.GetByNumeroSerieAsync(numeroSerie);
            if (bicicleta == null) return null;
            
            var cliente = await _clienteRepository.GetByIdAsync(bicicleta.ClienteId);
            return MapToBicicletaListViewModel(bicicleta, cliente);
        }

        public async Task<Dictionary<int, int>> GetCountByClienteAsync()
        {
            return await _bicicletaRepository.GetCountByClienteAsync();
        }

        private BicicletaListViewModel MapToBicicletaListViewModel(Bicicleta bicicleta, Cliente? cliente)
        {
            return new BicicletaListViewModel
            {
                Id = bicicleta.Id,
                ClienteId = bicicleta.ClienteId,
                Marca = bicicleta.Marca,
                Modelo = bicicleta.Modelo,
                Anio = bicicleta.Anio,
                NumeroSerie = bicicleta.NumeroSerie,
                Color = bicicleta.Color,
                Material = bicicleta.Material,
                Peso = bicicleta.Peso,
                Talla = bicicleta.Talla,
                Grupo = bicicleta.Grupo,
                TipoFrenos = bicicleta.TipoFrenos,
                FotoUrl = bicicleta.FotoUrl,
                FechaRegistro = bicicleta.FechaRegistro,
                Activo = bicicleta.Activo,
                NombreCompleto = bicicleta.NombreCompleto,
                ClienteNombre = cliente?.NombreCompleto ?? "Cliente no encontrado"
            };
        }
    }
}