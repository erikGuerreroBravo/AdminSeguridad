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
    
    public partial class Turno
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Turno()
        {
            this.HorarioLaboral = new HashSet<HorarioLaboral>();
        }
    
        public int id { get; set; }
        public string strNombre { get; set; }
        public string strDescripcion { get; set; }
        public string dteHorarioInicio { get; set; }
        public string dteHoraTermino { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HorarioLaboral> HorarioLaboral { get; set; }
    }
}
