using AsphaltLegendUnitedWiki.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using AsphaltLegendUnitedWiki.Data;
// Asegúrate de incluir el namespace donde definiste tu filtro
// using AsphaltLegendUnitedWiki.Filters; 

namespace AsphaltLegendUnitedWiki.Controllers
{
    public class AccesoController : Controller
    {
        private readonly AsphaltDbContext _context;

        public AccesoController(AsphaltDbContext context)
        {
            _context = context;
        }

        // [AÑADIDO] Esta protección redirigirá a SetupIP si no hay una IP en sesión
        [IpRequired]
        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult Registrar(Usuario user)
        {
            if (_context.Usuarios.Any(u => u.email == user.email))
            {
                ViewBag.Error = "El correo ya existe";
                return View("Index");
            }

            user.password_hash = BCrypt.Net.BCrypt.HashPassword(user.password_hash);

            _context.Usuarios.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Login(string Email, string PasswordHash)
        {
            // Esta consulta usará la IP configurada dinámicamente en Program.cs
            var user = _context.Usuarios.FirstOrDefault(u => u.email == Email);

            if (user != null && BCrypt.Net.BCrypt.Verify(PasswordHash, user.password_hash))
            {
                HttpContext.Session.SetString("usuario", user.nombre_usuario!);
                return RedirectToAction("Bienvenida", "Home");
            }

            ViewBag.Error = "Credenciales incorrectas";
            return View("Index");
        }

        [HttpGet]
        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrarse(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(usuario);
        }
    }
}