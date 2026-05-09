using AsphaltLegendUnitedWiki.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AsphaltLegendUnitedWiki.Controllers
{
    // [CAMBIO] Se agrega el atributo de protección a nivel de clase
    // Esto protege a Index, Privacy y cualquier otra acción futura.
    [IpRequired]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}