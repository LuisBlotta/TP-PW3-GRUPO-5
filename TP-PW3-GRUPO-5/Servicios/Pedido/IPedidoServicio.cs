using Clases_auxiliares;
using Entidades;
using System.Collections.Generic;

namespace Servicios
{
    public interface IPedidoServicio
    {
        public List<Pedido> ObtenerPedidos(PedidoFiltro pedidoFiltro = null);
    }
}