using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entidades;
using Clases_auxiliares;
using System.Text.Json;

namespace TP_PW3_GRUPO_5.Controllers
{
    public class ArticuloController : Controller
    {
        public IActionResult Index()
        {
            return View(ObtenerArticulos());
        }

        [HttpPost]
        public IActionResult ObtenerFiltros([FromBody] ArticuloFiltro articuloFiltro)
        {

            var resultado = JsonSerializer.Serialize(ObtenerArticulos(articuloFiltro));

            return Content(resultado);

        }

        public List<Articulo> ObtenerArticulos(ArticuloFiltro articuloFiltro = null)
        {

            List<Articulo> listaArticulos = new List<Articulo>();

            for (int i = 0; i < 80; i++)
            {
                Articulo miArticulo1 = new Articulo();
                miArticulo1.Descripcion = "Tuerca" + i;
                miArticulo1.Codigo = i;


                listaArticulos.Add(miArticulo1);

                Articulo miArticulo2 = new Articulo();
                miArticulo2.Descripcion = "Tornillo" + i;
                miArticulo2.Codigo = i;


                listaArticulos.Add(miArticulo2);

                Articulo miArticulo3 = new Articulo();
                miArticulo3.Descripcion = "Martillo" + i;
                miArticulo3.Codigo = i;
                miArticulo3.FechaBorrado = DateTime.Now.AddHours(+2);


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
                    listaArticulos = listaArticulos.Where(l => l.FechaBorrado < DateTime.Now).ToList();
                }

            }


            listaArticulos = listaArticulos.OrderBy(u => u.Codigo).ToList();

            return listaArticulos;

        }

    }
}
