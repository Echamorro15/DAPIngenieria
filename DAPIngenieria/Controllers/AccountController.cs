using DAPIngenieria.Data;
using Microsoft.AspNetCore.Mvc;

namespace DAPIngenieria.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Login
        public ActionResult Login()
        {
            // Verificar si el usuario ya está autenticado
            if (HttpContext.Session.GetString("Usuario") != null)
            {
                return RedirectToAction("Index", "Home");  // Si ya está logueado, redirige al home
            }

            ViewData["IsLoginPage"] = true;  // Oculta el menú
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string NombreUsuario, string Contraseña)
        {
            //Verificar si el usuario existe en la base de datos
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.NombreUsuario == NombreUsuario && u.Contraseña == Contraseña);

            if (usuario != null)
            {
                // Guardar usuario en sesión
                HttpContext.Session.SetString("Usuario", usuario.NombreUsuario);
                return RedirectToAction("Index", "Home");
            }

            //ViewBag.Error = "Credenciales incorrectas.";
            return View();
        }

        // GET: Logout
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();  // Limpiar sesión
            return RedirectToAction("Login");
        }
    }
}

