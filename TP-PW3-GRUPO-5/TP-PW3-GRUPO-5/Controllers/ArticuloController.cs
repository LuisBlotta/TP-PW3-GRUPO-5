using Microsoft.AspNetCore.Mvc;
using System;
using Servicios;
using Clases_auxiliares;
using System.Text.Json;
using Contexto_de_datos.Models;
using Microsoft.AspNetCore.Http;
using Servicios.SessionManager;

namespace TP_PW3_GRUPO_5.Controllers
{
    public class ArticuloController : Controller
    {
        private IArticuloServicio articuloServicio;
        private _20211CTPContext context;
        private ISessionManager sessionManager;

        public ArticuloController(_20211CTPContext ctx)
        {
            context = ctx;
            articuloServicio = new ArticuloServicio(context);
            sessionManager = new SessionManager();
        }

        public IActionResult Index()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                UsuarioSesion usuarioSesion = JsonSerializer.Deserialize<UsuarioSesion>(HttpContext.Session.GetString("User"));
                SessionManager sesion = sessionManager.ValidarUsuario(usuarioSesion);

                if (sesion.EsAdmin)
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
                UsuarioSesion usuarioSesion = JsonSerializer.Deserialize<UsuarioSesion>(HttpContext.Session.GetString("User"));
                SessionManager sesion = sessionManager.ValidarUsuario(usuarioSesion);

                if (sesion.EsAdmin)
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
                UsuarioSesion usuarioSesion = JsonSerializer.Deserialize<UsuarioSesion>(HttpContext.Session.GetString("User"));
                SessionManager sesion = sessionManager.ValidarUsuario(usuarioSesion);

                if (sesion.EsAdmin)
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
                UsuarioSesion usuarioSesion = JsonSerializer.Deserialize<UsuarioSesion>(HttpContext.Session.GetString("User"));
                SessionManager sesion = sessionManager.ValidarUsuario(usuarioSesion);

                if (sesion.EsAdmin)
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
    }
}
