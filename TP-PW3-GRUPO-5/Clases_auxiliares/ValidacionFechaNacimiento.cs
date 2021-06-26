using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases_auxiliares
{
    public class ValidacionFechaNacimiento : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                DateTime fecha = (DateTime)value;

                return fecha > DateTime.Now ? false : true;
            }
            return false;
        }
    }
}
