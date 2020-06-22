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
    public class VentasController : Controller
    {

        GenericUnitToWork _unitToWork = new GenericUnitToWork();

        // GET: Ventas
        public ActionResult AllVentas()
        {
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["mom"]).role_id == 2 || ((ProyectoFinal_Ecommerce.Models.Usuarios)Session["mom"]).role_id == 3)
            {
                List<Ventas> ventas = _unitToWork.GetRepositoryInstance<Ventas>().GetAllRecords().ToList();
                return View(ventas);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DetalleVenta(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["mom"]).role_id == 2 || ((ProyectoFinal_Ecommerce.Models.Usuarios)Session["mom"]).role_id == 3)
            {
                DetalleVentaModel model = new DetalleVentaModel();
                model.venta = _unitToWork.GetRepositoryInstance<Ventas>().GetFirstorDefaultByParameter(i => i.id == id);

                Usuarios usuario = _unitToWork.GetRepositoryInstance<Usuarios>().GetFirstorDefaultByParameter(i => i.id == model.venta.id_usuario);
                usuario.pass = "";
                usuario.fecha_nacimiento = null;
                usuario.telefono = "";

                model.usuario = usuario;

                List<Detalle_Ventas> lista = _unitToWork.GetRepositoryInstance<Detalle_Ventas>().GetListParameter(i => i.id_venta == id).ToList();
                List<ProductoVentaModel> pro = new List<ProductoVentaModel>();

                foreach (Detalle_Ventas dv in lista)
                {
                    ProductoVentaModel n = new ProductoVentaModel();
                    n.cantidad = dv.cantidad;
                    n.id = dv.id_producto;
                    n.precio_venta = dv.precio_venta;
                    n.nombre = _unitToWork.GetRepositoryInstance<Productos>().GetFirstorDefaultByParameter(i => i.id == dv.id_producto).nombre;
                    pro.Add(n);
                }

                model.products = pro;

                return View(model);
            }
            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public ActionResult updateStatus(int id, int status)
        {
            Ventas v = _unitToWork.GetRepositoryInstance<Ventas>().GetFirstorDefaultByParameter(i => i.id == id);
            v.stat = status;
            _unitToWork.GetRepositoryInstance<Ventas>().Update(v);
             return RedirectToAction("AllVentas", "Ventas");
             
            //return RedirectToAction("AllVentas", "Ventas");           
        }

    }
}