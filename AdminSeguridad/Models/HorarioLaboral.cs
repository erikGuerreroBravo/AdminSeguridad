//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdminSeguridad.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HorarioLaboral
    {
        public int id { get; set; }
        public Nullable<int> idEmpleado { get; set; }
        public Nullable<int> idTurno { get; set; }
        public Nullable<System.DateTime> dteHorarioEntrada { get; set; }
        public Nullable<System.DateTime> dteHorarioSalida { get; set; }
        public Nullable<System.DateTime> dteFechaActual { get; set; }
    
        public virtual Empleado Empleado { get; set; }
        public virtual Turno Turno { get; set; }
    }
}
