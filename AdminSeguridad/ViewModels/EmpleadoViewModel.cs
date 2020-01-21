using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//aqui esta el entityFramework
using AdminSeguridad.Models;

namespace AdminSeguridad.ViewModels
{
    public class EmpleadoViewModel
    {
        public int Id { get; set; }
        public string StrNombre { get; set; }
        public string StrApellidoPaterno { get; set; }
        public string StrApellidoMaterno { get; set; }
        public string DteFechaIngreso { get; set; }
        public Usuario usuario { get; set; }
        public HorarioLaboral HorarioLaboral { get; set; }


    }
}