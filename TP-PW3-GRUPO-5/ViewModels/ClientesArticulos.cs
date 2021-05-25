using Entidades;
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

        public static ClientesArticulos ObtenerClientesArticulos(List<Cliente> clientes , List<Articulo> articulos)
        {
            ClientesArticulos clientesArticulos = new ClientesArticulos();

            //Filtra los que no estan eliminados.
            clientesArticulos.Clientes = clientes.Where(o => o.BorradoPor == null).OrderBy(o => o.Nombre).ToList();
            //Filtra los que no estan eliminados.
            clientesArticulos.Articulos = articulos.Where(o => o.BorradoPor == null).OrderBy(o => o.Descripcion).ToList();

            return clientesArticulos;
        }
    }
}
