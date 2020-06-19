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
    public class CarritoController : Controller
    {
        public GenericUnitToWork _unitOfWork = new GenericUnitToWork();

        // GET: Carrito
        [AllowAnonymous]
        public ActionResult Carrito()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult AgregarProducto(int id)
        {
            CarritoModel item = new CarritoModel();
            Productos p = _unitOfWork.GetRepositoryInstance<Productos>().GetFirstOrDefaultByParameter(i => i.id == id);
            if (Session["carrito"] == null)
            {
                List<CarritoModel> carrito = new List<CarritoModel>();
                if (p != null)
                {
                    item.Id_producto = p.id;
                    item.Img = p.img;
                    item.Precio_venta = p.precio_venta;
                    item.Cantidad = 1;
                    item.Nombre = p.nombre;
                }
                carrito.Add(item);
                Session["carrito"] = carrito;
            }
            else
            {
                List<CarritoModel> carrito = (List<CarritoModel>)Session["carrito"];
                int index = ExisteProducto(id);
                if (index != -1)
                {
                    if (carrito[index].Cantidad + 1 > p.cantidad)
                    {
                        Session["error"] = "No se puede agregar al carrito, la tienda no cuenta con suficiente cantidad de este producto.";
                        return RedirectToAction("InfoProducto", "Productos", new { id });
                    }
                    carrito[index].Cantidad++;
                }
                else
                {
                    //Productos p = _unitOfWork.GetRepositoryInstance<Productos>().GetFirstOrDefaultByParameter(i => i.id == id);
                    if (p != null)
                    {
                        item.Id_producto = p.id;
                        item.Img = p.img;
                        item.Precio_venta = p.precio_venta;
                        item.Cantidad = 1;
                        item.Nombre = p.nombre;
                    }
                    carrito.Add(item);
                }
                Session["carro"] = carrito;
            }
            //int n = p.cantidad - 1;
            //p.cantidad = n;
            //_unitOfWork.GetRepositoryInstance<Productos>().Update(p);
            Session["success"] = "Se ha agregado al carrito exitosamente.";
            return RedirectToAction("InfoProducto", "Productos", new { id });
        }

        public int ExisteProducto(int id)
        {
            List<CarritoModel> carrito = (List<CarritoModel>)Session["carrito"];
            for (var i = 0; i < carrito.Count(); i++)
            {
                if (carrito[i].Id_producto == id) return i;
            }
            return -1;
        }

        [AllowAnonymous]
        public ActionResult DropProducto(int id)
        {
            List<CarritoModel> carrito = (List<CarritoModel>)Session["carrito"];
            int index = ExisteProducto(id);
            //CarritoModel m = carrito.FindLast(i => i.Id_producto == id);
            //Productos product = _unitOfWork.GetRepositoryInstance<Productos>().GetFirstOrDefaultByParameter(i => i.id == id);
            //int n = product.cantidad + m.Cantidad;
            //product.cantidad = n;
            //_unitOfWork.GetRepositoryInstance<Productos>().Update(product);
            carrito.RemoveAt(index);

            Session["carrito"] = carrito.Count() > 0 ? carrito : null;
            //if (carrito.Count() > 0)
            //{
            //    Session["carrito"] = carrito;
            //}
            //else
            //{
            //    Session["carrito"] = null;
            //}
            return RedirectToAction("Carrito");
        }

    }
}