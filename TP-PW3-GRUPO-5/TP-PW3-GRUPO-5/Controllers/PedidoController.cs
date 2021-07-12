using Clases_auxiliares;
using Contexto_de_datos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios;
using System;
using System.Collections.Generic;
using System.Text.Json;
using ViewModels;

namespace TP_PW3_GRUPO_5.Controllers
{
    public class PedidoController : Controller
    {
        private IPedidoServicio pedidoServicio;
        private IClienteServicio clienteServicio;
        private IArticuloServicio articuloServicio;
        private _20211CTPContext context;

        public PedidoController(_20211CTPContext ctx,IHttpContextAccessor _httpContextAccessor)
        {
            context = ctx;
            pedidoServicio = new PedidoServicio(context, _httpContextAccessor);
            clienteServicio = new ClienteServicio(context, _httpContextAccessor);
            articuloServicio = new ArticuloServicio(context, _httpContextAccessor);
        }

        public IActionResult Index()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                List<Pedido> pedidos = pedidoServicio.ObtenerPedidos();
                List<PedidoCliente> pedidosClientes = PedidoCliente.ObtenerPedidosCliente(pedidos);
                ViewData["selectFiltrado"] = ClientesEstadoPedido.ObtenerInfoSelects(pedidos);
                return View(pedidosClientes);
            }
            TempData["url"] = "/Pedido/Index";
            return Redirect("/Home/Ingresar");
        }

        [HttpPost]
        public IActionResult ObtenerFiltros([FromBody] PedidoFiltro pedidoFiltro)
        {
            List<PedidoCliente> pedidosClientes = PedidoCliente.ObtenerPedidosCliente(pedidoServicio.ObtenerPedidos(pedidoFiltro));
            var resultado = JsonSerializer.Serialize(pedidosClientes);
            return Content(resultado);
        }

        [HttpPost]
        public IActionResult ObtenerDetallePedido([FromBody] int IdPedido)
        {
            List<ArticuloCantidad> articulosCantidad = pedidoServicio.ObtenerPedidoDetalle(IdPedido);
            var resultado = JsonSerializer.Serialize(articulosCantidad);
            return Content(resultado);
        }

        public IActionResult NuevoPedido()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                ClientesArticulos clientesArticulos = ClientesArticulos.ObtenerClientesArticulos
                  (
                  clienteServicio.ObtenerClientesConPedidos(), articuloServicio.ObtenerArticulos()
                  );
                return View(clientesArticulos);
            }
            TempData["url"] = "/Pedido/NuevoPedido";
            return Redirect("/Home/Ingresar");
        }

        [HttpPost]
        public IActionResult NuevoPedido([FromBody] PedidoNuevoFiltro pedidoNuevoFiltro)
        {
            AccionMensaje accionMensaje = new AccionMensaje();
            string resultado = "";
            pedidoServicio.Alta(pedidoNuevoFiltro);
            if (pedidoNuevoFiltro.Accion == "guardar")
            {
                accionMensaje.Accion = pedidoNuevoFiltro.Accion;
                resultado = JsonSerializer.Serialize(accionMensaje);
                return Content(resultado);
            }
            string nombreCl = clienteServicio.ObtenerPorId(pedidoNuevoFiltro.IdCliente).Nombre;
            int nroPedido = pedidoServicio.ObtenerNumeroPedido() - 1;
            TempData["Mensaje"] = $"Pedido de {nombreCl} nro. {nroPedido} creado con exito.";
            accionMensaje.Accion = "guardarYCrear";
            resultado = JsonSerializer.Serialize(accionMensaje);
            return Content(resultado);
        }

        public IActionResult DetallePedido(string accion, int id)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                Pedido miPedido = pedidoServicio.ObtenerPorId(id);
                ViewData["accion"] = accion;
                ViewData["articulos"] = articuloServicio.ObtenerArticulos();
                return View(miPedido);
            }
            TempData["url"] = $"/Pedido/DetallePedido/{accion}/{id}";
            return Redirect("/Home/Ingresar");
        }

        [HttpPost]
        public IActionResult EditarPedido([FromBody] EditarPedido pedidoAEditar)
        {
            pedidoServicio.Modificar(pedidoAEditar);
            string resultado = JsonSerializer.Serialize("/Pedido/Index");
            return Content(resultado);
        }

        public IActionResult EliminarPedido(int id)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                pedidoServicio.Baja(id);
                return RedirectToAction(nameof(Index));
            }
            TempData["url"] = $"/Pedido/EliminarPedido/{id}";
            return Redirect("/Home/Ingresar");
        }
    }
}