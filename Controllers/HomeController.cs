using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using BikeServicePro.Services.Interfaces;
using BikeServicePro.Helpers;

namespace BikeServicePro.Controllers
{
    public class HomeController : Controller
    {
        private readonly IReporteService _reporteService;

        public HomeController(IReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        public async Task<IActionResult> Index()
        {
            if (!AuthorizationHelper.IsAuthenticated(HttpContext))
            {
                return RedirectToAction("Login", "Account");
            }

            var dashboardData = await _reporteService.GetDashboardDataAsync();
            return View(dashboardData);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}