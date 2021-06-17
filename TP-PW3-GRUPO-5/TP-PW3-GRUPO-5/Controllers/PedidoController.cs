﻿using Clases_auxiliares;
using Contexto_de_datos.Models;
using Microsoft.AspNetCore.Mvc;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ViewModels;

namespace TP_PW3_GRUPO_5.Controllers
{
    public class PedidoController : Controller
    {
        private IPedidoServicio pedidoServicio;
        private IClienteServicio clienteServicio;
        private IArticuloServicio articuloServicio;
        private _20211CTPContext context;
        public PedidoController(_20211CTPContext ctx)
        {
            context = ctx;
            pedidoServicio = new PedidoServicio();
            clienteServicio = new ClienteServicio(context);
            articuloServicio = new ArticuloServicio(context);
        }
        public IActionResult Index()
        {
            List<Pedido> pedidos = pedidoServicio.ObtenerPedidos();
            List<PedidoCliente> pedidosClientes = PedidoCliente.ObtenerPedidosCliente(pedidos);    
            ViewData["selectFiltrado"] = ClientesEstadoPedido.ObtenerInfoSelects(pedidos);
            return View(pedidosClientes);
        }


        [HttpPost]
        public IActionResult ObtenerFiltros([FromBody] PedidoFiltro pedidoFiltro)
        {
            List<PedidoCliente> pedidosClientes = PedidoCliente.ObtenerPedidosCliente(pedidoServicio.ObtenerPedidos(pedidoFiltro));
            var resultado = JsonSerializer.Serialize(pedidosClientes);
            return Content(resultado);

        }

        public IActionResult NuevoPedido()
        {
            ClientesArticulos clientesArticulos = ClientesArticulos.ObtenerClientesArticulos
                (
                clienteServicio.ObtenerClientes(), articuloServicio.ObtenerArticulos()
                );

            return View(clientesArticulos);
        }
        [HttpPost]
        public IActionResult NuevoPedido([FromBody] PedidoNuevoFiltro pedidoNuevoFiltro)
        {
            AccionMensaje accionMensaje = new AccionMensaje();
            string resultado = "";
            if (pedidoNuevoFiltro.Accion == "guardar")
            {
                accionMensaje.Accion = pedidoNuevoFiltro.Accion;
                 resultado = JsonSerializer.Serialize(accionMensaje);
                return Content(resultado);
            }

            accionMensaje.Mensaje = "El pedido ha sido creado exitosamente.";
            resultado = JsonSerializer.Serialize(accionMensaje);
            return Content(resultado);
        }
    }
}