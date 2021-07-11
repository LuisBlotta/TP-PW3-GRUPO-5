using Clases_auxiliares;
using Contexto_de_datos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios;
using Servicios.Session;
using System.Text.Json;

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
            if (sessionManager.EstaLogueado())
            {
                return JsonSerializer.Serialize(pedidoServicio.ObtenerInfoPedidos(busquedaPedido.IdCliente,busquedaPedido.IdEstado));
            }
            return "No tiene permisos para acceder al sitio";
        }
        [Route("Guardar")]
        [HttpPost]
        public string Guardar(PedidoNuevoFiltro pedido)
        {
            if (sessionManager.EstaLogueado())
            {
                if(pedidoServicio.AltaPedidoPorAPI(pedido))
                {
                    int nroPedido = pedidoServicio.ObtenerNumeroPedido() - 1;
                    return JsonSerializer.Serialize(new MensajeJSON { Mensaje = $"El pedido {nroPedido} se guardo con exito." });

                }
                return JsonSerializer.Serialize(new MensajeJSON { Mensaje = "No se pudo guardar el pedido." });

            }
            return JsonSerializer.Serialize(new MensajeJSON { Mensaje = "No tiene permisos para acceder al sitio." });

        }
    }
}