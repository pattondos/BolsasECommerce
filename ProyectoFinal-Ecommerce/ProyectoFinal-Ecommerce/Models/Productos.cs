//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoFinal_Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Productos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Productos()
        {
            this.SolicitudesVentasActualizar = new HashSet<SolicitudesVentasActualizar>();
            this.SolicitudesDevoluciones = new HashSet<SolicitudesDevoluciones>();
            this.SolicitudesOfertas = new HashSet<SolicitudesOfertas>();
        }
    
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int id_categoria { get; set; }
        public int cantidad { get; set; }
        public int id_proveedor { get; set; }
        public decimal precio_compra { get; set; }
        public decimal precio_venta { get; set; }
        public int stock { get; set; }
        public int stat { get; set; }
        public string img { get; set; }
        public Nullable<decimal> calificacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SolicitudesVentasActualizar> SolicitudesVentasActualizar { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SolicitudesDevoluciones> SolicitudesDevoluciones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SolicitudesOfertas> SolicitudesOfertas { get; set; }
    }
}