using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal_Ecommerce.Models
{
    public class DetalleCompraModel
    {
        public Compras compra { get; set; }
        public List<ProductoCompraModel> detalles { get; set; }
        public Proveedores proveedor { get; set; }

        public DetalleCompraModel()
        {
            compra = new Compras();
            detalles = new List<ProductoCompraModel>();
            proveedor = new Proveedores();
        }

    }

    public class ProductoCompraModel
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public decimal precio_compra { get; set; }
        public decimal precio_venta { get; set; }
        public int cantidad { get; set; }
        public int id_proveedor { get; set; }
    }
}