using Clases_auxiliares;
using Contexto_de_datos.Models;
using System.Collections.Generic;

namespace Servicios
{
    public interface IPedidoServicio
    {
        public List<Pedido> ObtenerPedidos(PedidoFiltro pedidoFiltro = null);
    }
}