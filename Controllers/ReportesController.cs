using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BikeServicePro.Services.Interfaces;
using BikeServicePro.Helpers;

namespace BikeServicePro.Controllers
{
    /// <summary>
    /// Controlador de reportes - Acceso: SOLO Admin
    /// </summary>
    public class ReportesController : Controller
    {
        private readonly IReporteService _reporteService;

        public ReportesController(IReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        public async Task<IActionResult> Dashboard()
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdmin(HttpContext))
                return RedirectToAction("Index", "Home");

            var dashboard = await _reporteService.GetDashboardDataAsync();
            return View(dashboard);
        }

        public async Task<IActionResult> Reparaciones()
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdmin(HttpContext))
                return RedirectToAction("Index", "Home");

            var reparacionesPorMes = await _reporteService.GetReparacionesPorMesAsync(12);
            var ingresosPorMes = await _reporteService.GetIngresosPorMesAsync(12);
            var topServicios = await _reporteService.GetTopServiciosAsync(10);

            ViewBag.ReparacionesPorMes = reparacionesPorMes;
            ViewBag.IngresosPorMes = ingresosPorMes;
            ViewBag.TopServicios = topServicios;
            
            return View();
        }

        public async Task<IActionResult> Clientes()
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdmin(HttpContext))
                return RedirectToAction("Index", "Home");

            var estadisticas = await _reporteService.GetEstadisticasClientesAsync();
            return View(estadisticas);
        }

        public async Task<IActionResult> Inventario()
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdmin(HttpContext))
                return RedirectToAction("Index", "Home");

            var estadisticas = await _reporteService.GetEstadisticasInventarioAsync();
            return View(estadisticas);
        }

        public async Task<IActionResult> Mecanicos()
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdmin(HttpContext))
                return RedirectToAction("Index", "Home");

            var dashboard = await _reporteService.GetDashboardDataAsync();
            return View(dashboard.ReparacionesPorMecanico);
        }

        public async Task<IActionResult> ActividadesRecientes()
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Login", "Account");

            if (!AuthorizationHelper.IsAdmin(HttpContext))
                return RedirectToAction("Index", "Home");

            var actividades = await _reporteService.GetActividadesRecientesAsync(10);
            return View(actividades);
        }
    }
}