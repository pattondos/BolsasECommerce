using Final_Ecommerce.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Ecommerce.Models
{
    public class DetalleCompraModel
    {
        public Ventas venta { get; set; }
        public List<CarritoModel> products { get; set; }

        public DetalleCompraModel()
        {
            venta = new Ventas();
            products = new List<CarritoModel>();
        }

    }
}