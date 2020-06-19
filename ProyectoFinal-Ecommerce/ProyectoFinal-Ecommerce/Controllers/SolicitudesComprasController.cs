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
    [Authorize]
    public class SolicitudesComprasController : Controller
    {
        private EntitiesBolsas1 db = new EntitiesBolsas1();

        // GET: SolicitudesCompras
        public ActionResult Index()
        {
            var solicitudesCompras = db.SolicitudesCompras.Include(s => s.Proveedores);
            return View(solicitudesCompras.ToList());
        }

        // GET: SolicitudesCompras/Details/5
        public ActionResult Details(int? id)
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
            return View(solicitudesCompras);
        }

        // GET: SolicitudesCompras/Create
        public ActionResult Create()
        {
            ViewBag.Id_Proveedor = new SelectList(db.Proveedores, "id", "razon_social");
            return View();
        }

        // POST: SolicitudesCompras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NuevoProducto,Cantidad,Id_Proveedor,Descripcion,Presupuesto,Status")] SolicitudesCompras solicitudesCompras)
        {
            if (ModelState.IsValid)
            {
                db.SolicitudesCompras.Add(solicitudesCompras);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Proveedor = new SelectList(db.Proveedores, "id", "razon_social", solicitudesCompras.Id_Proveedor);
            return View(solicitudesCompras);
        }

        // GET: SolicitudesCompras/Edit/5
        public ActionResult Edit(int? id)
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
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NuevoProducto,Cantidad,Id_Proveedor,Descripcion,Presupuesto,Status")] SolicitudesCompras solicitudesCompras)
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

        // GET: SolicitudesCompras/Delete/5
        public ActionResult Delete(int? id)
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
            return View(solicitudesCompras);
        }

        // POST: SolicitudesCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SolicitudesCompras solicitudesCompras = db.SolicitudesCompras.Find(id);
            db.SolicitudesCompras.Remove(solicitudesCompras);
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
