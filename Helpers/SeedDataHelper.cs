using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using BikeServicePro.Models;

namespace BikeServicePro.Helpers
{
    /// <summary>
    /// Helper para generar datos iniciales de ejemplo
    /// Asegura que el sistema nunca aparezca vacío
    /// </summary>
    public static class SeedDataHelper
    {
        private static readonly Random _random = new();

        /// <summary>
        /// Genera datos de ejemplo para todas las entidades
        /// </summary>
        public static (List<Usuario> Usuarios, List<Cliente> Clientes, List<Bicicleta> Bicicletas, 
                       List<Servicio> Servicios, List<Mecanico> Mecanicos, List<Inventario> Inventarios,
                       List<Reparacion> Reparaciones) GenerateSeedData()
        {
            var usuarios = GenerateUsuarios();
            var servicios = GenerateServicios();
            var mecanicos = GenerateMecanicos();
            var clientes = GenerateClientes();
            var bicicletas = GenerateBicicletas(clientes);
            var inventarios = GenerateInventarios();
            var reparaciones = GenerateReparaciones(clientes, bicicletas, mecanicos, servicios);

            return (usuarios, clientes, bicicletas, servicios, mecanicos, inventarios, reparaciones);
        }

        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        private static List<Usuario> GenerateUsuarios()
        {
            return new List<Usuario>
            {
                new()
                {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = HashPassword("admin123"),
                    NombreCompleto = "Administrador del Sistema",
                    Email = "admin@bikeservice.com",
                    Rol = RolUsuario.Administrador,
                    Activo = true,
                    FechaCreacion = DateTime.Now.AddMonths(-6)
                },
                new()
                {
                    Id = 2,
                    Username = "recepcionista",
                    PasswordHash = HashPassword("recep123"),
                    NombreCompleto = "María Recepcionista",
                    Email = "maria@bikeservice.com",
                    Rol = RolUsuario.Recepcionista,
                    Activo = true,
                    FechaCreacion = DateTime.Now.AddMonths(-4)
                },
                new()
                {
                    Id = 3,
                    Username = "mecanico1",
                    PasswordHash = HashPassword("meca123"),
                    NombreCompleto = "Carlos Mecánico",
                    Email = "carlos@bikeservice.com",
                    Rol = RolUsuario.Mecanico,
                    Activo = true,
                    FechaCreacion = DateTime.Now.AddMonths(-3)
                },
                new()
                {
                    Id = 4,
                    Username = "mecanico2",
                    PasswordHash = HashPassword("meca123"),
                    NombreCompleto = "Ana Mecánico",
                    Email = "ana@bikeservice.com",
                    Rol = RolUsuario.Mecanico,
                    Activo = true,
                    FechaCreacion = DateTime.Now.AddMonths(-2)
                }
            };
        }

