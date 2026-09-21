using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BikeServicePro.Services.Interfaces;
using BikeServicePro.ViewModels.Inventario;
using BikeServicePro.Helpers;

namespace BikeServicePro.Controllers
{
    /// <summary>
    /// Controlador de inventario - Acceso: SOLO Admin
    /// </summary>
    public class InventarioController : Controller
    {
        private readonly IInventarioService _inventarioService;

        public InventarioController(IInventarioService inventarioService)
        {
            _inventarioService = inventarioService;
        }

        public async Task<IActionResult> Index(string? categoria = null, bool soloStockBajo = false)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdmin(HttpContext))
                return RedirectToAction("Index", "Home");

            var items = soloStockBajo
                ? await _inventarioService.GetStockBajoAsync()
                : string.IsNullOrEmpty(categoria)
                    ? await _inventarioService.GetAllAsync()
                    : await _inventarioService.GetByCategoriaAsync(categoria);

            ViewData["Categoria"] = categoria;
            ViewData["SoloStockBajo"] = soloStockBajo;
            ViewBag.Categorias = await ObtenerCategorias();
            return View(items);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdmin(HttpContext))
                return RedirectToAction("Index", "Home");

            var item = await _inventarioService.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            return View(item);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdmin(HttpContext))
                return RedirectToAction("Index", "Home");

            return View(new InventarioCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InventarioCreateViewModel model)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdmin(HttpContext))
                return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
                return View(model);

            await _inventarioService.CreateInventarioAsync(model);
            TempData["SuccessMessage"] = "Ítem creado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdmin(HttpContext))
                return RedirectToAction("Index", "Home");

            var item = await _inventarioService.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            var model = new InventarioCreateViewModel
            {
                Id = item.Id,
                Nombre = item.Nombre,
                Categoria = item.Categoria,
                SubCategoria = item.SubCategoria,
                CodigoInterno = item.CodigoInterno,
                Cantidad = item.Cantidad,
                StockMinimo = item.StockMinimo,
                Proveedor = item.Proveedor,
                Costo = item.Costo,
                Precio = item.Precio,
                CodigoBarras = item.CodigoBarras,
                Ubicacion = item.Ubicacion,
                Activo = item.Activo
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InventarioCreateViewModel model)
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdmin(HttpContext))
                return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
                return View(model);

            var item = await _inventarioService.GetByIdAsync(model.Id);
            if (item == null)
                return NotFound();

            var itemActualizado = new BikeServicePro.Models.Inventario
            {
                Id = model.Id,
                Nombre = model.Nombre,
                Categoria = model.Categoria,
                SubCategoria = model.SubCategoria,
                CodigoInterno = model.CodigoInterno,
                Cantidad = model.Cantidad,
                StockMinimo = model.StockMinimo,
                Proveedor = model.Proveedor,
                Costo = model.Costo,
                Precio = model.Precio,
                CodigoBarras = model.CodigoBarras,
                Ubicacion = model.Ubicacion,
                Activo = model.Activo,
                FechaActualizacion = System.DateTime.Now
            };

            await _inventarioService.UpdateInventarioAsync(itemActualizado);
            TempData["SuccessMessage"] = "Ítem actualizado exitosamente.";
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
                TempData["ErrorMessage"] = "No tienes permiso para eliminar ítems.";
                return RedirectToAction(nameof(Index));
            }

            await _inventarioService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Ítem eliminado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        private async Task<Dictionary<string, int>> ObtenerCategorias()
        {
            var items = await _inventarioService.GetAllAsync();
            var categorias = new Dictionary<string, int>();
            
            foreach (var item in items)
            {
                if (!categorias.ContainsKey(item.Categoria))
                {
                    categorias[item.Categoria] = 0;
                }
                categorias[item.Categoria]++;
            }
            
            return categorias;
        }
    }
}