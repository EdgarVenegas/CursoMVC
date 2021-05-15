//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CapaDatos.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class Formula
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Formula()
        {
            this.FormulaDetalle = new HashSet<FormulaDetalle>();
            this.Producto = new HashSet<Producto>();
        }
    
        public int IdFormula { get; set; }
        public string Nombre { get; set; }
        public Nullable<bool> Activo { get; set; }
        public int IdUsuarioCreo { get; set; }
        public System.DateTime FechaCreo { get; set; }
        public Nullable<int> IdUsuarioModifico { get; set; }
        public Nullable<System.DateTime> FechaModifico { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FormulaDetalle> FormulaDetalle { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Producto> Producto { get; set; }
    }
}