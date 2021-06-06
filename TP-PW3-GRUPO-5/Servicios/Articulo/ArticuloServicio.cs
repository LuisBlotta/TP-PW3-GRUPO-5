using Clases_auxiliares;
using Contexto_de_datos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class ArticuloServicio : IArticuloServicio
    {
        _20211CTPContext context = new _20211CTPContext();

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
    }
}
