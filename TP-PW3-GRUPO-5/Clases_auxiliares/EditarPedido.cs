using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases_auxiliares
{
    public class EditarPedido
    {
        public int IdPedido { get; set; }
        public List<ArticuloCantidad> Articulos { get; set; }
        public int EstadoPedido { get; set; }

        public string Comentarios { get; set; }
    }
}
