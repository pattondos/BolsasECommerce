using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoFinal_Ecommerce.Models;
using ProyectoFinal_Ecommerce.Repository;

namespace ProyectoFinal_Ecommerce.Controllers
{
    public class ProveedoresController : Controller
    {

        GenericUnitToWork _unitToWork = new GenericUnitToWork();

        private Entities db = new Entities();

        // GET: Proveedores
        public ActionResult Index()
        {
            return View(db.Proveedores.ToList());
        }

        // GET: Proveedores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedores proveedores = db.Proveedores.Find(id);
            if (proveedores == null)
            {
                return HttpNotFound();
            }
            return View(proveedores);
        }

        // GET: Proveedores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Proveedores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Proveedores proveedores)
        {
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["mom"]).role_id == 2 || ((ProyectoFinal_Ecommerce.Models.Usuarios)Session["mom"]).role_id == 4)
            {
                Proveedores s = _unitToWork.GetRepositoryInstance<Proveedores>().GetLastRecord();

                if (s != null)
                {
                    proveedores.id = s.id + 1;
                }

                proveedores.stat = 1;

                if (ModelState.IsValid)
                {
                    db.Proveedores.Add(proveedores);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(proveedores);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Proveedores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["mom"]).role_id == 2 || ((ProyectoFinal_Ecommerce.Models.Usuarios)Session["mom"]).role_id == 4)
            {
                Proveedores proveedores = db.Proveedores.Find(id);
                if (proveedores == null)
                {
                    return HttpNotFound();
                }
                return View(proveedores);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Proveedores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Proveedores proveedores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proveedores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(proveedores);
        }

        // GET: Proveedores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["mom"]).role_id == 2 || ((ProyectoFinal_Ecommerce.Models.Usuarios)Session["mom"]).role_id == 4)
            {
                Proveedores proveedores = db.Proveedores.Find(id);
                if (proveedores == null)
                {
                    return HttpNotFound();
                }
                return View(proveedores);

            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proveedores proveedores = db.Proveedores.Find(id);
            db.Proveedores.Remove(proveedores);
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
