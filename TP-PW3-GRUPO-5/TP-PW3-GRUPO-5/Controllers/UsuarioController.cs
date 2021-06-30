﻿using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Clases_auxiliares;
using Servicios;
using Contexto_de_datos.Models;
using System.Linq;
using Servicios.SessionManager;
using System;
using Microsoft.AspNetCore.Http;

namespace TP_PW3_GRUPO_5.Controllers
{
    public class UsuarioController : Controller
    {
        private IUsuarioServicio usuarioServicio;
        private _20211CTPContext context;
        private ISessionManager sessionManager;

        public UsuarioController(_20211CTPContext ctx)
        {
            context = ctx;
            usuarioServicio = new UsuarioServicio(context);
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
                    return View(context.Usuarios.ToList());
                }
            }
            TempData["url"] = "/Usuario/Index";
            return Redirect("/Home/Ingresar");
        }

        public IActionResult NuevoUsuario()
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
            TempData["url"] = "/Usuario/NuevoUsuario";
            return Redirect("/Home/Ingresar");
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
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                UsuarioSesion usuarioSesion = JsonSerializer.Deserialize<UsuarioSesion>(HttpContext.Session.GetString("User"));
                SessionManager sesion = sessionManager.ValidarUsuario(usuarioSesion);

                if (sesion.EsAdmin)
                {
                    Usuario usuario = usuarioServicio.ObtenerPorId(id);
                    ViewData["accion"] = accion;
                    return View(usuario);
                }
            }
            TempData["url"] = $"/Usuario/DetalleUsuario/{accion}/{id}";
            return Redirect("/Home/Ingresar");
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
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                UsuarioSesion usuarioSesion = JsonSerializer.Deserialize<UsuarioSesion>(HttpContext.Session.GetString("User"));
                SessionManager sesion = sessionManager.ValidarUsuario(usuarioSesion);

                if (sesion.EsAdmin)
                {
                    usuarioServicio.Baja(id);
                    return RedirectToAction(nameof(Index));
                }
            }
            TempData["url"] = $"/Usuario/EliminarUsuario/{id}";
            return Redirect("/Home/Ingresar");
        }

        [HttpPost]
        public IActionResult ObtenerFiltros([FromBody] UsuarioFiltro usuarioFiltro)
        {
            var resultado = JsonSerializer.Serialize(usuarioServicio.ObtenerUsuarios(usuarioFiltro));
            return Content(resultado);
        }
    }
}
