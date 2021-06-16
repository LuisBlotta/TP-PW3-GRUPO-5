using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contexto_de_datos.Models
{
    [ModelMetadataType(typeof(ClienteModelMetadata))]
    public partial class Cliente
    {

    }
    public class ClienteModelMetadata
    {
        [RegularExpression(@"^\d$", ErrorMessage = "Ingrese un numero entero valido")]
        public int? Numero { get; set; }

        [Required(ErrorMessage = "Ingrese un nombre de cliente")]
        public string Nombre { get; set; }

        [EmailAddress(ErrorMessage = "Ingrese una direccion de email valida")]
        public string Email { get; set; }
    }
}
