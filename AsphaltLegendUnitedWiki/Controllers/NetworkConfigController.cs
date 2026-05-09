using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient; 
using System.Net;

namespace AsphaltLegendUnitedWiki.Controllers
{
    public class NetworkConfigController : Controller
    {
        [HttpGet]
        public IActionResult SetupIP() => View();

        [HttpPost]
        public IActionResult SetupIP(string serverIp)
        {
            // 1. Validaciones de formato
            if (string.IsNullOrEmpty(serverIp))
            {
                ViewBag.Error = "La dirección IP es obligatoria.";
                return View();
            }

            if (!IPAddress.TryParse(serverIp, out _))
            {
                ViewBag.Error = "El formato de la IP no es válido.";
                return View();
            }

            // 2. Validación de red local 
            // Cambia "192.168." por el que te salga en el ipconfig
            if (!serverIp.StartsWith("10.12.13.") && !serverIp.StartsWith("192.168.") && !serverIp.Equals("127.0.0.1"))
            {
                ViewBag.Error = "Debes ingresar una IP válida (10.12.13.xxx o 192.168.x.xxx)";
                return View();
            }

            // 3. Intento de conexión REAL a la base de datos de Docker
            try
            {
                // USA TUS CREDENCIALES REALES DE DOCKER AQUÍ
                string testConn = $"Server={serverIp};Database=asphalt_unite_wiki;User Id=colaboradores;Password=TuClaveSegura_2026;Connect Timeout=5;TrustServerCertificate=True;";

                using (var conn = new SqlConnection(testConn))
                {
                    conn.Open();
                }

                // Si llegamos aquí, la IP es correcta y hay conexión
                HttpContext.Session.SetString("DbServerIp", serverIp);
                return RedirectToAction("Index", "Acceso");
            }
            catch
            {
                ViewBag.Error = "IP detectada en la red, pero no se pudo conectar a la base de datos. ¿Está el Docker encendido?";
                return View();
            }
        }
    }
}