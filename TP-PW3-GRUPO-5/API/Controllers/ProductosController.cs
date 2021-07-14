using Clases_auxiliares;
using Contexto_de_datos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios;
using Servicios.Login;
using Servicios.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class ProductosController : ControllerBase
    {
        private _20211CTPContext context;
        private IArticuloServicio articuloServicio;
        private ISessionManager sessionManager;

        public ProductosController(_20211CTPContext ctx, IHttpContextAccessor _httpContextAccessor)
        {
            context = ctx;
            articuloServicio = new ArticuloServicio(context, _httpContextAccessor);
            sessionManager = new SessionManager(_httpContextAccessor);

        }
        [Route("")]
        [HttpGet]
        public string Get()
        {
            if (sessionManager.EstaLogueado())
            {
                if (sessionManager.EsAdmin())
                {
                    return JsonSerializer.Serialize(articuloServicio.ObtenerArticulos());
                }
            }
            return JsonSerializer.Serialize(new MensajeJSON { Mensaje = "No tiene permisos para acceder al sitio." });

        }
        [Route("filtrar")]
        [HttpPost]
        public string Filtrar([FromBody] string Descripcion)
        {
            if (sessionManager.EstaLogueado())
            {
                if (sessionManager.EsAdmin())
                {
                    return JsonSerializer.Serialize(articuloServicio.FiltrarPorDescripcion(Descripcion));
                }
            }
            return JsonSerializer.Serialize(new MensajeJSON { Mensaje = "No tiene permisos para acceder al sitio." });


        }
    }
}