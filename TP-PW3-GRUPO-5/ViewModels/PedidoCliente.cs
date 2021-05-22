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

        public static List<PedidoCliente> ObtenerPedidosCliente (List<Pedido> pedidos)
        {
            List<PedidoCliente> pedidosClientes = new List<PedidoCliente>();

            foreach (var pedido in pedidos)
            {
                PedidoCliente pedidoCliente = new PedidoCliente();
                pedidoCliente.Cliente = pedido.Cliente.Nombre;
                pedidoCliente.NroPedido = pedido.NroPedido;
                pedidoCliente.Estado = pedido.EstadoPedido.Descripcion;
                pedidoCliente.UltimaModificacion = CalcularUltimaModificacion(pedido);
                pedidosClientes.Add(pedidoCliente);
            }

            return pedidosClientes;
        }
    
        
        public static string CalcularUltimaModificacion(Pedido pedido)
        {
            DateTime fechaActual = DateTime.Now;
            DateTime fechaUltimaModificacion = pedido.FechaModificacion;
            double horasDiferencia = (fechaActual - fechaUltimaModificacion).TotalHours;
            string ultimaModificacion = "";
            string cliente = pedido.Cliente.Nombre;

            
            
            if (horasDiferencia < 1)
            {
                double minutos = Math.Round(horasDiferencia * 60);
                ultimaModificacion = "Hace " + minutos + " minutos (" + cliente + ")";
            }
            if(horasDiferencia>= 1 && horasDiferencia < 24)
            {
                ultimaModificacion = "Hoy " + fechaUltimaModificacion.ToString("HH:mm") + " (" + cliente + ")";
            }
            if (horasDiferencia>= 24 && horasDiferencia <48)
            {
                ultimaModificacion = "Ayer " + fechaUltimaModificacion.ToString("HH:mm") + " (" + cliente + ")";
            }
            if(horasDiferencia>=48)
            {
                ultimaModificacion = "El día " + fechaUltimaModificacion.ToString("dd-MM-yyyy HH:mm") + " (" + cliente + ")";
            }

            return ultimaModificacion;
        }
    }
}
