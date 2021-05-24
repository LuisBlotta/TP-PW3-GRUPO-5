using Clases_auxiliares;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class ClienteServicio : IClienteServicio
    {
        public List<Cliente> ObtenerClientes(ClienteFiltro clienteFiltro = null)
        {
            List<Cliente> listaClientes = new List<Cliente>();

            for (int i = 0; i < 80; i++)
            {
                Cliente micliente1 = new Cliente();
                micliente1.Nombre = "Pepe";
                micliente1.Numero = i;
                micliente1.Telefono = "1111-2222";


                listaClientes.Add(micliente1);

                Cliente micliente2 = new Cliente();
                micliente2.Nombre = "Juanito";
                micliente2.Numero = i;
                micliente2.Telefono = "1111-2222";



                listaClientes.Add(micliente2);

                Cliente micliente3 = new Cliente();
                micliente3.Nombre = "Manolo";
                micliente3.Numero = i;
                micliente3.Telefono = "1111-2222";
                micliente3.BorradoPor = new Usuario();



                listaClientes.Add(micliente3);


            }

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
