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
        private _20211CTPContext context;

        public ClienteServicio(_20211CTPContext ctx)
        {
            context = ctx;
        }
        public void Alta(Cliente cliente)
        {
            cliente.FechaCreacion = DateTime.Now;
            context.Clientes.Add(cliente);
            context.SaveChanges();

        }

        public void Baja(int id)
        {
            Cliente cliente = ObtenerPorId(id);
            cliente.FechaBorrado = DateTime.Now;
            //FALTA BORRADO POR
            context.SaveChanges();
        }

        public void Modificar(Cliente cliente)
        {
            Cliente clienteBD = ObtenerPorId(cliente.IdCliente);
            clienteBD.Nombre = cliente.Nombre;
            clienteBD.Numero = cliente.Numero;
            clienteBD.Telefono = cliente.Telefono;
            clienteBD.Direccion = cliente.Direccion;
            clienteBD.Email = cliente.Email;
            clienteBD.Cuit = cliente.Cuit;
            clienteBD.FechaModificacion = DateTime.Now;
            //FALTA AGREGAR MODIFICADO POR
            context.SaveChanges();
        }

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

        public Cliente ObtenerPorId(int id)
        {
            return context.Clientes.Find(id);
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
