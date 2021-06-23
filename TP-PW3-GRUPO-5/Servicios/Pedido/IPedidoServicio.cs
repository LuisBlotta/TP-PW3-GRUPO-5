using Clases_auxiliares;
using Contexto_de_datos.Models;
using System.Collections.Generic;

namespace Servicios
{
    public interface IPedidoServicio
    {
        public List<Pedido> ObtenerPedidos(PedidoFiltro pedidoFiltro = null);

        public void Alta(PedidoNuevoFiltro pedidoNuevoFiltro);
        public int ObtenerNumeroPedido();
        public void Baja(int id);
        public void Modificar(EditarPedido pedido);
        public Pedido ObtenerPorId(int id);
        public List<ArticuloCantidad> ObtenerPedidoDetalle(int id);
        public List<PedidoArticulo> ObtenerPedidosArticulos(int id);

    }
}