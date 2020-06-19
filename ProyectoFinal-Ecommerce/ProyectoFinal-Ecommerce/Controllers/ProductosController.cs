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
    [Authorize]
    public class ProductosController : Controller
    {

        GenericUnitToWork gw = new GenericUnitToWork();
        private Entities db = new Entities();

        // GET: Productos
        public ActionResult Index()
        {
            Usuarios s = (Usuarios)Session["admin"];
            if (s != null)
            {
                if (s.role_id == 3 || s.role_id == 2)
                {
                    return View(db.Productos.ToList());
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["mom"]).role_id == 2 || ((ProyectoFinal_Ecommerce.Models.Usuarios)Session["mom"]).role_id == 3)
            {
                Productos productos = db.Productos.Find(id);
                if (productos == null)
                {
                    return HttpNotFound();
                }
                return View(productos);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            Usuarios s = (Usuarios)Session["admin"];
            if (s != null)
            {
                if (s.role_id == 3 || s.role_id == 2)
                {
                    return View();
                }
            }
            return View("Index", "Home");
        }

        // POST: Productos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Productos productos)
        {

            Productos s = gw.GetRepositoryInstance<Productos>().GetLastRecord();

            if (s != null)
            {
                productos.id = s.id + 1;
            }
            productos.stat = 1;
            productos.img = "BatidoD.jpg";
            productos.id_categoria = 1;
            productos.id_proveedor = productos.id_proveedor;

            if (ModelState.IsValid)
            {
                db.Productos.Add(productos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productos);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            Usuarios s = (Usuarios)Session["admin"];
            if (s != null)
            {
                if (s.role_id == 3 || s.role_id == 2)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Productos productos = db.Productos.Find(id);
                    if (productos == null)
                    {
                        return HttpNotFound();
                    }
                    return View(productos);
                }
            }
            return View("Index", "Home");

        }

        // POST: Productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Productos productos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productos);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["mom"]).role_id == 2 || ((ProyectoFinal_Ecommerce.Models.Usuarios)Session["mom"]).role_id == 3)
            {
                Productos productos = db.Productos.Find(id);
                if (productos == null)
                {
                    return HttpNotFound();
                }
                return View(productos);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Productos productos = db.Productos.Find(id);
            db.Productos.Remove(productos);
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
