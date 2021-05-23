using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entidades;
using System.Web.Helpers;
using System.Text.Json;
using Clases_auxiliares;
using Servicios;

namespace TP_PW3_GRUPO_5.Controllers
{
    public class UsuarioController : Controller
    {
        IUsuarioServicio usuarioServicio;

        public UsuarioController()
        {
            usuarioServicio = new UsuarioServicio();
        }
        public IActionResult Index()
        {

            return View(usuarioServicio.ObtenerUsuarios());
        }

        public IActionResult NuevoUsuario()
        {

            return View();
        }

        [HttpPost]
        public IActionResult NuevoUsuario(Usuario usuario)
        {

            return View();
        }
        public IActionResult DetalleUsuario(string accion)
        {
            Usuario miUsuario1 = new Usuario();
            miUsuario1.IdUsuario = 205;
            miUsuario1.Nombre = "Xuan";
            miUsuario1.Email = "Xuan_perez@gmail.com";
            miUsuario1.Apellido = "Perez";
            miUsuario1.Password = "pepito123";
            miUsuario1.EsAdmin = false;
            miUsuario1.FechaNacimiento = DateTime.Now.AddYears(-20);

            ViewData["accion"] = accion;
            return View(miUsuario1);
        }

        [HttpPost]
        public IActionResult EditarUsuario(Usuario usuario)
        {

            return View();
        }

        [HttpPost]
        public IActionResult ObtenerFiltros([FromBody] UsuarioFiltro usuarioFiltro)
        {

            var resultado = JsonSerializer.Serialize(usuarioServicio.ObtenerUsuarios(usuarioFiltro));

            return Content(resultado);

        }

    }
}
