using Clases_auxiliares;
using Contexto_de_datos.Models;
using System.Collections.Generic;

namespace Servicios
{
    public interface IClienteServicio
    {
        public List<Cliente> ObtenerClientes (ClienteFiltro clienteFiltro = null);

        public List<string> ObtenerSelectClientes();
    }
}