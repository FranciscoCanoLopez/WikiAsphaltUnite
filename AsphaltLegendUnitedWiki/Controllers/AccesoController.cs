using AsphaltLegendUnitedWiki.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using AsphaltLegendUnitedWiki.Data;

namespace AsphaltLegendUnitedWiki.Controllers
{
    public class AccesoController : Controller
    {
        private readonly AsphaltDbContext _context; // Tu conexión a SQL Server o MySQL

        public AccesoController(AsphaltDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult Registrar(Usuario user)
        {
            // Reemplaza la validación de mysqli_num_rows
            if (_context.Usuarios.Any(u => u.email == user.email))
            {
                ViewBag.Error = "El correo ya existe";
                return View("Index");
            }

            // Equivalente a password_hash en PHP
            user.password_hash = BCrypt.Net.BCrypt.HashPassword(user.password_hash);

            _context.Usuarios.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Login(string Email, string PasswordHash)
        {
            var user = _context.Usuarios.FirstOrDefault(u => u.email == Email);

            // Equivalente a password_verify
            if (user != null && BCrypt.Net.BCrypt.Verify(PasswordHash, user.password_hash))
            {
                // Reemplaza a $_SESSION['usuario']
                HttpContext.Session.SetString("usuario", user.nombre_usuario!);
                return RedirectToAction("Bienvenida", "Home");
            }

            ViewBag.Error = "Credenciales incorrectas";
            return View("Index");
        }
        // Acción para mostrar la página de Registro
        [HttpGet]
        public IActionResult Registrarse()
        {
            return View();
        }

        // Acción para recibir los datos del formulario de Registro
        [HttpPost]
        public IActionResult Registrarse(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                // Aquí iría tu lógica para guardar en la base de datos
                // _context.Usuarios.Add(usuario);
                // _context.SaveChanges();
                return RedirectToAction("Index"); // Regresa al Login tras registrarse
            }
            return View(usuario);
        }
    }
}