        private static List<Servicio> GenerateServicios()
        {
            return new List<Servicio>
            {
                new() { Id = 1, Nombre = "Cambio de Cadena", Descripcion = "Reemplazo de cadena desgastada", PrecioBase = 25.00m, TiempoEstimado = 30, Categoria = CategoriaServicio.Mantenimiento, Icono = "bi-gear", Activo = true, FechaCreacion = DateTime.Now.AddMonths(-6) },
                new() { Id = 2, Nombre = "Cambio de Cassette", Descripcion = "Reemplazo de cassette completo", PrecioBase = 35.00m, TiempoEstimado = 45, Categoria = CategoriaServicio.Mantenimiento, Icono = "bi-gear", Activo = true, FechaCreacion = DateTime.Now.AddMonths(-6) },
                new() { Id = 3, Nombre = "Cambio de Cables", Descripcion = "Reemplazo de cables de freno y cambios", PrecioBase = 20.00m, TiempoEstimado = 40, Categoria = CategoriaServicio.Mantenimiento, Icono = "bi-sliders2", Activo = true, FechaCreacion = DateTime.Now.AddMonths(-6) },
                new() { Id = 4, Nombre = "Cambio de Fundas", Descripcion = "Reemplazo de fundas de cables", PrecioBase = 15.00m, TiempoEstimado = 30, Categoria = CategoriaServicio.Mantenimiento, Icono = "bi-sliders2", Activo = true, FechaCreacion = DateTime.Now.AddMonths(-6) },
                new() { Id = 5, Nombre = "Cambio de Llantas", Descripcion = "Reemplazo de llantas y cámaras", PrecioBase = 30.00m, TiempoEstimado = 35, Categoria = CategoriaServicio.Mantenimiento, Icono = "bi-circle", Activo = true, FechaCreacion = DateTime.Now.AddMonths(-6) },
                new() { Id = 6, Nombre = "Cambio de Cinta", Descripcion = "Reemplazo de cinta de manillar", PrecioBase = 15.00m, TiempoEstimado = 25, Categoria = CategoriaServicio.Mantenimiento, Icono = "bi-magic", Activo = true, FechaCreacion = DateTime.Now.AddMonths(-6) },
                new() { Id = 7, Nombre = "Centrado de Ruedas", Descripcion = "Centrado y tensado de rayos", PrecioBase = 20.00m, TiempoEstimado = 30, Categoria = CategoriaServicio.Ajuste, Icono = "bi-arrows-spin", Activo = true, FechaCreacion = DateTime.Now.AddMonths(-6) },
                new() { Id = 8, Nombre = "Ajuste de Cambios", Descripcion = "Ajuste de cambios y desviadores", PrecioBase = 15.00m, TiempoEstimado = 20, Categoria = CategoriaServicio.Ajuste, Icono = "bi-gear-wide-connected", Activo = true, FechaCreacion = DateTime.Now.AddMonths(-6) },
                new() { Id = 9, Nombre = "Lavado Completo", Descripcion = "Lavado y detallado de la bicicleta", PrecioBase = 25.00m, TiempoEstimado = 45, Categoria = CategoriaServicio.Limpieza, Icono = "bi-droplet", Activo = true, FechaCreacion = DateTime.Now.AddMonths(-6) },
                new() { Id = 10, Nombre = "Lubricación General", Descripcion = "Lubricación de cadena y componentes", PrecioBase = 10.00m, TiempoEstimado = 15, Categoria = CategoriaServicio.Mantenimiento, Icono = "bi-droplet-half", Activo = true, FechaCreacion = DateTime.Now.AddMonths(-6) },
                new() { Id = 11, Nombre = "Servicio Básico", Descripcion = "Revisión y ajustes básicos", PrecioBase = 40.00m, TiempoEstimado = 60, Categoria = CategoriaServicio.Mantenimiento, Icono = "bi-tools", Activo = true, FechaCreacion = DateTime.Now.AddMonths(-6) },
                new() { Id = 12, Nombre = "Servicio Completo", Descripcion = "Revisión completa de todos los componentes", PrecioBase = 80.00m, TiempoEstimado = 120, Categoria = CategoriaServicio.Mantenimiento, Icono = "bi-tools", Activo = true, FechaCreacion = DateTime.Now.AddMonths(-6) },
                new() { Id = 13, Nombre = "Servicio Premium", Descripcion = "Servicio completo con diagnóstico avanzado", PrecioBase = 120.00m, TiempoEstimado = 180, Categoria = CategoriaServicio.Mantenimiento, Icono = "bi-star", Activo = true, FechaCreacion = DateTime.Now.AddMonths(-6) }
            };
        }

        private static List<Mecanico> GenerateMecanicos()
        {
            return new List<Mecanico>
            {
                new() 
                { 
                    Id = 1, 
                    Nombre = "Carlos", 
                    Apellidos = "González", 
                    Especialidad = "Mecánica de Ruta", 
                    Telefono = "555-1234", 
                    Email = "carlos.gonzalez@bikeservice.com",
                    FechaContratacion = new DateTime(2022, 1, 15),
                    Certificaciones = "Shimano Certified, SRAM Certified",
                    Activo = true,
                    HorarioTrabajo = "Lunes a Viernes 9:00 - 18:00"
                },
                new() 
                { 
                    Id = 2, 
                    Nombre = "Ana", 
                    Apellidos = "Martínez", 
                    Especialidad = "Suspensiones y Dirección", 
                    Telefono = "555-5678", 
                    Email = "ana.martinez@bikeservice.com",
                    FechaContratacion = new DateTime(2023, 3, 1),
                    Certificaciones = "Fox Certified, Rockshox Certified",
                    Activo = true,
                    HorarioTrabajo = "Martes a Sábado 10:00 - 19:00"
                },
                new() 
                { 
                    Id = 3, 
                    Nombre = "Luis", 
                    Apellidos = "Rodríguez", 
                    Especialidad = "Componentes Eléctricos", 
                    Telefono = "555-9012", 
                    Email = "luis.rodriguez@bikeservice.com",
                    FechaContratacion = new DateTime(2023, 6, 10),
                    Certificaciones = "Bosch eBike Certified",
                    Activo = true,
                    HorarioTrabajo = "Lunes a Viernes 8:00 - 17:00"
                }
            };
        }

