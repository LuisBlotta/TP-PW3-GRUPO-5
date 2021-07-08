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
    public class PedidosController : ControllerBase
    {
        private _20211CTPContext context;
        private IPedidoServicio pedidoServicio;
        private ISessionManager sessionManager;

        public PedidosController(_20211CTPContext ctx, IHttpContextAccessor _httpContextAccessor)
        {
            context = ctx;
            pedidoServicio = new PedidoServicio(context, _httpContextAccessor);
            sessionManager = new SessionManager(_httpContextAccessor);

        }
        [Route("Buscar")]
        [HttpPost]
        public string Buscar(BusquedaPedido busquedaPedido)
        {
            if (sessionManager.EsAdmin())
            {
                return JsonSerializer.Serialize(pedidoServicio.ObtenerInfoPedidos(busquedaPedido.IdCliente,busquedaPedido.IdEstado));
            }
            return "No tiene permisos para acceder al sitio";
        }
    }
}