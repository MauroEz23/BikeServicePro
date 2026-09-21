using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BikeServicePro.Services.Interfaces;
using BikeServicePro.ViewModels.Bicicletas;
using BikeServicePro.Helpers;
using System.Linq;

namespace BikeServicePro.Controllers
{
    public class BicicletasController : Controller
    {
        private readonly IBicicletaService _bicicletaService;
        private readonly IClienteService _clienteService;

        public BicicletasController(IBicicletaService bicicletaService, IClienteService clienteService)
        {
            _bicicletaService = bicicletaService;
            _clienteService = clienteService;
        }

        /// <summary>
        /// Lista de bicicletas - Acceso: Admin, Recepcionista
        /// </summary>
        public async Task<IActionResult> Index(string? searchTerm = null)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdminOrRecepcionista(HttpContext))
                return RedirectToAction("Index", "Home");

            var bicicletas = string.IsNullOrEmpty(searchTerm)
                ? await _bicicletaService.GetAllAsync()
                : await _bicicletaService.SearchAsync(searchTerm);

            ViewData["SearchTerm"] = searchTerm;
            return View(bicicletas);
        }

        /// <summary>
        /// Detalle de bicicleta - Acceso: Admin, Recepcionista
        /// </summary>
        public async Task<IActionResult> Details(int id)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdminOrRecepcionista(HttpContext))
                return RedirectToAction("Index", "Home");

            var bicicleta = await _bicicletaService.GetBicicletaDetailAsync(id);
            if (bicicleta == null)
                return NotFound();

            return View(bicicleta);
        }

        /// <summary>
        /// Crear bicicleta - Acceso: Admin, Recepcionista
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Create(int? clienteId = null)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdminOrRecepcionista(HttpContext))
                return RedirectToAction("Index", "Home");

            var model = new BicicletaCreateViewModel
            {
                ClienteId = clienteId ?? 0
            };

            ViewBag.Clientes = await _clienteService.GetAllAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BicicletaCreateViewModel model)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdminOrRecepcionista(HttpContext))
                return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
            {
                ViewBag.Clientes = await _clienteService.GetAllAsync();
                return View(model);
            }

            await _bicicletaService.CreateBicicletaAsync(model);
            TempData["SuccessMessage"] = "Bicicleta creada exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Editar bicicleta - Acceso: Admin, Recepcionista
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdminOrRecepcionista(HttpContext))
                return RedirectToAction("Index", "Home");

            var bicicleta = await _bicicletaService.GetByIdAsync(id);
            if (bicicleta == null)
                return NotFound();

            var model = new BicicletaCreateViewModel
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
                Activo = bicicleta.Activo
            };

            ViewBag.Clientes = await _clienteService.GetAllAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BicicletaCreateViewModel model)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdminOrRecepcionista(HttpContext))
                return RedirectToAction("Index", "Home");

            if (id != model.Id)
                return BadRequest("El ID no coincide.");

            if (!ModelState.IsValid)
            {
                ViewBag.Clientes = await _clienteService.GetAllAsync();
                return View(model);
            }

            await _bicicletaService.UpdateBicicletaAsync(model);
            TempData["SuccessMessage"] = "Bicicleta actualizada exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Eliminar bicicleta - Acceso: SOLO Admin
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdmin(HttpContext))
            {
                TempData["ErrorMessage"] = "No tienes permiso para eliminar bicicletas.";
                return RedirectToAction(nameof(Index));
            }

            await _bicicletaService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Bicicleta eliminada exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetByCliente(int clienteId)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return Json(new { error = "No autenticado" });

            var bicicletas = await _bicicletaService.GetByClienteIdAsync(clienteId);
            var result = bicicletas.Select(b => new
            {
                id = b.Id,
                nombreCompleto = b.NombreCompleto
            });
            
            return Json(result);
        }
    }
}