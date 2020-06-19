using ProyectoFinal_Ecommerce.Models;
using ProyectoFinal_Ecommerce.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal_Ecommerce.Controllers
{
    [Authorize]
    public class ComprasController : Controller
    {
        GenericUnitToWork _unitToWork = new GenericUnitToWork();
        // GET: Compras
        public ActionResult AllCompras()
        {
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["mom"]).role_id == 2 || ((ProyectoFinal_Ecommerce.Models.Usuarios)Session["mom"]).role_id == 4)
            {
                List<Compras> lista = _unitToWork.GetRepositoryInstance<Compras>().GetAllRecords().ToList();
                return View(lista);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DetalleCompra(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["mom"]).role_id == 2 || ((ProyectoFinal_Ecommerce.Models.Usuarios)Session["mom"]).role_id == 4)
            {

                DetalleCompraModel c = new DetalleCompraModel();
                c.compra = _unitToWork.GetRepositoryInstance<Compras>().GetFirstorDefaultByParameter(i => i.id == id);
                c.proveedor = _unitToWork.GetRepositoryInstance<Proveedores>().GetFirstorDefaultByParameter(i => i.id == c.compra.id_proveedor);

                List<Detalle_Compras> detalles = _unitToWork.GetRepositoryInstance<Detalle_Compras>().GetListParameter(i => i.id_compra == id).ToList();
                List<ProductoCompraModel> pros = new List<ProductoCompraModel>();

                foreach (Detalle_Compras detalle in detalles)
                {
                    ProductoCompraModel p = new ProductoCompraModel();
                    p.id = detalle.id_producto;
                    p.nombre = _unitToWork.GetRepositoryInstance<Productos>().GetFirstorDefaultByParameter(i => i.id == detalle.id_producto).nombre;
                    p.cantidad = detalle.cantidad;
                    p.precio_compra = detalle.precio_compra;
                    pros.Add(p);
                }

                c.detalles = pros;

                return View(c);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult GenerarAllCompras()
        {
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["mom"]).role_id == 2 || ((ProyectoFinal_Ecommerce.Models.Usuarios)Session["mom"]).role_id == 4)
            {
                List<Productos> pros = _unitToWork.GetRepositoryInstance<Productos>().GetListParameter(i => i.cantidad < i.stock).ToList();
                List<ProductoCompraModel> d = new List<ProductoCompraModel>();
                foreach (Productos item in pros)
                {
                    ProductoCompraModel det = new ProductoCompraModel
                    {
                        id_proveedor = item.id_proveedor,
                        id = item.id,
                        precio_compra = item.precio_compra,
                        cantidad = item.stock - item.cantidad + 10,
                        precio_venta = item.precio_venta,
                        nombre = item.nombre
                    };
                    d.Add(det);
                }

                Session["compra"] = d;
                return View("ComprasRealizar");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ComprasRealizar()
        {
            return View();
        }

        public ActionResult DropProducto(int id)
        {

            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["mom"]).role_id == 2 || ((ProyectoFinal_Ecommerce.Models.Usuarios)Session["mom"]).role_id == 4)
            {
                List<ProductoCompraModel> carrito = (List<ProductoCompraModel>)Session["compra"];
                int index = ExisteProducto(id);
                //CarritoModel m = carrito.FindLast(i => i.Id_producto == id);
                //Productos product = _unitOfWork.GetRepositoryInstance<Productos>().GetFirstOrDefaultByParameter(i => i.id == id);
                //int n = product.cantidad + m.Cantidad;
                //product.cantidad = n;
                //_unitOfWork.GetRepositoryInstance<Productos>().Update(product);
                carrito.RemoveAt(index);

                Session["compra"] = carrito.Count() > 0 ? carrito : null;
                //if (carrito.Count() > 0)
                //{
                //    Session["carrito"] = carrito;
                //}
                //else
                //{
                //    Session["carrito"] = null;
                //}
                return RedirectToAction("ComprasRealizar");
            }
            return RedirectToAction("Index", "Home");
        }

        public int ExisteProducto(int id)
        {
            List<ProductoCompraModel> carrito = (List<ProductoCompraModel>)Session["compra"];
            for (var i = 0; i < carrito.Count(); i++)
            {
                if (carrito[i].id == id) return i;
            }
            return -1;
        }

        public ActionResult ConfirmarCompra()
        {


            List<ProductoCompraModel> lista = (List<ProductoCompraModel>)Session["compra"];
            List<int> provs = new List<int>();

            foreach (ProductoCompraModel item in lista)
            {
                if (ExisteId(item.id_proveedor, provs) == -1)
                {
                    provs.Add(item.id_proveedor);
                }
            }

            foreach (int id in provs)
            {
                List<Detalle_Compras> detalle = new List<Detalle_Compras>();
                Compras c = _unitToWork.GetRepositoryInstance<Compras>().GetLastRecord();
                Compras compra = new Compras();
                if (c != null)
                {
                    compra.id = c.id + 1;
                }
                compra.fecha = DateTime.Today;
                compra.id_proveedor = id;
                compra.iva = 0;
                compra.stat = 1;

                foreach (ProductoCompraModel item in lista)
                {
                    if (item.id_proveedor == id)
                    {
                        Detalle_Compras det = new Detalle_Compras();

                        det.id_compra = compra.id;
                        det.id_producto = item.id;
                        det.precio_compra = item.precio_compra;
                        det.precio_venta = item.precio_venta;
                        det.stat = 1;
                        det.cantidad = item.cantidad;

                        detalle.Add(det);
                    }
                }

                compra.subtotal = detalle.Sum(i => i.precio_compra * i.cantidad);
                compra.total = compra.subtotal;

                _unitToWork.GetRepositoryInstance<Compras>().Add(compra);

                foreach (Detalle_Compras item in detalle)
                {
                    Detalle_Compras d = _unitToWork.GetRepositoryInstance<Detalle_Compras>().GetLastRecord();
                    if (d != null)
                    {
                        item.id = d.id + 1;
                    }
                    _unitToWork.GetRepositoryInstance<Detalle_Compras>().Add(item);
                }

            }
            Session["compra"] = null;
            return RedirectToAction("AllCompras");
        }

        public int ExisteId(int id, List<int> ids)
        {

            for (var i = 0; i < ids.Count(); i++)
            {
                if (ids[i] == id) return i;
            }
            return -1;
        }

    }
}