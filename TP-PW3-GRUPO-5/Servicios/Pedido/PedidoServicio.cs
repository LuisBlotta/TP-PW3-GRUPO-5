using Clases_auxiliares;
using Contexto_de_datos.Models;
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
         

            if (pedidoFiltro != null)
            {
                if (pedidoFiltro.Cliente != "")
                {
                    listaPedidos = listaPedidos.Where(l => l.IdClienteNavigation.Nombre == (pedidoFiltro.Cliente)).ToList();
                }
                if (pedidoFiltro.Estado != "")
                {
                    listaPedidos = listaPedidos.Where(l => l.IdEstadoNavigation.Descripcion == (pedidoFiltro.Estado)).ToList();
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

            listaPedidos = listaPedidos.OrderBy(u => u.IdEstadoNavigation.Descripcion).ThenByDescending(o => o.FechaModificacion).ToList();

            return listaPedidos;
        }
    }
}
