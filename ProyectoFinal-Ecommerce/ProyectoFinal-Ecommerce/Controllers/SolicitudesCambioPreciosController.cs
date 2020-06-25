using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoFinal_Ecommerce.Models;

namespace ProyectoFinal_Ecommerce.Controllers
{
    public class SolicitudesCambioPreciosController : Controller
    {
        private EntitiesPreciosNuevos db = new EntitiesPreciosNuevos();

        // GET: SolicitudesCambioPrecios
        public ActionResult Index()
        {
            var solicitudesCambioPrecio = db.SolicitudesCambioPrecio.Include(s => s.CambioPrecioVenta).Include(s => s.Productos);
            return View(solicitudesCambioPrecio.ToList());
        }

        // GET: SolicitudesCambioPrecios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudesCambioPrecio solicitudesCambioPrecio = db.SolicitudesCambioPrecio.Find(id);
            if (solicitudesCambioPrecio == null)
            {
                return HttpNotFound();
            }
            return View(solicitudesCambioPrecio);
        }

        // GET: SolicitudesCambioPrecios/Create
        public ActionResult Create()
        {
            ViewBag.Id_TipoCambio = new SelectList(db.CambioPrecioVenta, "Id", "TipoCambio");
            ViewBag.Id_Producto = new SelectList(db.Productos, "id", "nombre");
            return View();
        }

        // POST: SolicitudesCambioPrecios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Id_Producto,PrecioActual,Id_TipoCambio,NuevoPrecio,Descuento,Motivo,Status,FechaActualizacion")] SolicitudesCambioPrecio solicitudesCambioPrecio)
        {
            if (ModelState.IsValid)
            {
                db.SolicitudesCambioPrecio.Add(solicitudesCambioPrecio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_TipoCambio = new SelectList(db.CambioPrecioVenta, "Id", "TipoCambio", solicitudesCambioPrecio.Id_TipoCambio);
            ViewBag.Id_Producto = new SelectList(db.Productos, "id", "nombre", solicitudesCambioPrecio.Id_Producto);
            return View(solicitudesCambioPrecio);
        }

        // GET: SolicitudesCambioPrecios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudesCambioPrecio solicitudesCambioPrecio = db.SolicitudesCambioPrecio.Find(id);
            if (solicitudesCambioPrecio == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_TipoCambio = new SelectList(db.CambioPrecioVenta, "Id", "TipoCambio", solicitudesCambioPrecio.Id_TipoCambio);
            ViewBag.Id_Producto = new SelectList(db.Productos, "id", "nombre", solicitudesCambioPrecio.Id_Producto);
            return View(solicitudesCambioPrecio);
        }

        // POST: SolicitudesCambioPrecios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Id_Producto,PrecioActual,Id_TipoCambio,NuevoPrecio,Descuento,Motivo,Status,FechaActualizacion")] SolicitudesCambioPrecio solicitudesCambioPrecio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitudesCambioPrecio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_TipoCambio = new SelectList(db.CambioPrecioVenta, "Id", "TipoCambio", solicitudesCambioPrecio.Id_TipoCambio);
            ViewBag.Id_Producto = new SelectList(db.Productos, "id", "nombre", solicitudesCambioPrecio.Id_Producto);
            return View(solicitudesCambioPrecio);
        }

        // GET: SolicitudesCambioPrecios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudesCambioPrecio solicitudesCambioPrecio = db.SolicitudesCambioPrecio.Find(id);
            if (solicitudesCambioPrecio == null)
            {
                return HttpNotFound();
            }
            return View(solicitudesCambioPrecio);
        }

        // POST: SolicitudesCambioPrecios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SolicitudesCambioPrecio solicitudesCambioPrecio = db.SolicitudesCambioPrecio.Find(id);
            db.SolicitudesCambioPrecio.Remove(solicitudesCambioPrecio);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
