using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class ClientesEstadoPedido
    {
        public List<string> Clientes { get; set; } = new List<string>();
        public List<string> EstadosPedidos { get; set; } = new List<string>();

        public static ClientesEstadoPedido ObtenerInfoSelects(List<Pedido> pedidos)
        {
            ClientesEstadoPedido clientesEstadoPedido = new ClientesEstadoPedido();

            foreach (Pedido p in pedidos)
            {

                if (clientesEstadoPedido.Clientes.Count == 0)
                {
                    clientesEstadoPedido.Clientes.Add(p.Cliente.Nombre);
                }
                if (!clientesEstadoPedido.Clientes.Contains(p.Cliente.Nombre))
                {
                    clientesEstadoPedido.Clientes.Add(p.Cliente.Nombre);
                }


                if (clientesEstadoPedido.EstadosPedidos.Count == 0)
                {
                    clientesEstadoPedido.EstadosPedidos.Add(p.EstadoPedido.Descripcion);
                }
                if (!clientesEstadoPedido.EstadosPedidos.Contains(p.EstadoPedido.Descripcion))
                {
                    clientesEstadoPedido.EstadosPedidos.Add(p.EstadoPedido.Descripcion);
                }

            }
            return clientesEstadoPedido;
        }
    }
}
