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
    
    public partial class SolicitudesDevoluciones
    {
        public int Id { get; set; }
        public Nullable<int> Id_Cliente { get; set; }
        public Nullable<int> Id_Producto { get; set; }
        public double PrecioVenta { get; set; }
        public double Devolucion { get; set; }
        public string Descripcion { get; set; }
        public int Status { get; set; }
        public Nullable<int> Id_Proveedor { get; set; }
        public Nullable<int> Id_Venta { get; set; }
    
        public virtual Productos Productos { get; set; }
        public virtual Usuarios Usuarios { get; set; }
        public virtual Proveedores Proveedores { get; set; }
    }
}
