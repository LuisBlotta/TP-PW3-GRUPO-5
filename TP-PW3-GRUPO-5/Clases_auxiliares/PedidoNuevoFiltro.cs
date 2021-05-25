using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases_auxiliares
{
    public class PedidoNuevoFiltro
    {
        public string NumeroCliente { get; set; }
        public List<object> Articulos { get; set; }

        public string Accion { get; set; }
        public string Comentarios { get; set; }
    }
}
