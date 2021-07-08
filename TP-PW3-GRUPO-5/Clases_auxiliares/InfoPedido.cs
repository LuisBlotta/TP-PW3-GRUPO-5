using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases_auxiliares
{
    public class InfoPedido
    {
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }
        public string Estado { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public UsuarioInfoPedido ModificadoPor { get; set; }
        public List<ArticuloCantidad> Articulos { get; set; }
    }
}
