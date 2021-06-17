using Contexto_de_datos.Models;
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
                    clientesEstadoPedido.Clientes.Add(p.IdClienteNavigation.Nombre);
                }
                if (!clientesEstadoPedido.Clientes.Contains(p.IdClienteNavigation.Nombre))
                {
                    clientesEstadoPedido.Clientes.Add(p.IdClienteNavigation.Nombre);
                }


                if (clientesEstadoPedido.EstadosPedidos.Count == 0)
                {
                    clientesEstadoPedido.EstadosPedidos.Add(p.IdEstadoNavigation.Descripcion);
                }
                if (!clientesEstadoPedido.EstadosPedidos.Contains(p.IdEstadoNavigation.Descripcion))
                {
                    clientesEstadoPedido.EstadosPedidos.Add(p.IdEstadoNavigation.Descripcion);
                }

            }
            return clientesEstadoPedido;
        }
    }
}
