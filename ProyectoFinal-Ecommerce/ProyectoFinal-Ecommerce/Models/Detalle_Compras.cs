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
    
    public partial class Detalle_Compras
    {
        public int id { get; set; }
        public int id_producto { get; set; }
        public int id_compra { get; set; }
        public int cantidad { get; set; }
        public decimal precio_compra { get; set; }
        public decimal precio_venta { get; set; }
        public int stat { get; set; }
    }
}
