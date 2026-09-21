using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BikeServicePro.Services.Interfaces;
using BikeServicePro.ViewModels.Clientes;
using BikeServicePro.Helpers;

namespace BikeServicePro.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        /// <summary>
        /// Lista de clientes - Acceso: Admin, Recepcionista
        /// </summary>
        public async Task<IActionResult> Index(string? searchTerm = null)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdminOrRecepcionista(HttpContext))
                return RedirectToAction("Index", "Home");

            var clientes = string.IsNullOrEmpty(searchTerm)
                ? await _clienteService.GetAllAsync()
                : await _clienteService.SearchAsync(searchTerm);

            ViewData["SearchTerm"] = searchTerm;
            return View(clientes);
        }

        /// <summary>
        /// Detalle de cliente - Acceso: Admin, Recepcionista
        /// </summary>
        public async Task<IActionResult> Details(int id)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdminOrRecepcionista(HttpContext))
                return RedirectToAction("Index", "Home");

            var cliente = await _clienteService.GetClienteDetailAsync(id);
            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        /// <summary>
        /// Crear cliente - Acceso: Admin, Recepcionista
        /// </summary>
        [HttpGet]
        public IActionResult Create()
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdminOrRecepcionista(HttpContext))
                return RedirectToAction("Index", "Home");

            return View(new ClienteCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClienteCreateViewModel model)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdminOrRecepcionista(HttpContext))
                return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
                return View(model);

            await _clienteService.CreateClienteAsync(model);
            TempData["SuccessMessage"] = "Cliente creado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Editar cliente - Acceso: Admin, Recepcionista
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdminOrRecepcionista(HttpContext))
                return RedirectToAction("Index", "Home");

            var cliente = await _clienteService.GetByIdAsync(id);
            if (cliente == null)
                return NotFound();

            var model = new ClienteCreateViewModel
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellidos = cliente.Apellidos,
                Email = cliente.Email,
                Telefono = cliente.Telefono,
                Direccion = cliente.Direccion,
                Observaciones = cliente.Observaciones,
                Activo = cliente.Activo
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClienteCreateViewModel model)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdminOrRecepcionista(HttpContext))
                return RedirectToAction("Index", "Home");

            if (id != model.Id)
                return BadRequest("El ID no coincide.");

            if (!ModelState.IsValid)
                return View(model);

            await _clienteService.UpdateClienteAsync(model);
            TempData["SuccessMessage"] = "Cliente actualizado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Eliminar cliente - Acceso: SOLO Admin
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdmin(HttpContext))
            {
                TempData["ErrorMessage"] = "No tienes permiso para eliminar clientes.";
                return RedirectToAction(nameof(Index));
            }

            await _clienteService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Cliente eliminado exitosamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}