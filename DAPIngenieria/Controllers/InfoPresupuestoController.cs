using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using DAPIngenieria.Models;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using DAPIngenieria.Data;
namespace DAPIngenieria.Controllers
{
    public class InformeController : Controller
    {
        private readonly AppDbContext db;

        public InformeController(AppDbContext context)
        {
            db = context;
        }

        // Acción para mostrar el informe
        public ActionResult GenerarInforme(int idCliente)
        {
            // Obtener los datos del presupuesto basados en el IdCliente
            var presupuestos = db.Presupuestos
                .Where(p => p.IdCliente == idCliente)
                .Select(p => new InfoPresupuesto
                {
                    IdPresupuesto = p.IdPresupuesto,
                    RazonSocial = p.Cliente.RazonSocial,
                    //DesServicio = p.TipoServicio.DesServicio,
                    Estado = p.Estado
                }).ToList();

            // Pasar los datos a la vista
            return View(presupuestos);
        }

        //// Acción para exportar el informe a PDF
        //public ActionResult ExportarInformePDF(int idCliente)
        //{
        //    // Obtener los datos del presupuesto basados en el IdCliente
        //    var presupuestos = db.Presupuestos
        //        .Where(p => p.IdCliente == idCliente)
        //        .Select(p => new InfoPresupuesto
        //        {
        //            IdPresupuesto = p.IdPresupuesto,
        //            RazonSocial = p.Cliente.RazonSocial,
        //            //DesServicio = p.TipoServicio.DesServicio,
        //            Estado = p.Estado
        //        }).ToList();

        //    // Renderizar la vista a HTML
        //    var htmlContent = RenderRazorViewToString("GenerarInforme", presupuestos);

        //    // Usar una librería como WkHtmlToPdf para convertir HTML a PDF
        //    var pdf = new HtmlToPdf().ConvertHtmlString(htmlContent);

        //    // Devolver el archivo PDF
        //    return File(pdf, "application/pdf", "Informe.pdf");
        //}

        //// Método para renderizar la vista Razor a HTML
        //private string RenderRazorViewToString(string viewName, object model)
        //{
        //    ViewData.Model = model;
        //    using (var sw = new StringWriter())
        //    {
        //        var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
        //        var context = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
        //        viewResult.View.Render(context, sw);
        //        return sw.GetStringBuilder().ToString();
        //    }
        //}
    }
}

