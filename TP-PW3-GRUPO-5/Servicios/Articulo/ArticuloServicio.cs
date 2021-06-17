using Clases_auxiliares;
using Contexto_de_datos.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Servicios
{
    public class ArticuloServicio : IArticuloServicio
    {
        private _20211CTPContext context;

        public ArticuloServicio(_20211CTPContext ctx)
        {
            context = ctx;
        }

        public void Alta(Articulo articulo)
        {
            context.Articulos.Add(articulo);
            context.SaveChanges();
        }

        public void Baja(int id)
        {
            Articulo articulo = ObtenerPorId(id);
            articulo.FechaBorrado = DateTime.Now;
            // FALTA BORRADO POR
            context.SaveChanges();
        }

        public void Modificar(Articulo articulo)
        {
            Articulo articuloBD = ObtenerPorId(articulo.IdArticulo);
            articuloBD.Codigo = articulo.Codigo;
            articuloBD.Descripcion = articulo.Descripcion;
            articuloBD.FechaModificacion = DateTime.Now;
            //FALTA MODIFICADO POR
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

        public Articulo ObtenerPorId(int id)
        {
            return context.Articulos.Find(id);
        }
    }
}
