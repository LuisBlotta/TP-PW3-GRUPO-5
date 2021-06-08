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
            return View(context.Articulos.ToList());
        }

        public IActionResult NuevoArticulo()
        {

            return View();
        }


        [HttpPost]
        public IActionResult NuevoArticulo(Entidades.Articulo articulo, string submit)
        {
            if(ModelState.IsValid)
            {
                Articulo miArticulo = new Articulo();

                miArticulo.Codigo = articulo.Codigo;
                miArticulo.Descripcion = articulo.Descripcion;
                
                context.Articulos.Add(miArticulo);
                context.SaveChanges();

                if(submit == "Guardar")
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewData["mensaje"] = $"El artículo {miArticulo.Codigo} - {miArticulo.Descripcion} se agregó correctamente.";
                    return RedirectToAction(nameof(NuevoArticulo));
                }
                
            }
            return View(articulo);
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
