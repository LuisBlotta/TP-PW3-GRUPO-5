using Clases_auxiliares;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class PedidoServicio : IPedidoServicio
    {
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

                Cliente micliente4 = new Cliente();
                micliente4.Nombre = "Linux";

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
                miPedido1.FechaCreacion = DateTime.Now.AddDays(-2);
                miPedido1.FechaModificacion = DateTime.Now.AddHours(-3);

                listaPedidos.Add(miPedido1);

                Pedido miPedido2 = new Pedido();
                miPedido2.NroPedido = i;
                miPedido2.Cliente = micliente2;
                miPedido2.EstadoPedido = estadoPedido2;
                miPedido2.FechaCreacion = DateTime.Now.AddMonths(-4);
                miPedido2.FechaModificacion = DateTime.Now.AddDays(-5);

                listaPedidos.Add(miPedido2);

                Pedido miPedido3 = new Pedido();
                miPedido3.NroPedido = i;
                miPedido3.Cliente = micliente3;
                miPedido3.EstadoPedido = estadoPedido3;
                miPedido3.FechaCreacion = DateTime.Now.AddDays(-5);
                miPedido3.FechaModificacion = DateTime.Now.AddMinutes(-10);
                miPedido3.BorradoPor = new Usuario();

                listaPedidos.Add(miPedido3);

                Pedido miPedido4 = new Pedido();
                miPedido4.NroPedido = i;
                miPedido4.Cliente = micliente4;
                miPedido4.EstadoPedido = estadoPedido3;
                miPedido4.FechaCreacion = DateTime.Now.AddDays(-1);
                miPedido4.FechaModificacion = DateTime.Now.AddDays(-1).AddHours(-3);
                miPedido4.BorradoPor = new Usuario();


                listaPedidos.Add(miPedido4);
            }

            if (pedidoFiltro != null)
            {
                if (pedidoFiltro.Cliente != "")
                {
                    listaPedidos = listaPedidos.Where(l => l.Cliente.Nombre == (pedidoFiltro.Cliente)).ToList();
                }
                if (pedidoFiltro.Estado != "")
                {
                    listaPedidos = listaPedidos.Where(l => l.EstadoPedido.Descripcion == (pedidoFiltro.Estado)).ToList();
                }
                if (pedidoFiltro.Eliminado)
                {
                    listaPedidos = listaPedidos.Where(l => l.BorradoPor == null).ToList();
                }
                if (pedidoFiltro.UltimosDosMeses)
                {
                    listaPedidos = listaPedidos.Where(l => l.FechaCreacion >= DateTime.Now.AddMonths(-2)).ToList();
                }

            }

            listaPedidos = listaPedidos.OrderBy(u => u.EstadoPedido.Descripcion).ThenByDescending(o => o.FechaModificacion).ToList();

            return listaPedidos;
        }
    }
}
