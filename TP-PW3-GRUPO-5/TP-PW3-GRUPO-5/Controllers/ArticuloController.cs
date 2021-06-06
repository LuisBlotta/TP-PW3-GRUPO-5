using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Entidades;
using Servicios;
using Clases_auxiliares;
using System.Text.Json;
using TP_PW3_GRUPO_5.Models;
using Contexto_de_datos.Models;

namespace TP_PW3_GRUPO_5.Controllers
{
    public class ArticuloController : Controller
    {
        IArticuloServicio articuloServicio;
        _20211CTPContext context;

           

        public ArticuloController(_20211CTPContext ctx)
        {
            context = ctx;
            articuloServicio = new ArticuloServicio();
        }
        public IActionResult Index()
        {
            return View(articuloServicio.ObtenerArticulos());
        }

        public IActionResult NuevoArticulo()
        {

            return View();
        }

        public IActionResult PruebaNuevoArticulo()
        {
            Articulo prueba = new Articulo();


            prueba.Codigo = "8";
            prueba.Descripcion = "PruebaNuevoArt";

            context.Articulos.Add(prueba);
            context.SaveChanges();

            return View(context.Articulos.ToList());
            //return RedirectToAction(nameof(Index));
        }

        public IActionResult PruebaModificarArticulo(int id)
        {
            Articulo articulo = context.Articulos.Find(id);
            articulo.Codigo = "2";
            context.SaveChanges();

            return RedirectToAction(nameof(PruebaNuevoArticulo));
            //return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult NuevoArticulo(Articulo articulo)
        {

            return View();
        }
        public IActionResult DetalleArticulo(string accion)
        {
            Articulo miArticulo1 = new Articulo();
            miArticulo1.IdArticulo = 205;
            miArticulo1.Codigo = "22";
            miArticulo1.Descripcion = "Maquina de cortar pasto";

            ViewData["accion"] = accion;
            return View(miArticulo1);
        }

        [HttpPost]
        public IActionResult EditarArticulo(Articulo articulo)
        {

            return View();
        }


        [HttpPost]
        public IActionResult ObtenerFiltros([FromBody] ArticuloFiltro articuloFiltro)
        {

            var resultado = JsonSerializer.Serialize(articuloServicio.ObtenerArticulos(articuloFiltro));

            return Content(resultado);

        }

    }
}
