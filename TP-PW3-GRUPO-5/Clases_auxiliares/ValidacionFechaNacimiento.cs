using System;
using System.ComponentModel.DataAnnotations;

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
