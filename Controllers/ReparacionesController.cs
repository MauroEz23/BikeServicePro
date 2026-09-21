using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BikeServicePro.Services.Interfaces;
using BikeServicePro.ViewModels.Reparaciones;
using BikeServicePro.Models;
using BikeServicePro.Helpers;
using System.Linq;

namespace BikeServicePro.Controllers
{
    public class ReparacionesController : Controller
    {
        private readonly IReparacionService _reparacionService;
        private readonly IClienteService _clienteService;
        private readonly IBicicletaService _bicicletaService;
        private readonly IMecanicoService _mecanicoService;

        public ReparacionesController(
            IReparacionService reparacionService,
            IClienteService clienteService,
            IBicicletaService bicicletaService,
            IMecanicoService mecanicoService)
        {
            _reparacionService = reparacionService;
            _clienteService = clienteService;
            _bicicletaService = bicicletaService;
            _mecanicoService = mecanicoService;
        }

        /// <summary>
        /// Lista de reparaciones - Acceso: Admin, Recepcionista, Mecánico
        /// </summary>
        public async Task<IActionResult> Index(string? estado = null)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            // Mecánico solo ve sus reparaciones asignadas
            if (AuthorizationHelper.IsMecanico(HttpContext))
            {
                // Obtener el nombre del mecánico desde la sesión
                var nombreCompleto = HttpContext.Session.GetString("NombreCompleto");
                
                // Buscar el mecánico por nombre
                var mecanicos = await _mecanicoService.GetAllAsync();
                var mecanico = mecanicos.FirstOrDefault(m => 
                    m.NombreCompleto == nombreCompleto || 
                    m.Email.Contains(nombreCompleto?.Split(' ')[0] ?? ""));
                
                if (mecanico != null)
                {
                    var reparacionesMecanico = await _reparacionService.GetByMecanicoIdAsync(mecanico.Id);
                    
                    if (!string.IsNullOrEmpty(estado))
                    {
                        reparacionesMecanico = reparacionesMecanico
                            .Where(r => r.Estado == (EstadoReparacion)int.Parse(estado))
                            .ToList();
                    }
                    
                    ViewData["Estado"] = estado;
                    ViewBag.Estados = System.Enum.GetValues<EstadoReparacion>();
                    return View(reparacionesMecanico);
                }
            }

            // Admin y Recepcionista ven todas
            var reparaciones = string.IsNullOrEmpty(estado)
                ? await _reparacionService.GetAllAsync()
                : await _reparacionService.GetByEstadoAsync((EstadoReparacion)int.Parse(estado));

            ViewData["Estado"] = estado;
            ViewBag.Estados = System.Enum.GetValues<EstadoReparacion>();
            return View(reparaciones);
        }

        /// <summary>
        /// Detalle de reparación - Acceso: Admin, Recepcionista, Mecánico
        /// </summary>
        public async Task<IActionResult> Details(int id)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            var reparacion = await _reparacionService.GetReparacionDetailAsync(id);
            if (reparacion == null)
                return NotFound();

            // Mecánico solo ve sus reparaciones asignadas
            if (AuthorizationHelper.IsMecanico(HttpContext))
            {
                var nombreCompleto = HttpContext.Session.GetString("NombreCompleto");
                var mecanicos = await _mecanicoService.GetAllAsync();
                var mecanico = mecanicos.FirstOrDefault(m => m.NombreCompleto == nombreCompleto);
                
                if (mecanico != null && reparacion.MecanicoId != mecanico.Id)
                {
                    TempData["ErrorMessage"] = "No tienes permiso para ver esta reparación.";
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(reparacion);
        }

        /// <summary>
        /// Crear reparación - Acceso: Admin, Recepcionista
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Create(int? clienteId = null)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdminOrRecepcionista(HttpContext))
                return RedirectToAction("Index", "Home");

            var model = new ReparacionCreateViewModel
            {
                ClienteId = clienteId ?? 0,
                FechaIngreso = System.DateTime.Now,
                Estado = EstadoReparacion.Recibida,
                Prioridad = Prioridad.Media
            };

