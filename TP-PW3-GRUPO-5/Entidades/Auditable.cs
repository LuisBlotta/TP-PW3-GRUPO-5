using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public abstract class Auditable
    {
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public DateTime FechaBorrado { get; set; }
        public Usuario CreadoPor { get; set; }
        public Usuario ModificadoPor { get; set; }
        public Usuario BorradoPor { get; set; }

        public Auditable()
        {

        }
        public Auditable(DateTime fechacreacion,
            DateTime fechamodificacion,
            DateTime fechaborrado,
            Usuario creadopor,
            Usuario modificadopor,
            Usuario borradopor)
        {
            FechaCreacion = fechacreacion;
            FechaModificacion = fechamodificacion;
            FechaBorrado = fechaborrado;
            CreadoPor = creadopor;
            ModificadoPor = modificadopor;
            BorradoPor = borradopor;
        }

    }
}
