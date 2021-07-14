using Clases_auxiliares;
using Contexto_de_datos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios;
using Servicios.Login;
using Servicios.Session;
using System.Text.Json;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class ClientesController : ControllerBase
    {
        private _20211CTPContext context;
        private ILoginServicio loginServicio;
        private IClienteServicio clienteServicio;
        private ISessionManager sessionManager;

        public ClientesController(_20211CTPContext ctx, IHttpContextAccessor _httpContextAccessor)
        {
            context = ctx;
            loginServicio = new LoginServicio(context, _httpContextAccessor);
            clienteServicio = new ClienteServicio(context, _httpContextAccessor);
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
                    return JsonSerializer.Serialize(clienteServicio.ObtenerClientes());
                }
            }
            return JsonSerializer.Serialize(new MensajeJSON { Mensaje = "No tiene permisos para acceder al sitio." });

        }
        [Route("filtrar")]
        [HttpPost]
        public string Filtrar([FromBody] string Nombre)
        {
            if (sessionManager.EstaLogueado())
            {
                if (sessionManager.EsAdmin())
                {
                    return JsonSerializer.Serialize(clienteServicio.FiltrarPorNombre(Nombre));
                }
            }
            return JsonSerializer.Serialize(new MensajeJSON { Mensaje = "No tiene permisos para acceder al sitio." });


        }
    }
}