        private static List<Cliente> GenerateClientes()
        {
            var nombres = new[] { "Juan", "María", "Pedro", "Laura", "Roberto", "Carmen", "Miguel", "Isabel", "Antonio", "Elena" };
            var apellidos = new[] { "Pérez", "López", "García", "Fernández", "Sánchez", "Ramírez", "Torres", "Rivera", "Morales", "Ortega" };
            var dominios = new[] { "gmail.com", "hotmail.com", "yahoo.com", "outlook.com" };
            var clientes = new List<Cliente>();

            for (int i = 1; i <= 10; i++)
            {
                var nombre = nombres[_random.Next(nombres.Length)];
                var apellido = apellidos[_random.Next(apellidos.Length)];
                var dominio = dominios[_random.Next(dominios.Length)];
                
                clientes.Add(new Cliente
                {
                    Id = i,
                    Nombre = nombre,
                    Apellidos = apellido,
                    Email = $"{nombre.ToLower()}.{apellido.ToLower()}@{dominio}",
                    Telefono = $"555-{_random.Next(1000, 9999):D4}",
                    Direccion = $"Calle {nombres[_random.Next(nombres.Length)]} {_random.Next(100, 999)}",
                    Observaciones = i % 3 == 0 ? "Cliente frecuente" : i % 5 == 0 ? "Cliente VIP" : "",
                    FechaRegistro = DateTime.Now.AddDays(-_random.Next(1, 180)),
                    Activo = true
                });
            }

            return clientes;
        }

        private static List<Bicicleta> GenerateBicicletas(List<Cliente> clientes)
        {
            var bicicletas = new List<Bicicleta>();
            var marcas = new[] { "Trek", "Specialized", "Cannondale", "Giant", "Bianchi", "Pinarello", "Scott", "Cervélo", "Wilier", "Orbea" };
            var modelos = new[] { "Domane", "Tarmac", "SuperSix", "Defy", "Infinito", "Dogma", "Addict", "R5", "Zero", "Orca" };
            var colores = new[] { "Rojo", "Azul", "Negro", "Blanco", "Verde", "Amarillo", "Plateado", "Gris", "Naranja", "Morado" };
            var materiales = new[] { "Carbono", "Aluminio", "Acero", "Titanio" };
            var tallas = new[] { "XS", "S", "M", "L", "XL", "XXL" };
            var grupos = new[] { "Shimano 105", "Shimano Ultegra", "Shimano Dura-Ace", "SRAM Rival", "SRAM Force", "SRAM Red" };
            var tiposFrenos = new[] { "Disco", "V-Brake", "Caliper" };

            for (int i = 1; i <= 15; i++)
            {
                var cliente = clientes[_random.Next(clientes.Count)];
                bicicletas.Add(new Bicicleta
                {
                    Id = i,
                    ClienteId = cliente.Id,
                    Marca = marcas[_random.Next(marcas.Length)],
                    Modelo = modelos[_random.Next(modelos.Length)],
                    Anio = 2020 + _random.Next(6),
                    NumeroSerie = $"SN-{i:D4}-{DateTime.Now.Year}-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}",
                    Color = colores[_random.Next(colores.Length)],
                    Material = materiales[_random.Next(materiales.Length)],
                    Peso = Math.Round(7.5 + _random.NextDouble() * 3, 1),
                    Talla = tallas[_random.Next(tallas.Length)],
                    Grupo = grupos[_random.Next(grupos.Length)],
                    TipoFrenos = tiposFrenos[_random.Next(tiposFrenos.Length)],
                    FechaRegistro = DateTime.Now.AddDays(-_random.Next(30, 180)),
                    Activo = true
                });
            }

            return bicicletas;
        }

