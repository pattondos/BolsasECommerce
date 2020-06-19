using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal_Ecommerce.Models
{
    public class DetalleVentaModel
    {

        public Ventas venta { get; set; }
        public List<ProductoVentaModel> products { get; set; }
        public Usuarios usuario { get; set; }

        public DetalleVentaModel()
        {
            venta = new Ventas();
            products = new List<ProductoVentaModel>();
            usuario = new Usuarios();
        }
    }

    public class ProductoVentaModel
    {
        public string nombre { get; set; }
        public int cantidad { get; set; }
        public decimal precio_venta { get; set; }
        public int id { get; set; }
    }

}