using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Final_Ecommerce.Models
{
    public class CarritoModel
    {
        public string Img { get; set; }
        public string Nombre { get; set; }
        public int Id_producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio_venta { get; set; }
    }

    public class AddProductoModel
    {
        public int Cantidad { get; set; }
    }

}