using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminSeguridad.ViewModels
{
    public class HorarioLaboralViewModel
    {
        public int Id { get; set; }
        public int IdEmpleado { get; set; }
        public int IdTurno { get; set; }
        public DateTime DteHorarioEntrada{ get; set; }
        public DateTime DteHorarioSalida { get; set; }
        public DateTime DteFechaActual { get; set; }

    }
}