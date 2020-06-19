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
    public class SolicitudesVentasActualizarsController : Controller
    {
        private EntitiesBolsas1 db = new EntitiesBolsas1();

        // GET: SolicitudesVentasActualizars
        public ActionResult Index()
        {
            var solicitudesVentasActualizar = db.SolicitudesVentasActualizar.Include(s => s.Productos);
            return View(solicitudesVentasActualizar.ToList());
        }

        // GET: SolicitudesVentasActualizars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudesVentasActualizar solicitudesVentasActualizar = db.SolicitudesVentasActualizar.Find(id);
            if (solicitudesVentasActualizar == null)
            {
                return HttpNotFound();
            }
            return View(solicitudesVentasActualizar);
        }

        // GET: SolicitudesVentasActualizars/Create
        public ActionResult Create()
        {
            ViewBag.Id_Producto = new SelectList(db.Productos, "id", "nombre");
            return View();
        }

        // POST: SolicitudesVentasActualizars/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Id_Producto,Stock,PrecioCompra,PrecioActual,NuevoPrecio,Status")] SolicitudesVentasActualizar solicitudesVentasActualizar)
        {
            if (ModelState.IsValid)
            {
                db.SolicitudesVentasActualizar.Add(solicitudesVentasActualizar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Producto = new SelectList(db.Productos, "id", "nombre", solicitudesVentasActualizar.Id_Producto);
            return View(solicitudesVentasActualizar);
        }

        // GET: SolicitudesVentasActualizars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudesVentasActualizar solicitudesVentasActualizar = db.SolicitudesVentasActualizar.Find(id);
            if (solicitudesVentasActualizar == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Producto = new SelectList(db.Productos, "id", "nombre", solicitudesVentasActualizar.Id_Producto);
            return View(solicitudesVentasActualizar);
        }

        // POST: SolicitudesVentasActualizars/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Id_Producto,Stock,PrecioCompra,PrecioActual,NuevoPrecio,Status")] SolicitudesVentasActualizar solicitudesVentasActualizar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitudesVentasActualizar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Producto = new SelectList(db.Productos, "id", "nombre", solicitudesVentasActualizar.Id_Producto);
            return View(solicitudesVentasActualizar);
        }

        // GET: SolicitudesVentasActualizars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudesVentasActualizar solicitudesVentasActualizar = db.SolicitudesVentasActualizar.Find(id);
            if (solicitudesVentasActualizar == null)
            {
                return HttpNotFound();
            }
            return View(solicitudesVentasActualizar);
        }

        // POST: SolicitudesVentasActualizars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SolicitudesVentasActualizar solicitudesVentasActualizar = db.SolicitudesVentasActualizar.Find(id);
            db.SolicitudesVentasActualizar.Remove(solicitudesVentasActualizar);
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
