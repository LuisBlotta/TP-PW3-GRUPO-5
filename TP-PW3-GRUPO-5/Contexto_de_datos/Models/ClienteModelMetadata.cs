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
        [RegularExpression(@"^([0-9])*$", ErrorMessage = "Ingrese un numero entero valido")]
        public int? Numero { get; set; }

        [Required(ErrorMessage = "Ingrese un nombre de cliente")]
        [StringLength(200, ErrorMessage = "Excede la cantidad válida de caracteres")]

        public string Nombre { get; set; }

        [EmailAddress(ErrorMessage = "Ingrese una direccion de email valida")]
        [StringLength(300, ErrorMessage = "Excede la cantidad válida de caracteres")]

        public string Email { get; set; }
        [StringLength(50, ErrorMessage = "Excede la cantidad válida de caracteres")]

        public string Telefono { get; set; }
        [StringLength(300, ErrorMessage = "Excede la cantidad válida de caracteres")]


        public string Direccion { get; set; }
        [StringLength(50, ErrorMessage = "Excede la cantidad válida de caracteres")]

        public string Cuit { get; set; }
    }
}
