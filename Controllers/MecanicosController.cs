using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BikeServicePro.Services.Interfaces;
using BikeServicePro.ViewModels.Mecanicos;
using BikeServicePro.Helpers;

namespace BikeServicePro.Controllers
{
    /// <summary>
    /// Controlador de mecánicos - Acceso: SOLO Admin
    /// </summary>
    public class MecanicosController : Controller
    {
        private readonly IMecanicoService _mecanicoService;

        public MecanicosController(IMecanicoService mecanicoService)
        {
            _mecanicoService = mecanicoService;
        }

        public async Task<IActionResult> Index(bool soloActivos = false)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdmin(HttpContext))
                return RedirectToAction("Index", "Home");

            var mecanicos = soloActivos
                ? await _mecanicoService.GetActiveMecanicosAsync()
                : await _mecanicoService.GetAllAsync();

            ViewData["SoloActivos"] = soloActivos;
            return View(mecanicos);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdmin(HttpContext))
                return RedirectToAction("Index", "Home");

            var mecanico = await _mecanicoService.GetMecanicoDetailAsync(id);
            if (mecanico == null)
                return NotFound();

            return View(mecanico);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdmin(HttpContext))
                return RedirectToAction("Index", "Home");

            return View(new MecanicoCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MecanicoCreateViewModel model)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdmin(HttpContext))
                return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
                return View(model);

            await _mecanicoService.CreateMecanicoAsync(model);
            TempData["SuccessMessage"] = "Mecánico creado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdmin(HttpContext))
                return RedirectToAction("Index", "Home");

            var mecanico = await _mecanicoService.GetByIdAsync(id);
            if (mecanico == null)
                return NotFound();

            var model = new MecanicoCreateViewModel
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
                Activo = mecanico.Activo
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MecanicoCreateViewModel model)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdmin(HttpContext))
                return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
                return View(model);

            await _mecanicoService.UpdateMecanicoAsync(model);
            TempData["SuccessMessage"] = "Mecánico actualizado exitosamente.";
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
                TempData["ErrorMessage"] = "No tienes permiso para eliminar mecánicos.";
                return RedirectToAction(nameof(Index));
            }

            await _mecanicoService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Mecánico eliminado exitosamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}