
using Final_Ecommerce.Models;
using Final_Ecommerce.Models.DAL;
using Final_Ecommerce.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Final_Ecommerce.Controllers
{
    [Authorize]
    public class ComprasController : Controller
    {
        GenericUnitToWork _unitToWork = new GenericUnitToWork();

        // GET: Compras
        public ActionResult HistorialCompras()
        {
            Usuarios user = (Usuarios)Session["usr"];
            List<Ventas> ventasUsuarios = _unitToWork.GetRepositoryInstance<Ventas>().GetListParameter(i => i.id_usuario == user.id).ToList();
            return View(ventasUsuarios);
        }

        public ActionResult DetalleCompra(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Home", "Index");
            }

            DetalleCompraModel model = new DetalleCompraModel();
            model.venta = _unitToWork.GetRepositoryInstance<Ventas>().GetFirstOrDefaultByParameter(i => i.id == id);
            List<Detalle_Ventas> lista = _unitToWork.GetRepositoryInstance<Detalle_Ventas>().GetListParameter(i => i.id_venta == id).ToList();
            List<CarritoModel> pro = new List<CarritoModel>();

            foreach (Detalle_Ventas item in lista)
            {
                CarritoModel c = new CarritoModel();
                c.Cantidad = item.cantidad;
                c.Precio_venta = item.precio_venta;
                Productos p = _unitToWork.GetRepositoryInstance<Productos>().GetFirstOrDefaultByParameter(i => i.id == item.id_producto);
                c.Nombre = p.nombre;
                c.Img = p.img;
                c.Id_producto = p.id;

                pro.Add(c);
            }

            model.products = pro;

            return View(model);
        }

    }
}