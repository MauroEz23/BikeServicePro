using System;
using System.Threading.Tasks;
using BikeServicePro.Models;
using BikeServicePro.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BikeServicePro.Helpers
{
    /// <summary>
    /// Inicializador de datos para el sistema
    /// Crea datos de ejemplo si los archivos JSON están vacíos
    /// </summary>
    public static class DataInitializer
    {
        /// <summary>
        /// Inicializa los datos del sistema
        /// </summary>
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                // Obtener repositorios
                var usuarioRepo = services.GetRequiredService<IUsuarioRepository>();
                var clienteRepo = services.GetRequiredService<IClienteRepository>();
                var bicicletaRepo = services.GetRequiredService<IBicicletaRepository>();
                var servicioRepo = services.GetRequiredService<IServicioRepository>();
                var inventarioRepo = services.GetRequiredService<IInventarioRepository>();
                var reparacionRepo = services.GetRequiredService<IReparacionRepository>();
                var mecanicoRepo = services.GetRequiredService<IMecanicoRepository>();

                // Verificar si ya hay datos
                var usuarios = await usuarioRepo.GetAllAsync();
                var usuariosList = usuarios as System.Collections.Generic.List<Usuario> ?? new System.Collections.Generic.List<Usuario>(usuarios);
                
                if (usuariosList.Count == 0)
                {
                    Console.WriteLine("Inicializando datos de ejemplo...");

                    // Generar datos de ejemplo
                    var (usuariosSeed, clientesSeed, bicicletasSeed, serviciosSeed, mecanicosSeed, inventariosSeed, reparacionesSeed) 
                        = SeedDataHelper.GenerateSeedData();

                    // Guardar usuarios
                    foreach (var usuario in usuariosSeed)
                    {
                        await usuarioRepo.AddAsync(usuario);
                    }

                    // Guardar clientes
                    foreach (var cliente in clientesSeed)
                    {
                        await clienteRepo.AddAsync(cliente);
                    }

                    // Guardar bicicletas
                    foreach (var bicicleta in bicicletasSeed)
                    {
                        await bicicletaRepo.AddAsync(bicicleta);
                    }

                    // Guardar servicios
                    foreach (var servicio in serviciosSeed)
                    {
                        await servicioRepo.AddAsync(servicio);
                    }

                    // Guardar mecánicos
                    foreach (var mecanico in mecanicosSeed)
                    {
                        await mecanicoRepo.AddAsync(mecanico);
                    }

                    // Guardar inventario
                    foreach (var inventario in inventariosSeed)
                    {
                        await inventarioRepo.AddAsync(inventario);
                    }

                    // Guardar reparaciones
                    foreach (var reparacion in reparacionesSeed)
                    {
                        await reparacionRepo.AddAsync(reparacion);
                    }

                    Console.WriteLine("Datos de ejemplo inicializados correctamente.");
                }
                else
                {
                    Console.WriteLine($"El sistema ya contiene datos. Usuarios encontrados: {usuariosList.Count}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al inicializar datos: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                throw;
            }
        }
    }
}