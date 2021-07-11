using Clases_auxiliares;
using Contexto_de_datos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Servicios.Session;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Servicios
{
    public class PedidoServicio : IPedidoServicio
    {
        private _20211CTPContext context;
        private IArticuloServicio articuloServicio;
        private ISessionManager sessionManager;
        private IClienteServicio clienteServicio;
        public PedidoServicio(_20211CTPContext ctx, IHttpContextAccessor _httpContextAccessor)
        {
            context = ctx;
            articuloServicio = new ArticuloServicio(context, _httpContextAccessor);
            sessionManager = new SessionManager(_httpContextAccessor);
            clienteServicio = new ClienteServicio(context, _httpContextAccessor);
        }

        public void Alta(PedidoNuevoFiltro pedidoNuevoFiltro)
        {
            Pedido miPedido = new Pedido();
            miPedido.FechaCreacion = DateTime.Now;
            miPedido.IdCliente = pedidoNuevoFiltro.IdCliente;
            miPedido.IdEstado = 1;
            miPedido.NroPedido = ObtenerNumeroPedido();
            miPedido.Comentarios = pedidoNuevoFiltro.Comentarios;
            miPedido.CreadoPor = sessionManager.ObtenerIDUsuarioLogueado();
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
        public bool AltaPedidoPorAPI(PedidoNuevoFiltro pedidoNuevoFiltro)
        {
            if (ValidarNuevoPedido(pedidoNuevoFiltro))
            {
                Pedido miPedido = new Pedido();
                miPedido.FechaCreacion = DateTime.Now;
                miPedido.IdCliente = pedidoNuevoFiltro.IdCliente;
                miPedido.IdEstado = 1;
                miPedido.NroPedido = ObtenerNumeroPedido();
                miPedido.Comentarios = pedidoNuevoFiltro.Comentarios;
                miPedido.CreadoPor = sessionManager.ObtenerIDUsuarioLogueado();
                context.Pedidos.Add(miPedido);
                context.SaveChanges();
                int idPedido = context.Pedidos.Max(o => o.IdPedido);
                foreach (ArticuloCantidad articulo in pedidoNuevoFiltro.Articulos)
                {
                    PedidoArticulo pedidoArticulo = new PedidoArticulo();
                    pedidoArticulo.Cantidad = articulo.Cantidad;
                    pedidoArticulo.IdArticulo = articuloServicio.ObtenerPorCodigo(articulo.Codigo).IdArticulo;
                    pedidoArticulo.IdPedido = idPedido;
                    context.PedidoArticulos.Add(pedidoArticulo);
                }
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool ValidarNuevoPedido(PedidoNuevoFiltro pedidoNuevoFiltro)
        {
            if (clienteServicio.ExisteElCliente(pedidoNuevoFiltro.IdCliente))
            {
                if (!clienteServicio.TienePedidosAbiertos(pedidoNuevoFiltro.IdCliente)
                    && !articuloServicio.ArticulosEstanEliminados(pedidoNuevoFiltro.Articulos))
                {
                    return true;
                }
            }
            return false;
        }

        public void Baja(int id)
        {
            Pedido pedido = ObtenerPorId(id);
            pedido.FechaBorrado = DateTime.Now;
            pedido.BorradoPor = sessionManager.ObtenerIDUsuarioLogueado();
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
            pedidoBD.ModificadoPor = sessionManager.ObtenerIDUsuarioLogueado();
            context.SaveChanges();

        }

        public int ObtenerNumeroPedido()
        {
            return context.Pedidos.Max(o => o.NroPedido) + 1;
        }

        public List<Pedido> ObtenerPedidos(PedidoFiltro pedidoFiltro = null)
        {
            List<Pedido> listaPedidos = context.Pedidos.Include(o => o.IdEstadoNavigation).Include(o => o.IdClienteNavigation).Include(o =>o.ModificadoPorNavigation).ToList();
         

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
            return context.Pedidos.Include(o=>o.PedidoArticulos).Include(o=>o.IdClienteNavigation).Include(o=>o.IdEstadoNavigation).Include(o => o.ModificadoPorNavigation).FirstOrDefault(o=>o.IdPedido == id);
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

        public List<InfoPedido> ObtenerInfoPedidos(int idCliente, int idEstado)
        {
            List<Pedido> pedidos = context.Pedidos.Include(o => o.IdEstadoNavigation).Include(o=>o.ModificadoPorNavigation)
                .Where(o => o.IdCliente == idCliente).Where(o => o.IdEstado == idEstado)
                .ToList();

            List<InfoPedido> infoPedidos = new List<InfoPedido>();
            foreach (Pedido pedido in pedidos)
            {
                InfoPedido infoPedido = new InfoPedido();
                infoPedido.IdCliente = pedido.IdCliente;
                infoPedido.IdPedido = pedido.IdPedido;
                infoPedido.FechaModificacion = pedido.FechaModificacion;
                infoPedido.ModificadoPor = ObtenerUsuarioInfoPedido(pedido.ModificadoPorNavigation);
                infoPedido.Estado = pedido.IdEstadoNavigation.Descripcion;
                infoPedido.Articulos = ObtenerPedidoDetalle(pedido.IdPedido);
                infoPedidos.Add(infoPedido);
            }

            return infoPedidos;
        }

        public UsuarioInfoPedido ObtenerUsuarioInfoPedido(Usuario usuario)
        {
            if(usuario != null)
            {
                UsuarioInfoPedido user = new UsuarioInfoPedido();
                user.Nombre = usuario.Nombre;
                user.Apellido = usuario.Apellido;
                user.Email = usuario.Email;
                user.FechaNacimiento = usuario.FechaNacimiento;
                user.IdUsuario = usuario.IdUsuario;
                return user;
            }
            return null;
        }
    }
}
