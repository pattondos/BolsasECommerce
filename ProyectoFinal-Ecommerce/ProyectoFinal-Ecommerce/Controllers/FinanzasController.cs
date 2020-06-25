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
        private EntitiesPreciosNuevos db1 = new EntitiesPreciosNuevos();
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
////////////////////////////////AQUI VES LAS ACTUALIZACIONES DIVERSAS//////////////////////////////////////////////////////////////
       

        // GET: SolicitudesCambioPrecios
        public ActionResult VerCambiosPreciosEn()
        {
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 2 || ((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 6)
            {
                var solicitudesCambioPrecio = db1.SolicitudesCambioPrecio.Include(s => s.CambioPrecioVenta).Include(s => s.Productos);
                return View(solicitudesCambioPrecio.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult VerCambiosPreciosAp()
        {
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 2 || ((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 6)
            {
                var solicitudesCambioPrecio = db1.SolicitudesCambioPrecio.Include(s => s.CambioPrecioVenta).Include(s => s.Productos);
                return View(solicitudesCambioPrecio.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult VerCambiosPreciosRec()
        {
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 2 || ((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 6)
            {
                var solicitudesCambioPrecio = db1.SolicitudesCambioPrecio.Include(s => s.CambioPrecioVenta).Include(s => s.Productos);
                return View(solicitudesCambioPrecio.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET
        public ActionResult DetailPrecio(int? id)
        {
            
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                SolicitudesCambioPrecio solicitudesCambioPrecio = db1.SolicitudesCambioPrecio.Find(id);
                if (solicitudesCambioPrecio == null)
                {
                    return HttpNotFound();
                }
                return View(solicitudesCambioPrecio);
 
        }

        [HttpPost]
        public ActionResult EditarEstadoPrecio(int id, int status)
        {
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 2 || ((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 6)
            {
                SolicitudesCambioPrecio solicitudes = db1.SolicitudesCambioPrecio.Find(id);

                solicitudes.Status = status;

                db1.SaveChanges();
                if (solicitudes.Status == 2)
                {
                    return RedirectToAction("VerCambiosPreciosEn", "Finanzas");
                }
                else {
                    if (solicitudes.Status == 3)
                    {
                        return RedirectToAction("VerCambiosPreciosRec", "Finanzas");
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }
//////////// Este edita motivos cuando se rechaza
        // GET:
        public ActionResult EditMotivoAc(int? id)
        {
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 2 || ((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 6)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                SolicitudesCambioPrecio solicitudesCambioPrecio = db1.SolicitudesCambioPrecio.Find(id);
                if (solicitudesCambioPrecio == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Id_TipoCambio = new SelectList(db1.CambioPrecioVenta, "Id", "TipoCambio", solicitudesCambioPrecio.Id_TipoCambio);
                ViewBag.Id_Producto = new SelectList(db1.Productos, "id", "nombre", solicitudesCambioPrecio.Id_Producto);
                return View(solicitudesCambioPrecio);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

       

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMotivoAc([Bind(Include = "Id,Id_Producto,PrecioActual,Id_TipoCambio,NuevoPrecio,Descuento,Motivo,Status,FechaActualizacion")] SolicitudesCambioPrecio solicitudesCambioPrecio)
        {
            if (ModelState.IsValid)
            {
                db1.Entry(solicitudesCambioPrecio).State = EntityState.Modified;
                db1.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_TipoCambio = new SelectList(db1.CambioPrecioVenta, "Id", "TipoCambio", solicitudesCambioPrecio.Id_TipoCambio);
            ViewBag.Id_Producto = new SelectList(db1.Productos, "id", "nombre", solicitudesCambioPrecio.Id_Producto);
            return View(solicitudesCambioPrecio);
        }

///////////////////////////AQUI VES LO DE LAS OFERTAS////////////////////////////////////////////////////////////////// 
        public ActionResult VerCambiosOfertasEn()
        {
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 2 || ((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 6)
            {
                var solicitudesCambioPrecio = db1.SolicitudesCambioPrecio.Include(s => s.CambioPrecioVenta).Include(s => s.Productos);
                return View(solicitudesCambioPrecio.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult VerCambiosOfertasAp()
        {
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 2 || ((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 6)
            {
                var solicitudesCambioPrecio = db1.SolicitudesCambioPrecio.Include(s => s.CambioPrecioVenta).Include(s => s.Productos);
                return View(solicitudesCambioPrecio.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult VerCambiosOfertasRec()
        {
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 2 || ((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 6)
            {
                var solicitudesCambioPrecio = db1.SolicitudesCambioPrecio.Include(s => s.CambioPrecioVenta).Include(s => s.Productos);
                return View(solicitudesCambioPrecio.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult EditaEstadoOferta(int id, int status)
        {
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 2 || ((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 6)
            {
                SolicitudesCambioPrecio solicitudes = db1.SolicitudesCambioPrecio.Find(id);

                solicitudes.Status = status;

                db1.SaveChanges();
                if (solicitudes.Status == 2)
                {
                    return RedirectToAction("VerCambiosOfertasEn", "Finanzas");
                }
                else
                {
                    if (solicitudes.Status == 3)
                    {
                        return RedirectToAction("VerCambiosPreciosRec", "Finanzas");
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

//////////// Este edita motivos cuando se rechaza
        // GET:
        public ActionResult EditOfer(int? id)
        {
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 2 || ((ProyectoFinal_Ecommerce.Models.Usuarios)Session["admin"]).role_id == 6)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                SolicitudesCambioPrecio solicitudesCambioPrecio = db1.SolicitudesCambioPrecio.Find(id);
                if (solicitudesCambioPrecio == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Id_TipoCambio = new SelectList(db1.CambioPrecioVenta, "Id", "TipoCambio", solicitudesCambioPrecio.Id_TipoCambio);
                ViewBag.Id_Producto = new SelectList(db1.Productos, "id", "nombre", solicitudesCambioPrecio.Id_Producto);
                return View(solicitudesCambioPrecio);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }



        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOfer([Bind(Include = "Id,Id_Producto,PrecioActual,Id_TipoCambio,NuevoPrecio,Descuento,Motivo,Status,FechaActualizacion")] SolicitudesCambioPrecio solicitudesCambioPrecio)
        {
            if (ModelState.IsValid)
            {
                db1.Entry(solicitudesCambioPrecio).State = EntityState.Modified;
                db1.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_TipoCambio = new SelectList(db1.CambioPrecioVenta, "Id", "TipoCambio", solicitudesCambioPrecio.Id_TipoCambio);
            ViewBag.Id_Producto = new SelectList(db1.Productos, "id", "nombre", solicitudesCambioPrecio.Id_Producto);
            return View(solicitudesCambioPrecio);
        }

 /////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
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