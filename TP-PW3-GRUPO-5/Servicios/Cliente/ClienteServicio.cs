using Clases_auxiliares;
using Contexto_de_datos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Servicios.Session;
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
        private ISessionManager sessionManager;

        public ClienteServicio(_20211CTPContext ctx, IHttpContextAccessor _httpContextAccessor)
        {
            context = ctx;
            sessionManager = new SessionManager(_httpContextAccessor);

        }
        public void Alta(Cliente cliente)
        {
            cliente.CreadoPor = sessionManager.ObtenerIDUsuarioLogueado();
            cliente.FechaCreacion = DateTime.Now;
            context.Clientes.Add(cliente);
            context.SaveChanges();

        }

        public void Baja(int id)
        {
            Cliente cliente = ObtenerPorId(id);
            cliente.FechaBorrado = DateTime.Now;
            cliente.BorradoPor = sessionManager.ObtenerIDUsuarioLogueado();
            foreach (Pedido p in cliente.Pedidos)            
            {
                p.FechaBorrado = DateTime.Now;
                p.BorradoPor = sessionManager.ObtenerIDUsuarioLogueado(); 
            }
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
            clienteBD.ModificadoPor = sessionManager.ObtenerIDUsuarioLogueado();
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
            return context.Clientes.Include(o=>o.Pedidos).FirstOrDefault(o=>o.IdCliente == id);
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
        public List<Cliente> ObtenerClientesConPedidos()
        {
            return context.Clientes.Include(o => o.Pedidos).ToList();
        }

        public bool ConsultarEstadoPedidos(int id)
        {
            Cliente cliente = ObtenerPorId(id);
            foreach (Pedido pedido in cliente.Pedidos)
            {
                if(pedido.BorradoPor== null)
                {
                    return true;
                }
            }
            return false; 
        }
        public bool TienePedidosAbiertos(int id)
        {
            Cliente cliente = ObtenerPorId(id);
            foreach (Pedido pedido in cliente.Pedidos)
            {
                if(pedido.IdEstado == 1 && pedido.BorradoPor == null)
                {
                    return true;
                }
            }
            return false;
        }
        public List<Cliente> FiltrarPorNombre(string nombre)
        {

            return ObtenerClientes().Where(o => o.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public bool ExisteElCliente(int id)
        {
            return ObtenerPorId(id) != null ? true : false;
        }
    }
}
