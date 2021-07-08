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

    public class ArticuloServicio : IArticuloServicio
    {
        private _20211CTPContext context;
        private ISessionManager sessionManager;

        public ArticuloServicio(_20211CTPContext ctx,IHttpContextAccessor _httpContextAccessor)
        {
            context = ctx;
            sessionManager = new SessionManager(_httpContextAccessor);
        }

        public void Alta(Articulo articulo)
        {
            articulo.CreadoPor = sessionManager.ObtenerIDUsuarioLogueado();
            context.Articulos.Add(articulo);
            context.SaveChanges();
        }

        public void Baja(int id)
        {
            Articulo articulo = ObtenerPorId(id);
            articulo.FechaBorrado = DateTime.Now;
            articulo.BorradoPor = sessionManager.ObtenerIDUsuarioLogueado();
            articulo.PedidoArticulos.Clear();
            context.SaveChanges();
        }

        public void Modificar(Articulo articulo)
        {
            Articulo articuloBD = ObtenerPorId(articulo.IdArticulo);
            articuloBD.Codigo = articulo.Codigo;
            articuloBD.Descripcion = articulo.Descripcion;
            articuloBD.FechaModificacion = DateTime.Now;
            articuloBD.ModificadoPor = sessionManager.ObtenerIDUsuarioLogueado();
            context.SaveChanges();
        }

        public List<Articulo> ObtenerArticulos(ArticuloFiltro articuloFiltro = null)
        {
            List<Articulo> listaArticulos = context.Articulos.ToList();

            if (articuloFiltro != null)
            {
                if (articuloFiltro.Descripcion != "")
                {
                    listaArticulos = listaArticulos.Where(l => l.Descripcion == (articuloFiltro.Descripcion)).ToList();
                }
                if (articuloFiltro.Codigo != "")
                {
                    listaArticulos = listaArticulos.Where(l => l.Codigo == (articuloFiltro.Codigo)).ToList();
                }
                if (articuloFiltro.Eliminado)
                {
                    listaArticulos = listaArticulos.Where(l => l.BorradoPor == null ).ToList();
                }

            }


            listaArticulos = listaArticulos.OrderBy(u => u.Codigo).ToList();

            return listaArticulos;
        }

        public Articulo ObtenerPorCodigo(string codigo)
        {
            return context.Articulos.First(o => o.Codigo == codigo);
        }

        public Articulo ObtenerPorId(int id)
        {
            return context.Articulos.Include(o => o.PedidoArticulos).FirstOrDefault(o=>o.IdArticulo == id);
        }

        public bool ConsultarEstadoPedidos(int id)
        {
            Articulo articulo = ObtenerPorId(id);
            foreach (PedidoArticulo art in articulo.PedidoArticulos)
            {
                Pedido pedido = context.Pedidos.Find(art.IdPedido);

                if (pedido.BorradoPor == null)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Articulo> FiltrarPorDescripcion(string descripcion)
        {
            return ObtenerArticulos().Where(o => o.Descripcion.Contains(descripcion, StringComparison.OrdinalIgnoreCase)).ToList();

        }
    }
}
