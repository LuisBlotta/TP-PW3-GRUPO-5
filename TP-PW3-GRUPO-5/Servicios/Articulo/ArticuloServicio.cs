using Clases_auxiliares;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class ArticuloServicio : IArticuloServicio
    {
        public List<Articulo> ObtenerArticulos(ArticuloFiltro articuloFiltro = null)
        {
            List<Articulo> listaArticulos = new List<Articulo>();

            for (int i = 0; i < 80; i++)
            {
                Articulo miArticulo1 = new Articulo();
                miArticulo1.Descripcion = "Tuerca";
                miArticulo1.Codigo = i;


                listaArticulos.Add(miArticulo1);

                Articulo miArticulo2 = new Articulo();
                miArticulo2.Descripcion = "Tornillo";
                miArticulo2.Codigo = i;


                listaArticulos.Add(miArticulo2);

                Articulo miArticulo3 = new Articulo();
                miArticulo3.Descripcion = "Martillo";
                miArticulo3.Codigo = i;
                miArticulo3.BorradoPor = new Usuario();


                listaArticulos.Add(miArticulo3);


            }

            if (articuloFiltro != null)
            {
                if (articuloFiltro.Descripcion != "")
                {
                    listaArticulos = listaArticulos.Where(l => l.Descripcion == (articuloFiltro.Descripcion)).ToList();
                }
                if (articuloFiltro.Codigo != null)
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
