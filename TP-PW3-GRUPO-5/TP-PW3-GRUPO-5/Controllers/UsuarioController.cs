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
        IUsuarioServicio usuarioServicio;
        _20211CTPContext context;

        public UsuarioController(_20211CTPContext ctx)
        {
            context = ctx;
            usuarioServicio = new UsuarioServicio();
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
                usuario.FechaCreacion = DateTime.Now;
                context.Usuarios.Add(usuario);
                context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }
        public IActionResult DetalleUsuario(string accion, int id)
        {
            Usuario usuario = context.Usuarios.Find(id);
            ViewData["accion"] = accion;
            return View(usuario);
        }

        [HttpPost]
        public IActionResult EditarUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                Usuario usuarioBD = context.Usuarios.Find(usuario.IdUsuario);
                usuarioBD.EsAdmin = usuario.EsAdmin;
                usuarioBD.Email = usuario.Email;
                usuarioBD.Nombre = usuario.Nombre;
                usuarioBD.Apellido = usuario.Apellido;
                usuarioBD.FechaNacimiento = usuario.FechaNacimiento;
                usuarioBD.Password = usuario.Password;
                usuarioBD.FechaModificacion = DateTime.Now;
                //FALTA AGREGAR MODIFICADO POR
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return Redirect($"/usuario/detalleUsuario?accion=editar&id={usuario.IdUsuario}");
            }
        }
        public IActionResult EliminarUsuario(int id)
        {
            Usuario usuarioBD = context.Usuarios.Find(id);

            usuarioBD.FechaBorrado = DateTime.Now;
            //FALTA BORRADO POR Y CONFIRMACION DEL BORRADO
            context.SaveChanges();
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
