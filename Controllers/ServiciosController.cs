using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BikeServicePro.Services.Interfaces;
using BikeServicePro.ViewModels.Servicios;
using BikeServicePro.Helpers;

namespace BikeServicePro.Controllers
{
    /// <summary>
    /// Controlador de servicios - Acceso: SOLO Admin
    /// </summary>
    public class ServiciosController : Controller
    {
        private readonly IServicioService _servicioService;

        public ServiciosController(IServicioService servicioService)
        {
            _servicioService = servicioService;
        }

        public async Task<IActionResult> Index(string? categoria = null)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdmin(HttpContext))
                return RedirectToAction("Index", "Home");

            var servicios = string.IsNullOrEmpty(categoria)
                ? await _servicioService.GetAllAsync()
                : await _servicioService.GetByCategoriaAsync(categoria);

            ViewData["Categoria"] = categoria;
            ViewBag.Categorias = await ObtenerCategorias();
            return View(servicios);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdmin(HttpContext))
                return RedirectToAction("Index", "Home");

            return View(new ServicioCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServicioCreateViewModel model)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdmin(HttpContext))
                return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
                return View(model);

            await _servicioService.CreateServicioAsync(model);
            TempData["SuccessMessage"] = "Servicio creado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdmin(HttpContext))
                return RedirectToAction("Index", "Home");

            var servicio = await _servicioService.GetByIdAsync(id);
            if (servicio == null)
                return NotFound();

            var model = new ServicioCreateViewModel
            {
                Id = servicio.Id,
                Nombre = servicio.Nombre,
                Descripcion = servicio.Descripcion,
                PrecioBase = servicio.PrecioBase,
                TiempoEstimado = servicio.TiempoEstimado,
                Categoria = servicio.Categoria,
                Icono = servicio.Icono,
                RequiereRepuesto = servicio.RequiereRepuesto,
                Activo = servicio.Activo
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ServicioCreateViewModel model)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdmin(HttpContext))
                return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
                return View(model);

            await _servicioService.UpdateServicioAsync(model);
            TempData["SuccessMessage"] = "Servicio actualizado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdmin(HttpContext))
            {
                TempData["ErrorMessage"] = "No tienes permiso para eliminar servicios.";
                return RedirectToAction(nameof(Index));
            }

            await _servicioService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Servicio eliminado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        private async Task<Dictionary<string, int>> ObtenerCategorias()
        {
            var servicios = await _servicioService.GetAllAsync();
            var categorias = new Dictionary<string, int>();
            
            foreach (var servicio in servicios)
            {
                if (!categorias.ContainsKey(servicio.CategoriaNombre))
                {
                    categorias[servicio.CategoriaNombre] = 0;
                }
                categorias[servicio.CategoriaNombre]++;
            }
            
            return categorias;
        }
    }
}