            await CargarListasSelect(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReparacionCreateViewModel model)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdminOrRecepcionista(HttpContext))
                return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
            {
                await CargarListasSelect(model);
                return View(model);
            }

            await _reparacionService.CreateReparacionAsync(model);
            TempData["SuccessMessage"] = "Reparación creada exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Editar reparación - Acceso: Admin, Recepcionista
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdminOrRecepcionista(HttpContext))
                return RedirectToAction("Index", "Home");

            var reparacion = await _reparacionService.GetByIdAsync(id);
            if (reparacion == null)
                return NotFound();

            var model = new ReparacionCreateViewModel
            {
                Id = reparacion.Id,
                Numero = reparacion.Numero,
                ClienteId = reparacion.ClienteId,
                BicicletaId = reparacion.BicicletaId,
                MecanicoId = reparacion.MecanicoId,
                FechaIngreso = reparacion.FechaIngreso,
                Estado = reparacion.Estado,
                Diagnostico = reparacion.Diagnostico,
                Observaciones = reparacion.Observaciones,
                Prioridad = reparacion.Prioridad,
                CostoManoObra = reparacion.CostoManoObra,
                CostoRepuestos = reparacion.CostoRepuestos,
                EsGarantia = reparacion.EsGarantia,
                FechaActualizacion = reparacion.FechaActualizacion
            };

            await CargarListasSelect(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ReparacionCreateViewModel model)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdminOrRecepcionista(HttpContext))
                return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
            {
                await CargarListasSelect(model);
                return View(model);
            }

            var reparacion = await _reparacionService.GetByIdAsync(model.Id);
            if (reparacion == null)
                return NotFound();

            var reparacionActualizada = new Reparacion
            {
                Id = model.Id,
                Numero = reparacion.Numero,
                ClienteId = model.ClienteId,
                BicicletaId = model.BicicletaId,
                MecanicoId = model.MecanicoId,
                FechaIngreso = reparacion.FechaIngreso,
                Estado = model.Estado,
                Diagnostico = model.Diagnostico,
                Observaciones = model.Observaciones,
                Prioridad = model.Prioridad,
                CostoManoObra = model.CostoManoObra,
                CostoRepuestos = model.CostoRepuestos,
                EsGarantia = model.EsGarantia,
                FechaActualizacion = System.DateTime.Now
            };

            await _reparacionService.UpdateReparacionAsync(reparacionActualizada);
            TempData["SuccessMessage"] = "Reparación actualizada exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Actualizar estado - Acceso: Admin, Recepcionista, Mecánico (solo sus reparaciones)
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEstado(int id, EstadoReparacion estado)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            // Verificar si el mecánico tiene permiso para esta reparación
            if (AuthorizationHelper.IsMecanico(HttpContext))
            {
                var reparacion = await _reparacionService.GetByIdAsync(id);
                if (reparacion == null)
                    return NotFound();

                var nombreCompleto = HttpContext.Session.GetString("NombreCompleto");
                var mecanicos = await _mecanicoService.GetAllAsync();
                var mecanico = mecanicos.FirstOrDefault(m => m.NombreCompleto == nombreCompleto);

                if (mecanico == null || reparacion.MecanicoId != mecanico.Id)
                {
                    TempData["ErrorMessage"] = "No tienes permiso para cambiar el estado de esta reparación.";
                    return RedirectToAction(nameof(Index));
                }
            }

            await _reparacionService.UpdateEstadoAsync(id, estado);
            TempData["SuccessMessage"] = $"Estado actualizado a {estado}.";
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Asignar mecánico - Acceso: Admin, Recepcionista
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AsignarMecanico(int id, int mecanicoId)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdminOrRecepcionista(HttpContext))
                return RedirectToAction("Index", "Home");

            await _reparacionService.AsignarMecanicoAsync(id, mecanicoId);
            TempData["SuccessMessage"] = "Mecánico asignado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Eliminar reparación - Acceso: SOLO Admin
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdmin(HttpContext))
            {
                TempData["ErrorMessage"] = "No tienes permiso para eliminar reparaciones.";
                return RedirectToAction(nameof(Index));
            }

            await _reparacionService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Reparación eliminada exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        private async Task CargarListasSelect(ReparacionCreateViewModel model)
        {
            var clientes = await _clienteService.GetAllAsync();
            ViewBag.Clientes = clientes;

            var mecanicos = await _mecanicoService.GetAllAsync();
            ViewBag.Mecanicos = mecanicos;

            if (model.ClienteId > 0)
            {
                var bicicletas = await _bicicletaService.GetByClienteIdAsync(model.ClienteId);
                ViewBag.Bicicletas = bicicletas;
            }
        }
    }
}