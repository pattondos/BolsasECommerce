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
    public class SolicitudesOfertasController : Controller
    {
        private EntitiesBolsas1 db = new EntitiesBolsas1();

        // GET: SolicitudesOfertas
        public ActionResult Index()
        {
            var solicitudesOfertas = db.SolicitudesOfertas.Include(s => s.Productos);
            return View(solicitudesOfertas.ToList());
        }

        // GET: SolicitudesOfertas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudesOfertas solicitudesOfertas = db.SolicitudesOfertas.Find(id);
            if (solicitudesOfertas == null)
            {
                return HttpNotFound();
            }
            return View(solicitudesOfertas);
        }

        // GET: SolicitudesOfertas/Create
        public ActionResult Create()
        {
            ViewBag.Id_Producto = new SelectList(db.Productos, "id", "nombre");
            return View();
        }

        // POST: SolicitudesOfertas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Id_Producto,PrecioActual,Descripcion,Oferta,Status")] SolicitudesOfertas solicitudesOfertas)
        {
            if (ModelState.IsValid)
            {
                db.SolicitudesOfertas.Add(solicitudesOfertas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Producto = new SelectList(db.Productos, "id", "nombre", solicitudesOfertas.Id_Producto);
            return View(solicitudesOfertas);
        }

        // GET: SolicitudesOfertas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudesOfertas solicitudesOfertas = db.SolicitudesOfertas.Find(id);
            if (solicitudesOfertas == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Producto = new SelectList(db.Productos, "id", "nombre", solicitudesOfertas.Id_Producto);
            return View(solicitudesOfertas);
        }

        // POST: SolicitudesOfertas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Id_Producto,PrecioActual,Descripcion,Oferta,Status")] SolicitudesOfertas solicitudesOfertas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitudesOfertas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Producto = new SelectList(db.Productos, "id", "nombre", solicitudesOfertas.Id_Producto);
            return View(solicitudesOfertas);
        }

        // GET: SolicitudesOfertas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudesOfertas solicitudesOfertas = db.SolicitudesOfertas.Find(id);
            if (solicitudesOfertas == null)
            {
                return HttpNotFound();
            }
            return View(solicitudesOfertas);
        }

        // POST: SolicitudesOfertas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SolicitudesOfertas solicitudesOfertas = db.SolicitudesOfertas.Find(id);
            db.SolicitudesOfertas.Remove(solicitudesOfertas);
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
