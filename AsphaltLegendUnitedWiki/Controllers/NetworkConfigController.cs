using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Net;
using AsphaltLegendUnitedWiki.Models; // Asegúrate de importar el namespace

namespace AsphaltLegendUnitedWiki.Controllers
{
    public class NetworkConfigController : Controller
    {
        private readonly DbSettings _globalSettings;

        // Inyectamos la variable global mediante el constructor
        public NetworkConfigController(DbSettings globalSettings)
        {
            _globalSettings = globalSettings;
        }

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

            // 2. Validación de red local (Filtro de seguridad)
            if (!serverIp.StartsWith("10.12.13.") && !serverIp.StartsWith("192.168.") && !serverIp.Equals("127.0.0.1"))
            {
                ViewBag.Error = "Debes ingresar una IP válida de tu red local.";
                return View();
            }

            // 3. Intento de conexión REAL
            try
            {
                // Construimos la cadena de prueba con la IP ingresada
                string testConn = $"Server={serverIp};Database=asphalt_unite_wiki;User Id=colaboradores;Password=TuClaveSegura_2026;Connect Timeout=5;TrustServerCertificate=True;";

                using (var conn = new SqlConnection(testConn))
                {
                    conn.Open(); // Si falla, salta directamente al catch
                }

                // --- MODIFICACIÓN CLAVE ---
                // Si la conexión fue exitosa, actualizamos la variable GLOBAL
                _globalSettings.ServerIp = serverIp;

                // También la guardamos en sesión por si necesitas usarla en las vistas
                HttpContext.Session.SetString("DbServerIp", serverIp);

                return RedirectToAction("Index", "Acceso");
            }
            catch (Exception ex)
            {
                // Usamos 'ex.Message' para dar un detalle más técnico en el error
                ViewBag.Error = $"Error de conexión: {ex.Message}";
                return View();
            }
        }
    }
}