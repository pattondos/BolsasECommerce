using Final_Ecommerce.Repository;
using Final_Ecommerce.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Final_Ecommerce.Models
{
    [Authorize]
    public class ProductosController : Controller
    {
        public GenericUnitToWork _unitOfWork = new GenericUnitToWork();

        // GET: Productos
        [AllowAnonymous]
        public ActionResult Productos()
        {
            List<Productos> all = _unitOfWork.GetRepositoryInstance<Productos>().GetAllRecords().ToList();
            return View(all);
        }

        [AllowAnonymous]
        public ActionResult AllProductos()
        {
            List<Productos> all = _unitOfWork.GetRepositoryInstance<Productos>().GetAllRecords().ToList();
            return View(all);
        }

        [AllowAnonymous]
        public ActionResult InfoProducto(int? id)
        {
            if (id != null)
            {
                Productos producto = _unitOfWork.GetRepositoryInstance<Productos>().GetFirstOrDefaultByParameter(i => i.id == id);
                //ProductoVenta productoVenta = new ProductoVenta();
                //productoVenta.Producto = producto;
                return View(producto);
            }
            return RedirectToAction("AllProductos");
        }

    }
}