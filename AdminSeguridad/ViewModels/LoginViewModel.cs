using System;
using System.Collections.Generic;
//cargamos la libreria de anotaciones para las validaciones
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminSeguridad.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Correo:")]
        public string Email { get; set; }
        [Display(Name = "Clave de Acceso:")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Recordarme")]
        public bool RememberMe { get; set; }
    }
}