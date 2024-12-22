using DAPIngenieria.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DAPIngenieria.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Acci�n para la vista principal
        public IActionResult Index()
        {
            // Verificar si el usuario est� autenticado
            string usuario = HttpContext.Session.GetString("Usuario");
            if (!string.IsNullOrEmpty(usuario))
            {
                ViewBag.Usuario = usuario;
            }
            else
            {
                // Redirigir al login si no est� autenticado
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        // Acci�n para la vista Registrar
        public IActionResult Registrar()
        {
            return View();
        }

        // Acci�n para la vista Generar
        public IActionResult Generar()
        {
            return View();
        }

        // Manejo de errores
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
