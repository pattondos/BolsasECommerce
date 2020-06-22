using ProyectoFinal_Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal_Ecommerce.Controllers
{
    [Authorize]
    public class FinanzasController : Controller
    {
        private EntitiesBolsas1 db = new EntitiesBolsas1();

        // GET: Finanzas
        public ActionResult Index()
        {
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 2 || ((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 6)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
/// 
/// Este apartado es para las compras de nuevos productos
/// 
///       
        // GET: Ver las solicitudes del departamento de compras
        public ActionResult VerSolicitudesCompras()
        {
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 2 || ((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 6)
            {
                var solicitudesCompras = db.SolicitudesCompras.Include(s => s.Proveedores);
                return View(solicitudesCompras.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult EditarEstadoCompras(int id, int status)
        {
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 2 || ((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 6)
            {
                SolicitudesCompras solicitudesCompras = db.SolicitudesCompras.Find(id);

                solicitudesCompras.Status = status;

                db.SaveChanges();

                return RedirectToAction("VerSolicitudesCompras", "Finanzas");
            }

            return RedirectToAction("Index", "Home");
        }

 /// 
 /// Este apartado es para las actualizaciones de precios
 ///
 /// 
     // GET: Ver las solicitudes de actualizaciones de precios
        public ActionResult VerSolicitudesActualizar()
        {
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 2 || ((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 6)
            {
                var solicitudesVentasActualizar = db.SolicitudesVentasActualizar.Include(s => s.Productos);
                return View(solicitudesVentasActualizar.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult EditarEstadoActualizar(int id, int status)
        {
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 2 || ((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 6)
            {
                
                SolicitudesVentasActualizar solicitudesVentas = db.SolicitudesVentasActualizar.Find(id);

                solicitudesVentas.Status = status;

                db.SaveChanges();

                return RedirectToAction("VerSolicitudesActualizar", "Finanzas");
            }

            return RedirectToAction("Index", "Home");
        }
/// 
/// Este apartado es para las devoluciones
/// 
///       
        // GET: Finanzas
        public ActionResult VerSolicitudesDevoluciones()
        {
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 2 || ((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 6)
            {
                var solicitudesDevoluciones = db.SolicitudesDevoluciones.Include(s => s.Productos).Include(s => s.Usuarios);
                return View(solicitudesDevoluciones.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult EditarEstadoDevolucion(int id, int status)
        {
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 2 || ((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 6)
            {
                
                SolicitudesDevoluciones solicitudesDevoluciones = db.SolicitudesDevoluciones.Find(id);

                solicitudesDevoluciones.Status = status;

                db.SaveChanges();

                return RedirectToAction("VerSolicitudesActualizar", "Finanzas");
            }

            return RedirectToAction("Index", "Home");
        }
/// 
/// Este apartado es para las ofertas de los productos
/// 
///        
        // GET: Finanzas
        public ActionResult VerSolicitudesOfertas()
        {
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 2 || ((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 6)
            {
                var solicitudesOfertas = db.SolicitudesOfertas.Include(s => s.Productos);
                return View(solicitudesOfertas.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult EditarEstadoOferta(int id, int status)
        {
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 2 || ((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 6)
            {

                SolicitudesOfertas solicitudesOfertas = db.SolicitudesOfertas.Find(id);

                solicitudesOfertas.Status = status;

                db.SaveChanges();

                return RedirectToAction("VerSolicitudesActualizar", "Finanzas");
            }

            return RedirectToAction("Index", "Home");
        }

    }
}