using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases_auxiliares
{
    public class PedidoNuevoFiltro
    {
        public int IdCliente { get; set; }
        public List<ArticuloCantidad> Articulos { get; set; }

        public string Accion { get; set; }
        public string Comentarios { get; set; }
    }
}
