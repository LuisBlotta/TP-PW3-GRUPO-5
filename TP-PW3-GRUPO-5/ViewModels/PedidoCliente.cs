using Entidades;
using System;
using System.Collections.Generic;

namespace ViewModels
{
    public class PedidoCliente
    {
        public string Cliente { get; set; }
        public int NroPedido { get; set; }
        public string Estado { get; set; }
        public string UltimaModificacion { get; set; }

        public List<PedidoCliente> obtenerPedidosCliente (List<Pedido> pedidos)
        {
            List<PedidoCliente> pedidosClientes = new List<PedidoCliente>();

            foreach (var pedido in pedidos)
            {
                PedidoCliente pedidoCliente = new PedidoCliente();
                pedidoCliente.Cliente = pedido.Cliente.Nombre;
                pedidoCliente.NroPedido = pedido.NroPedido;
                pedidoCliente.Estado = pedido.EstadoPedido.Descripcion;
                pedidoCliente.UltimaModificacion = calcularUltimaModificacion(pedido);
            }

            return pedidosClientes;
        }
    
        public string calcularUltimaModificacion(Pedido pedido)
        {
            DateTime fechaActual = DateTime.Now;
            DateTime fechaUltimaModificacion = pedido.FechaModificacion;
            double horasDiferencia = (fechaActual - fechaUltimaModificacion).TotalHours;


            return "";
        }
    }
}
