using Microsoft.AspNetCore.Mvc;
using System;
using Servicios;
using Clases_auxiliares;
using System.Text.Json;
using Contexto_de_datos.Models;
using Microsoft.AspNetCore.Http;
using Servicios.Session;

namespace TP_PW3_GRUPO_5.Controllers
{
    public class ArticuloController : Controller
    {
        private IArticuloServicio articuloServicio;
        private _20211CTPContext context;
        private ISessionManager sessionManager;

        public ArticuloController(_20211CTPContext ctx,IHttpContextAccessor _httpContextAccessor)
        {
            context = ctx;
            articuloServicio = new ArticuloServicio(context, _httpContextAccessor);
            sessionManager = new SessionManager(_httpContextAccessor);
        }

        public IActionResult Index()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                if (sessionManager.EsAdmin())
                {
                    return View(articuloServicio.ObtenerArticulos());
                }
            }
            TempData["url"] = "/Articulo/Index";
            return Redirect("/Home/Ingresar");
        }

        public IActionResult NuevoArticulo()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                if (sessionManager.EsAdmin())
                {
                    return View();
                }
            }
            TempData["url"] = "/Articulo/NuevoArticulo";
            return Redirect("/Home/Ingresar");
        }

        [HttpPost]
        public IActionResult NuevoArticulo(Articulo articulo, string submit)
        {
            if (ModelState.IsValid)
            {
                articuloServicio.Alta(articulo);

                if (submit == "Guardar")
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["mensaje"] = $"El artículo {articulo.Codigo} - {articulo.Descripcion} se agregó correctamente.";
                    return RedirectToAction(nameof(NuevoArticulo));
                }
            }
            return View(articulo);
        }

        public IActionResult DetalleArticulo(string accion, int id)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                if (sessionManager.EsAdmin())
                {
                    Articulo miArticulo = articuloServicio.ObtenerPorId(id);
                    ViewData["accion"] = accion;
                    return View(miArticulo);
                }
            }
            TempData["url"] = $"/Articulo/DetalleArticulo/{accion}/{id}";
            return Redirect("/Home/Ingresar");
        }

        [HttpPost]
        public IActionResult EditarArticulo(Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                articuloServicio.Modificar(articulo);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return Redirect($"/articulo/detallearticulo/editar/{articulo.IdArticulo}");
            }
        }

        public IActionResult EliminarArticulo(int id)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                if (sessionManager.EsAdmin())
                {
                    articuloServicio.Baja(id);
                    return RedirectToAction(nameof(Index));
                }
            }
            TempData["url"] = $"/Articulo/EliminarArticulo/{id}";
            return Redirect("/Home/Ingresar");
        }

        [HttpPost]
        public IActionResult ObtenerFiltros([FromBody] ArticuloFiltro articuloFiltro)
        {
            var resultado = JsonSerializer.Serialize(articuloServicio.ObtenerArticulos(articuloFiltro));
            return Content(resultado);
        }

       [HttpPost]
        public IActionResult ConsultarEstadoPedidos([FromBody] int Id)
        {
            bool tienePedidos = articuloServicio.ConsultarEstadoPedidos(Id);
            var resultado = JsonSerializer.Serialize(tienePedidos);
            return Content(resultado);
        }
    }
}
