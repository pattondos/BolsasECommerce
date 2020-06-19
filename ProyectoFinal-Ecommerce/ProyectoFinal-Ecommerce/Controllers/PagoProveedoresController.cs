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
    public class PagoProveedoresController : Controller
    {
        private EntitiesBolsas1 db = new EntitiesBolsas1();

        // GET: PagoProveedores
        public ActionResult Index()
        {
            var pagoProveedores = db.PagoProveedores.Include(p => p.Proveedores);
            return View(pagoProveedores.ToList());
        }

        // GET: PagoProveedores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagoProveedores pagoProveedores = db.PagoProveedores.Find(id);
            if (pagoProveedores == null)
            {
                return HttpNotFound();
            }
            return View(pagoProveedores);
        }

        // GET: PagoProveedores/Create
        public ActionResult Create()
        {
            ViewBag.Id_Proveedor = new SelectList(db.Proveedores, "id", "razon_social");
            return View();
        }

        // POST: PagoProveedores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Id_Proveedor,FechaEmision,FechaEntrega,FechaPago,TotalPagar,Restante,Status")] PagoProveedores pagoProveedores)
        {
            if (ModelState.IsValid)
            {
                db.PagoProveedores.Add(pagoProveedores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Proveedor = new SelectList(db.Proveedores, "id", "razon_social", pagoProveedores.Id_Proveedor);
            return View(pagoProveedores);
        }

        // GET: PagoProveedores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagoProveedores pagoProveedores = db.PagoProveedores.Find(id);
            if (pagoProveedores == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Proveedor = new SelectList(db.Proveedores, "id", "razon_social", pagoProveedores.Id_Proveedor);
            return View(pagoProveedores);
        }

        // POST: PagoProveedores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Id_Proveedor,FechaEmision,FechaEntrega,FechaPago,TotalPagar,Restante,Status")] PagoProveedores pagoProveedores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pagoProveedores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Proveedor = new SelectList(db.Proveedores, "id", "razon_social", pagoProveedores.Id_Proveedor);
            return View(pagoProveedores);
        }

        // GET: PagoProveedores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagoProveedores pagoProveedores = db.PagoProveedores.Find(id);
            if (pagoProveedores == null)
            {
                return HttpNotFound();
            }
            return View(pagoProveedores);
        }

        // POST: PagoProveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PagoProveedores pagoProveedores = db.PagoProveedores.Find(id);
            db.PagoProveedores.Remove(pagoProveedores);
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
