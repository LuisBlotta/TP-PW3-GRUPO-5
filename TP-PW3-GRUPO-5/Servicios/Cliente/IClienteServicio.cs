using Clases_auxiliares;
using Entidades;
using System.Collections.Generic;

namespace Servicios
{
    public interface IClienteServicio
    {
        public List<Cliente> ObtenerClientes (ClienteFiltro clienteFiltro = null);

        public List<string> ObtenerSelectClientes();
    }
}