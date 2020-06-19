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
    public class SolicitudesDevolucionesController : Controller
    {
        
        private EntitiesBolsas1 db = new EntitiesBolsas1();

        // GET: SolicitudesDevoluciones
        public ActionResult Index()
        {
            var solicitudesDevoluciones = db.SolicitudesDevoluciones.Include(s => s.Productos).Include(s => s.Usuarios);
            return View(solicitudesDevoluciones.ToList());
        }

        // GET: SolicitudesDevoluciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudesDevoluciones solicitudesDevoluciones = db.SolicitudesDevoluciones.Find(id);
            if (solicitudesDevoluciones == null)
            {
                return HttpNotFound();
            }
            return View(solicitudesDevoluciones);
        }

        // GET: SolicitudesDevoluciones/Create
        public ActionResult Create()
        {
            ViewBag.Id_Producto = new SelectList(db.Productos, "id", "nombre");
            ViewBag.Id_Cliente = new SelectList(db.Usuarios, "id", "nombre");
            return View();
        }

        // POST: SolicitudesDevoluciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Id_Cliente,Id_Producto,PrecioVenta,Devolucion,Descripcion,Status")] SolicitudesDevoluciones solicitudesDevoluciones)
        {
            if (ModelState.IsValid)
            {
                db.SolicitudesDevoluciones.Add(solicitudesDevoluciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Producto = new SelectList(db.Productos, "id", "nombre", solicitudesDevoluciones.Id_Producto);
            ViewBag.Id_Cliente = new SelectList(db.Usuarios, "id", "nombre", solicitudesDevoluciones.Id_Cliente);
            return View(solicitudesDevoluciones);
        }

        // GET: SolicitudesDevoluciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudesDevoluciones solicitudesDevoluciones = db.SolicitudesDevoluciones.Find(id);
            if (solicitudesDevoluciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Producto = new SelectList(db.Productos, "id", "nombre", solicitudesDevoluciones.Id_Producto);
            ViewBag.Id_Cliente = new SelectList(db.Usuarios, "id", "nombre", solicitudesDevoluciones.Id_Cliente);
            return View(solicitudesDevoluciones);
        }

        // POST: SolicitudesDevoluciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Id_Cliente,Id_Producto,PrecioVenta,Devolucion,Descripcion,Status")] SolicitudesDevoluciones solicitudesDevoluciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitudesDevoluciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Producto = new SelectList(db.Productos, "id", "nombre", solicitudesDevoluciones.Id_Producto);
            ViewBag.Id_Cliente = new SelectList(db.Usuarios, "id", "nombre", solicitudesDevoluciones.Id_Cliente);
            return View(solicitudesDevoluciones);
        }

        // GET: SolicitudesDevoluciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudesDevoluciones solicitudesDevoluciones = db.SolicitudesDevoluciones.Find(id);
            if (solicitudesDevoluciones == null)
            {
                return HttpNotFound();
            }
            return View(solicitudesDevoluciones);
        }

        // POST: SolicitudesDevoluciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SolicitudesDevoluciones solicitudesDevoluciones = db.SolicitudesDevoluciones.Find(id);
            db.SolicitudesDevoluciones.Remove(solicitudesDevoluciones);
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
