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
    using System.ComponentModel.DataAnnotations;

    public partial class SolicitudesDevoluciones
    {
        [Required]
        [Display(Name = "* No. de Orden:")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "* Cliente solicitante:")]
        public Nullable<int> Id_Cliente { get; set; }

        [Required]
        [Display(Name = "* Producto:")]
        public Nullable<int> Id_Producto { get; set; }

        [Required]
        [Display(Name = "* Precio de venta:")]
        public double PrecioVenta { get; set; }

        [Required]
        [Display(Name = "* Capital a devolver:")]
        public double Devolucion { get; set; }
        [Required]
        [Display(Name = "* Motivo de devolución:")]
        public string Descripcion { get; set; }

        [Required]
        [Display(Name = "* Status:")]
        public int Status { get; set; }
    
        public virtual Productos Productos { get; set; }
        public virtual Usuarios Usuarios { get; set; }
    }
}