        private static List<Inventario> GenerateInventarios()
        {
            return new List<Inventario>
            {
                new() { Id = 1, Nombre = "Cadena Shimano 105", Categoria = "Repuestos", SubCategoria = "Cadenas", CodigoInterno = "SH-105-C", Cantidad = 15, StockMinimo = 5, Proveedor = "Shimano Distribución", Costo = 25.00m, Precio = 45.00m, Ubicacion = "A1", Activo = true, FechaActualizacion = DateTime.Now },
                new() { Id = 2, Nombre = "Cadena Shimano Ultegra", Categoria = "Repuestos", SubCategoria = "Cadenas", CodigoInterno = "SH-ULT-C", Cantidad = 10, StockMinimo = 3, Proveedor = "Shimano Distribución", Costo = 35.00m, Precio = 65.00m, Ubicacion = "A2", Activo = true, FechaActualizacion = DateTime.Now },
                new() { Id = 3, Nombre = "Cassette Shimano 105 11-32", Categoria = "Repuestos", SubCategoria = "Cassettes", CodigoInterno = "SH-105-CS", Cantidad = 8, StockMinimo = 3, Proveedor = "Shimano Distribución", Costo = 45.00m, Precio = 85.00m, Ubicacion = "B1", Activo = true, FechaActualizacion = DateTime.Now },
                new() { Id = 4, Nombre = "Pastillas de Freno Disco", Categoria = "Repuestos", SubCategoria = "Frenos", CodigoInterno = "BR-PAST-01", Cantidad = 25, StockMinimo = 10, Proveedor = "Frenos Pro", Costo = 8.00m, Precio = 15.00m, Ubicacion = "C1", Activo = true, FechaActualizacion = DateTime.Now },
                new() { Id = 5, Nombre = "Discos de Freno 160mm", Categoria = "Repuestos", SubCategoria = "Frenos", CodigoInterno = "BR-DISC-160", Cantidad = 12, StockMinimo = 5, Proveedor = "Frenos Pro", Costo = 15.00m, Precio = 30.00m, Ubicacion = "C2", Activo = true, FechaActualizacion = DateTime.Now },
                new() { Id = 6, Nombre = "Rodamientos de Buje", Categoria = "Repuestos", SubCategoria = "Rodamientos", CodigoInterno = "ROD-BUJE-01", Cantidad = 30, StockMinimo = 10, Proveedor = "Rodamientos Elite", Costo = 5.00m, Precio = 12.00m, Ubicacion = "D1", Activo = true, FechaActualizacion = DateTime.Now },
                new() { Id = 7, Nombre = "Lubricante de Cadena Cerámico", Categoria = "Lubricantes", SubCategoria = "Cadenas", CodigoInterno = "LUB-CER-01", Cantidad = 20, StockMinimo = 5, Proveedor = "Lubricantes Pro", Costo = 10.00m, Precio = 20.00m, Ubicacion = "E1", Activo = true, FechaActualizacion = DateTime.Now },
                new() { Id = 8, Nombre = "Lubricante de Suspensión", Categoria = "Lubricantes", SubCategoria = "Suspensiones", CodigoInterno = "LUB-SUS-01", Cantidad = 15, StockMinimo = 3, Proveedor = "Lubricantes Pro", Costo = 12.00m, Precio = 25.00m, Ubicacion = "E2", Activo = true, FechaActualizacion = DateTime.Now },
                new() { Id = 9, Nombre = "Llanta Continental GP5000 25mm", Categoria = "Repuestos", SubCategoria = "Llantas", CodigoInterno = "LL-CON-GP5", Cantidad = 8, StockMinimo = 4, Proveedor = "Continental Distribución", Costo = 40.00m, Precio = 75.00m, Ubicacion = "F1", Activo = true, FechaActualizacion = DateTime.Now },
                new() { Id = 10, Nombre = "Cámara de Aire 700x25", Categoria = "Repuestos", SubCategoria = "Cámaras", CodigoInterno = "CAM-700-25", Cantidad = 30, StockMinimo = 10, Proveedor = "Cámaras Pro", Costo = 3.00m, Precio = 8.00m, Ubicacion = "F2", Activo = true, FechaActualizacion = DateTime.Now },
                new() { Id = 11, Nombre = "Herramienta Multiusos", Categoria = "Herramientas", SubCategoria = "Generales", CodigoInterno = "HER-MULTI-01", Cantidad = 10, StockMinimo = 3, Proveedor = "Tool Pro", Costo = 15.00m, Precio = 30.00m, Ubicacion = "G1", Activo = true, FechaActualizacion = DateTime.Now },
                new() { Id = 12, Nombre = "Extractor de Cassette", Categoria = "Herramientas", SubCategoria = "Especializadas", CodigoInterno = "HER-EXT-CS", Cantidad = 5, StockMinimo = 2, Proveedor = "Tool Pro", Costo = 20.00m, Precio = 40.00m, Ubicacion = "G2", Activo = true, FechaActualizacion = DateTime.Now }
            };
        }

