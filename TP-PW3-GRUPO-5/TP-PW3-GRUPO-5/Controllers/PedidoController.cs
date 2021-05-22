using Clases_auxiliares;
using Entidades;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace TP_PW3_GRUPO_5.Controllers
{
    public class PedidoController : Controller
    {
        public IActionResult Index()
        {
            return View(ObtenerPedidos());
        }


        [HttpPost]
        public IActionResult ObtenerFiltros([FromBody] PedidoFiltro pedidoFiltro)
        {

            var resultado = JsonSerializer.Serialize(ObtenerPedidos(pedidoFiltro));

            return Content(resultado);

        }

        public List<Pedido> ObtenerPedidos(PedidoFiltro pedidoFiltro = null)
    {
            List<Pedido> listaPedidos = new List<Pedido>();

            for (int i = 0; i < 80; i++)
            {
                Cliente micliente1 = new Cliente();
                micliente1.Nombre = "Pepe";

                Cliente micliente2 = new Cliente();
                micliente2.Nombre = "Roberto";

                Cliente micliente3 = new Cliente();
                micliente3.Nombre = "Windows";

                EstadoPedido estadoPedido1 = new EstadoPedido();
                estadoPedido1.Descripcion = "Abierto";

                EstadoPedido estadoPedido2 = new EstadoPedido();
                estadoPedido2.Descripcion = "Cerrado";

                EstadoPedido estadoPedido3 = new EstadoPedido();
                estadoPedido3.Descripcion = "Entregado";

                Pedido miPedido1 = new Pedido();
                miPedido1.NroPedido = i;
                miPedido1.Cliente = micliente1;
                miPedido1.EstadoPedido = estadoPedido1;
                miPedido1.FechaCreacion = DateTime.Now.AddDays(-1);
                miPedido1.FechaModificacion = DateTime.Now;

                listaPedidos.Add(miPedido1);

                Pedido miPedido2 = new Pedido();
                miPedido2.NroPedido = i;
                miPedido2.Cliente = micliente2;
                miPedido2.EstadoPedido = estadoPedido2;
                miPedido2.FechaCreacion = DateTime.Now.AddMonths(-4);
                miPedido2.FechaModificacion = DateTime.Now;

                listaPedidos.Add(miPedido2);

                Pedido miPedido3 = new Pedido();
                miPedido3.NroPedido = i;
                miPedido3.Cliente = micliente3;
                miPedido3.EstadoPedido = estadoPedido3;
                miPedido3.FechaCreacion = DateTime.Now.AddDays(-5);
                miPedido3.FechaModificacion = DateTime.Now;
                miPedido3.FechaBorrado = DateTime.Now.AddHours(+2);

                listaPedidos.Add(miPedido3);
            }

            if (pedidoFiltro != null)
            {
                //if (pedidoFiltro.Descripcion != "")
                //{
                //    listaArticulos = listaArticulos.Where(l => l.Descripcion == (articuloFiltro.Descripcion)).ToList();
                //}
                //if (articuloFiltro.Codigo != null)
                //{
                //    listaArticulos = listaArticulos.Where(l => l.Codigo == (articuloFiltro.Codigo)).ToList();
                //}
                //if (articuloFiltro.Eliminado)
                //{
                //    listaArticulos = listaArticulos.Where(l => l.FechaBorrado < DateTime.Now).ToList();
                //}

            }


            listaPedidos = listaPedidos.OrderBy(u => u.EstadoPedido.Descripcion).ThenBy(o=> o.FechaModificacion).ToList();

            return listaPedidos;

        }
    }
}