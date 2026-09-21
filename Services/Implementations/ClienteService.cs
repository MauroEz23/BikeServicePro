using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeServicePro.Models;
using BikeServicePro.Repositories.Interfaces;
using BikeServicePro.Services.Interfaces;
using BikeServicePro.ViewModels.Clientes;

namespace BikeServicePro.Services.Implementations
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IBicicletaRepository _bicicletaRepository;

        public ClienteService(IClienteRepository clienteRepository, IBicicletaRepository bicicletaRepository)
        {
            _clienteRepository = clienteRepository;
            _bicicletaRepository = bicicletaRepository;
        }

        public async Task<IEnumerable<ClienteListViewModel>> GetAllAsync()
        {
            var clientes = await _clienteRepository.GetAllAsync();
            return clientes.Select(c => MapToClienteListViewModel(c));
        }

        public async Task<ClienteListViewModel?> GetByIdAsync(int id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            return cliente != null ? MapToClienteListViewModel(cliente) : null;
        }

        public async Task<ClienteDetailViewModel?> GetClienteDetailAsync(int id)
        {
            var cliente = await _clienteRepository.GetClienteWithBicicletasAsync(id);
            if (cliente == null) return null;

            var bicicletas = await _bicicletaRepository.GetByClienteIdAsync(id);
            
            return new ClienteDetailViewModel
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellidos = cliente.Apellidos,
                Email = cliente.Email,
                Telefono = cliente.Telefono,
                Direccion = cliente.Direccion,
                Observaciones = cliente.Observaciones, // CORREGIDO: Ya es nullable
                FechaRegistro = cliente.FechaRegistro,
                Activo = cliente.Activo,
                NombreCompleto = cliente.NombreCompleto,
                Bicicletas = bicicletas.Select(b => new BicicletaBasicViewModel
                {
                    Id = b.Id,
                    Marca = b.Marca,
                    Modelo = b.Modelo,
                    Anio = b.Anio,
                    Color = b.Color,
                    NumeroSerie = b.NumeroSerie
                }).ToList()
            };
        }

        public async Task<ClienteListViewModel> CreateAsync(ClienteListViewModel viewModel)
        {
            var cliente = new Cliente
            {
                Nombre = viewModel.Nombre,
                Apellidos = viewModel.Apellidos,
                Email = viewModel.Email,
                Telefono = viewModel.Telefono,
                Direccion = viewModel.Direccion ?? string.Empty,
                Observaciones = viewModel.Observaciones,
                FechaRegistro = DateTime.Now,
                Activo = true
            };

            var created = await _clienteRepository.AddAsync(cliente);
            return MapToClienteListViewModel(created);
        }

        public async Task<ClienteCreateViewModel> CreateClienteAsync(ClienteCreateViewModel viewModel)
        {
            var cliente = new Cliente
            {
                Nombre = viewModel.Nombre,
                Apellidos = viewModel.Apellidos,
                Email = viewModel.Email,
                Telefono = viewModel.Telefono,
                Direccion = viewModel.Direccion ?? string.Empty,
                Observaciones = viewModel.Observaciones,
                FechaRegistro = DateTime.Now,
                Activo = true
            };

            var created = await _clienteRepository.AddAsync(cliente);
            
            return new ClienteCreateViewModel
            {
                Id = created.Id,
                Nombre = created.Nombre,
                Apellidos = created.Apellidos,
                Email = created.Email,
                Telefono = created.Telefono,
                Direccion = created.Direccion,
                Observaciones = created.Observaciones,
                Activo = created.Activo
            };
        }

        public async Task UpdateAsync(ClienteListViewModel viewModel)
        {
            var cliente = await _clienteRepository.GetByIdAsync(viewModel.Id);
            if (cliente == null) throw new Exception($"Cliente con ID {viewModel.Id} no encontrado");

            cliente.Nombre = viewModel.Nombre;
            cliente.Apellidos = viewModel.Apellidos;
            cliente.Email = viewModel.Email;
            cliente.Telefono = viewModel.Telefono;
            cliente.Direccion = viewModel.Direccion ?? string.Empty;
            cliente.Observaciones = viewModel.Observaciones;
            cliente.Activo = viewModel.Activo;

            await _clienteRepository.UpdateAsync(cliente);
        }

        public async Task UpdateClienteAsync(ClienteCreateViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));
            if (viewModel.Id <= 0) throw new Exception("ID de cliente inválido");

            var cliente = await _clienteRepository.GetByIdAsync(viewModel.Id);
            if (cliente == null) 
                throw new Exception($"Cliente con ID {viewModel.Id} no encontrado");

            cliente.Nombre = viewModel.Nombre;
            cliente.Apellidos = viewModel.Apellidos;
            cliente.Email = viewModel.Email;
            cliente.Telefono = viewModel.Telefono;
            cliente.Direccion = viewModel.Direccion ?? string.Empty;
            cliente.Observaciones = viewModel.Observaciones;
            cliente.Activo = viewModel.Activo;

            await _clienteRepository.UpdateAsync(cliente);
        }

        public async Task DeleteAsync(int id)
        {
            await _clienteRepository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _clienteRepository.ExistsAsync(id);
        }

        public async Task<IEnumerable<ClienteListViewModel>> SearchAsync(string searchTerm)
        {
            var clientes = await _clienteRepository.SearchAsync(searchTerm);
            return clientes.Select(c => MapToClienteListViewModel(c));
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _clienteRepository.GetTotalCountAsync();
        }

        public async Task<IEnumerable<ClienteListViewModel>> GetActiveClientesAsync()
        {
            var clientes = await _clienteRepository.GetActiveClientesAsync();
            return clientes.Select(c => MapToClienteListViewModel(c));
        }

        private ClienteListViewModel MapToClienteListViewModel(Cliente cliente)
        {
            return new ClienteListViewModel
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellidos = cliente.Apellidos,
                Email = cliente.Email,
                Telefono = cliente.Telefono,
                Direccion = cliente.Direccion,
                Observaciones = cliente.Observaciones,
                FechaRegistro = cliente.FechaRegistro,
                Activo = cliente.Activo,
                NombreCompleto = cliente.NombreCompleto
            };
        }
    }
}