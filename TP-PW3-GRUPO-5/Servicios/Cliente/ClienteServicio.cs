using Clases_auxiliares;
using Contexto_de_datos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class ClienteServicio : IClienteServicio
    {
        _20211CTPContext context = new _20211CTPContext();
        public List<Cliente> ObtenerClientes(ClienteFiltro clienteFiltro = null)
        {
            List<Cliente> listaClientes = context.Clientes.ToList();

            if (clienteFiltro != null)
            {
                if (clienteFiltro.Nombre != "")
                {
                    listaClientes = listaClientes.Where(l => l.Nombre == (clienteFiltro.Nombre)).ToList();
                }
                if (clienteFiltro.Numero != null)
                {
                    listaClientes = listaClientes.Where(l => l.Numero == (clienteFiltro.Numero)).ToList();
                }
                if (clienteFiltro.Eliminado)
                {
                    listaClientes = listaClientes.Where(l => l.BorradoPor == null).ToList();
                }

            }


            listaClientes = listaClientes.OrderBy(u => u.Nombre).ToList();

            return listaClientes;
        }

        public List<string> ObtenerSelectClientes()
        {
            List<string> selectClientes = new List<string>();

            foreach (Cliente c in ObtenerClientes())
            {
                if (selectClientes.Count == 0)
                {
                    selectClientes.Add(c.Nombre);
                }
                if (!selectClientes.Contains(c.Nombre))
                {
                    selectClientes.Add(c.Nombre);
                }
            }
            return selectClientes;
        }
    }
}
