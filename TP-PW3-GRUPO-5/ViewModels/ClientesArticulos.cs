using Contexto_de_datos.Models;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class ClientesArticulos
    {
        public List<Cliente> Clientes { get; set; } = new List<Cliente>();

        public List<Articulo> Articulos { get; set; } = new List<Articulo>();

        public static ClientesArticulos ObtenerClientesArticulos(List<Cliente> clientes, List<Articulo> articulos)
        {
            ClientesArticulos clientesArticulos = new ClientesArticulos();
            //Filtra los que no estan eliminados.
            clientesArticulos.Clientes = clientes.Where(o => o.BorradoPor == null).OrderBy(o => o.Nombre).ToList();
            List<Cliente> listaClientes = new List<Cliente>();


            //Filtra los que no tienen pedidos abiertos.
            foreach (Cliente c in clientesArticulos.Clientes)
            {
                foreach (Pedido p in c.Pedidos)
                {
                    if(p.IdEstado == 1 && p.BorradoPor == null)
                    {
                        listaClientes.Add(c);
                        break;
                    }
                }
            }

            foreach (Cliente c in listaClientes)
            {
                clientesArticulos.Clientes.Remove(c);
            }

            //Filtra los que no estan eliminados.
            clientesArticulos.Articulos = articulos.Where(o => o.BorradoPor == null).OrderBy(o => o.Descripcion).ToList();

            return clientesArticulos;
        }
    }
}
