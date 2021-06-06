using Clases_auxiliares;
using Contexto_de_datos.Models;
using System.Collections.Generic;

namespace Servicios
{
    public interface IArticuloServicio
    {
        public List<Articulo> ObtenerArticulos(ArticuloFiltro articuloFiltro = null);
    }
}