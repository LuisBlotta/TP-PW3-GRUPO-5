using Clases_auxiliares;
using Contexto_de_datos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Servicios
{
    public class PedidoServicio : IPedidoServicio
    {
        private _20211CTPContext context;
        private IClienteServicio clienteServicio;
        private IArticuloServicio articuloServicio;
        public PedidoServicio(_20211CTPContext ctx)
        {
            context = ctx;
            clienteServicio = new ClienteServicio(context);
            articuloServicio = new ArticuloServicio(context);
        }

        public void Alta(PedidoNuevoFiltro pedidoNuevoFiltro)
        {
            Pedido miPedido = new Pedido();
            miPedido.FechaCreacion = DateTime.Now;
            miPedido.IdCliente = pedidoNuevoFiltro.IdCliente;
            miPedido.IdEstado = 1;
            miPedido.NroPedido = ObtenerNumeroPedido();
            miPedido.Comentarios = pedidoNuevoFiltro.Comentarios;
            //FALTA CREADO POR
            context.Pedidos.Add(miPedido);
            context.SaveChanges();
            int idPedido = context.Pedidos.Max(o=>o.IdPedido);
            foreach (ArticuloCantidad articulo in pedidoNuevoFiltro.Articulos)
            {
                PedidoArticulo pedidoArticulo = new PedidoArticulo();
                pedidoArticulo.Cantidad = articulo.Cantidad;
                pedidoArticulo.IdArticulo = articuloServicio.ObtenerPorCodigo(articulo.Codigo).IdArticulo;
                pedidoArticulo.IdPedido = idPedido;
                context.PedidoArticulos.Add(pedidoArticulo);
            }
            context.SaveChanges();

        }

        public void Baja(int id)
        {
            Pedido pedido = ObtenerPorId(id);
            pedido.FechaBorrado = DateTime.Now;
            //FALTA BORRADO POR
            context.SaveChanges();
        }

        public void Modificar(EditarPedido pedido)
        {
            Pedido pedidoBD = ObtenerPorId(pedido.IdPedido);
            pedidoBD.IdEstado = pedido.EstadoPedido;
            pedidoBD.Comentarios = pedido.Comentarios;
            if(pedido.Articulos != null)
            {
                pedidoBD.PedidoArticulos.Clear();
                context.SaveChanges();

                foreach (ArticuloCantidad articulo in pedido.Articulos)
                {
                    PedidoArticulo pedidoArticulo = new PedidoArticulo();
                    pedidoArticulo.Cantidad = articulo.Cantidad;
                    pedidoArticulo.IdArticulo = articuloServicio.ObtenerPorCodigo(articulo.Codigo).IdArticulo;
                    pedidoArticulo.IdPedido = pedido.IdPedido;
                    context.PedidoArticulos.Add(pedidoArticulo);
                }
            }
            pedidoBD.FechaModificacion = DateTime.Now;
            // FALTA MODIFICADO POR
            context.SaveChanges();

        }

        public int ObtenerNumeroPedido()
        {
            return context.Pedidos.Max(o => o.NroPedido) + 1;
        }

        public List<Pedido> ObtenerPedidos(PedidoFiltro pedidoFiltro = null)
        {
            List<Pedido> listaPedidos = context.Pedidos.Include(o=>o.IdEstadoNavigation).Include(o=>o.IdClienteNavigation).ToList();
         

            if (pedidoFiltro != null)
            {
                if (pedidoFiltro.Cliente != "")
                {
                    listaPedidos = listaPedidos.Where(l => l.IdClienteNavigation.Nombre == (pedidoFiltro.Cliente)).ToList();
                }
                if (pedidoFiltro.Estado != "")
                {
                    listaPedidos = listaPedidos.Where(l => l.IdEstadoNavigation.Descripcion == (pedidoFiltro.Estado)).ToList();
                }
                if (pedidoFiltro.Eliminado)
                {
                    listaPedidos = listaPedidos.Where(l => l.BorradoPor == null).ToList();
                }
                if (pedidoFiltro.UltimosDosMeses)
                {
                    listaPedidos = listaPedidos.Where(l => l.FechaCreacion >= DateTime.Now.AddMonths(-2)).ToList();
                }

            }

            listaPedidos = listaPedidos.OrderBy(u => u.IdEstadoNavigation.Descripcion).ThenByDescending(o => o.FechaModificacion).ToList();

            return listaPedidos;
        }

        public Pedido ObtenerPorId(int id)
        {
            return context.Pedidos.Include(o=>o.PedidoArticulos).Include(o=>o.IdClienteNavigation).Include(o=>o.IdEstadoNavigation).FirstOrDefault(o=>o.IdPedido == id);
        }
        public List<PedidoArticulo> ObtenerPedidosArticulos(int id)
        {
            return context.PedidoArticulos.Include(o => o.IdArticuloNavigation).Where(o=>o.IdPedido == id).ToList();
        }
        public List<ArticuloCantidad> ObtenerPedidoDetalle(int id)
        {
            List<PedidoArticulo> pedidosArticulos = ObtenerPedidosArticulos(id);
            List<ArticuloCantidad> articulosCantidad = new List<ArticuloCantidad>();

            foreach (PedidoArticulo pedidoArticulo in pedidosArticulos)
            {
                ArticuloCantidad articuloCantidad = new ArticuloCantidad();
                articuloCantidad.Cantidad = pedidoArticulo.Cantidad;
                articuloCantidad.Codigo = pedidoArticulo.IdArticuloNavigation.Codigo;
                articuloCantidad.Descripcion = pedidoArticulo.IdArticuloNavigation.Descripcion;
                articulosCantidad.Add(articuloCantidad);
            }

            return articulosCantidad;
        }
    }
}
