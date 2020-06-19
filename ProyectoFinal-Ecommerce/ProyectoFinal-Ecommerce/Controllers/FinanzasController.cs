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

        // GET: SolicitudesCompras/Edit/5
        public ActionResult EditarEstado(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudesCompras solicitudesCompras = db.SolicitudesCompras.Find(id);
            if (solicitudesCompras == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Proveedor = new SelectList(db.Proveedores, "id", "razon_social", solicitudesCompras.Id_Proveedor);
            return View(solicitudesCompras);
        }

        // POST: SolicitudesCompras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarEstado([Bind(Include = "Id,NuevoProducto,Cantidad,Id_Proveedor,Descripcion,Presupuesto,Status")] SolicitudesCompras solicitudesCompras)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitudesCompras).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Proveedor = new SelectList(db.Proveedores, "id", "razon_social", solicitudesCompras.Id_Proveedor);
            return View(solicitudesCompras);
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
    }
}