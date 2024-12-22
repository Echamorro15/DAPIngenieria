using Microsoft.AspNetCore.Mvc;

namespace DAPIngenieria.Controllers
{
    public class GerencialController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
