using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using Clases_auxiliares;
using Servicios;
using Contexto_de_datos.Models;
using System.Linq;

namespace TP_PW3_GRUPO_5.Controllers
{
    public class UsuarioController : Controller
    {
        private IUsuarioServicio usuarioServicio;
        private _20211CTPContext context;

        public UsuarioController(_20211CTPContext ctx)
        {
            context = ctx;
            usuarioServicio = new UsuarioServicio(context);
        }
        public IActionResult Index()
        {

            return View(context.Usuarios.ToList());
        }

        public IActionResult NuevoUsuario()
        {

            return View();
        }

        [HttpPost]
        public IActionResult NuevoUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuarioServicio.Alta(usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }
        public IActionResult DetalleUsuario(string accion, int id)
        {
            Usuario usuario = usuarioServicio.ObtenerPorId(id);
            ViewData["accion"] = accion;
            return View(usuario);
        }

        [HttpPost]
        public IActionResult EditarUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuarioServicio.Modificar(usuario);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return Redirect($"/usuario/detalleUsuario?accion=editar&id={usuario.IdUsuario}");
            }
        }
        public IActionResult EliminarUsuario(int id)
        {
            usuarioServicio.Baja(id);
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public IActionResult ObtenerFiltros([FromBody] UsuarioFiltro usuarioFiltro)
        {

            var resultado = JsonSerializer.Serialize(usuarioServicio.ObtenerUsuarios(usuarioFiltro));

            return Content(resultado);

        }

    }
}