        private static List<Reparacion> GenerateReparaciones(List<Cliente> clientes, List<Bicicleta> bicicletas, 
                                                             List<Mecanico> mecanicos, List<Servicio> servicios)
        {
            var reparaciones = new List<Reparacion>();
            var estados = new[] { EstadoReparacion.Recibida, EstadoReparacion.Diagnostico, EstadoReparacion.EsperandoRepuestos, 
                                  EstadoReparacion.EnReparacion, EstadoReparacion.Lista, EstadoReparacion.Entregada };
            var prioridades = new[] { Prioridad.Baja, Prioridad.Media, Prioridad.Alta, Prioridad.Urgente };
            var diagnosticos = new[]
            {
                "Cambio de cadena y cassette desgastados",
                "Ajuste de cambios y frenos",
                "Reemplazo de llantas y cámaras",
                "Centrado de ruedas y ajuste general",
                "Revisión completa del sistema de frenos",
                "Cambio de cables y fundas",
                "Limpieza y lubricación completa",
                "Ajuste de suspensión y dirección",
                "Reemplazo de rodamientos de buje",
                "Revisión y ajuste de cambio electrónico"
            };

            for (int i = 1; i <= 15; i++)
            {
                var cliente = clientes[_random.Next(clientes.Count)];
                var bicicletasCliente = bicicletas.Where(b => b.ClienteId == cliente.Id).ToList();
                var bicicleta = bicicletasCliente.Count > 0 ? bicicletasCliente[_random.Next(bicicletasCliente.Count)] : bicicletas[_random.Next(bicicletas.Count)];
                var mecanico = mecanicos[_random.Next(mecanicos.Count)];
                var estado = estados[_random.Next(estados.Length)];
                var prioridad = prioridades[_random.Next(prioridades.Length)];
                var fechaIngreso = DateTime.Now.AddDays(-_random.Next(1, 30));
                var fechaEntrega = estado == EstadoReparacion.Entregada ? fechaIngreso.AddDays(_random.Next(1, 10)) : (DateTime?)null;

                var reparacion = new Reparacion
                {
                    Id = i,
                    Numero = $"REP-{DateTime.Now.Year}-{i:D3}",
                    ClienteId = cliente.Id,
                    BicicletaId = bicicleta.Id,
                    MecanicoId = mecanico.Id,
                    FechaIngreso = fechaIngreso,
                    FechaEntrega = fechaEntrega,
                    Estado = estado,
                    Diagnostico = diagnosticos[_random.Next(diagnosticos.Length)],
                    Observaciones = $"Observaciones de la reparación {i}",
                    Prioridad = prioridad,
                    CostoManoObra = (decimal)Math.Round(15 + _random.NextDouble() * 50, 2),
                    CostoRepuestos = (decimal)Math.Round(10 + _random.NextDouble() * 100, 2),
                    EsGarantia = _random.NextDouble() < 0.1,
                    FechaActualizacion = fechaIngreso.AddDays(_random.Next(1, 5))
                };

                reparaciones.Add(reparacion);
            }

            return reparaciones;
        }
    }
}