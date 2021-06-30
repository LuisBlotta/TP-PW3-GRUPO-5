using Clases_auxiliares;
using Microsoft.AspNetCore.Mvc;
using Servicios;
using System.Collections.Generic;
using System.Text.Json;
using Contexto_de_datos.Models;
using Servicios.SessionManager;
using System;
using Microsoft.AspNetCore.Http;

namespace TP_PW3_GRUPO_5.Controllers
{
    public class ClienteController : Controller
    {
        private IClienteServicio clienteServicio;
        private _20211CTPContext context;
        private ISessionManager sessionManager;

        public ClienteController(_20211CTPContext ctx)
        {
            context = ctx;
            clienteServicio = new ClienteServicio(context);
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
                    ViewData["selectClientes"] = clienteServicio.ObtenerSelectClientes();
                    return View(clienteServicio.ObtenerClientes());
                }
            }
            TempData["url"] = "/Cliente/Index";
            return Redirect("/Home/Ingresar");
        }

        public IActionResult NuevoCliente()
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
            TempData["url"] = "/Cliente/NuevoCliente";
            return Redirect("/Home/Ingresar");
        }

        [HttpPost]
        public IActionResult NuevoCliente(Cliente cliente, string submit)
        {
            if (ModelState.IsValid)
            {
                clienteServicio.Alta(cliente);

                if (submit == "Guardar")
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["mensaje"] = $"El cliente {cliente.Nombre} fue creado con éxito.";
                    return RedirectToAction(nameof(NuevoCliente));
                }
            }
            return View(cliente);
        }

        public IActionResult DetalleCliente(string accion, int id)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                UsuarioSesion usuarioSesion = JsonSerializer.Deserialize<UsuarioSesion>(HttpContext.Session.GetString("User"));
                SessionManager sesion = sessionManager.ValidarUsuario(usuarioSesion);

                if (sesion.EsAdmin)
                {
                    Cliente miCliente = clienteServicio.ObtenerPorId(id);
                    ViewData["accion"] = accion;
                    return View(miCliente);
                }
            }
            TempData["url"] = $"/Cliente/DetalleCliente/{accion}/{id}";
            return Redirect("/Home/Ingresar");
        }

        [HttpPost]
        public IActionResult EditarCliente(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                clienteServicio.Modificar(cliente);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return Redirect($"/cliente/detallecliente/editar/{cliente.IdCliente}");
            }
        }

        public IActionResult EliminarCliente(int id)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                UsuarioSesion usuarioSesion = JsonSerializer.Deserialize<UsuarioSesion>(HttpContext.Session.GetString("User"));
                SessionManager sesion = sessionManager.ValidarUsuario(usuarioSesion);

                if (sesion.EsAdmin)
                {
                    clienteServicio.Baja(id);
                    return RedirectToAction(nameof(Index));
                }
            }
            TempData["url"] = $"/Cliente/EliminarCliente/{id}";
            return Redirect("/Home/Ingresar");
        }

        [HttpPost]
        public IActionResult ObtenerFiltros([FromBody] ClienteFiltro clienteFiltro)
        {
            List<Cliente> clientes = clienteServicio.ObtenerClientes(clienteFiltro);
            var resultado = JsonSerializer.Serialize(clientes);
            return Content(resultado);
        }
    }
}
