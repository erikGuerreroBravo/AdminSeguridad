﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminSeguridad.ViewModels
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string StrNombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
    }
}