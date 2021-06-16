﻿using Clases_auxiliares;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contexto_de_datos.Models
{
    [ModelMetadataType(typeof(UsuarioModelMetadata))]
    public partial class Usuario
    {

    }
    public class UsuarioModelMetadata
    {
        [Required(ErrorMessage = "Ingrese un email.")]
        [EmailAddress(ErrorMessage = "Ingrese un email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ingrese una contraseña.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Ingrese un nombre valido.")]
        public string Nombre { get; set; }

        [ValidacionFechaNacimiento(ErrorMessage = "Ingrese una fecha de nacimiento menor a la actual.")]
        public DateTime? FechaNacimiento { get; set; }
    }
}
