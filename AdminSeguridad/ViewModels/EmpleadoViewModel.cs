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
        public int IdTurno { get; set; }
        public int IdRoles { get; set; }
        public string Rol { get; set; }
        public string Turno { get; set; }

        public UsuarioViewModel UsuarioViewModel { get; set; }
        public HorarioLaboralViewModel HorarioLaboralViewModel { get; set; }


    }
}