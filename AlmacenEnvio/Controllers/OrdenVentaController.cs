using AlmacenEnvio.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlmacenEnvio.Controllers
{
    public class OrdenVentaController : Controller
    {

        Entities1 db = new Entities1();
            // GET: Order
            public ActionResult Index()
            {
                return View(db.Orders.OrderByDescending(x => x.OrderID).ToList());
            }
            public ActionResult Detalles(int id)
            {
                Order ord = db.Orders.Find(id);
            var status = db.Orders.Find(id).DIspatched;
            var statusEnv = db.Orders.Find(id).Shipped;
            var statusEnt = db.Orders.Find(id).Deliver;


            var Ord_details = db.OrderDetails.Where(x => x.OrderID == id).ToList();
                var tuple = new Tuple<Order, IEnumerable<OrderDetail>>(ord, Ord_details);

            if (statusEnt == true)
            {
                ViewBag.status = "Entregado";
            }else if (statusEnv == true)
            {
                ViewBag.status = "Enviado";
            }else if (status == true)
            {
                ViewBag.status = "Entregado a paquetería";
            }else if (status == false)
            {
                ViewBag.status = "Sin procesar";
            }

            double SumAmount = Convert.ToDouble(Ord_details.Sum(x => x.TotalAmount));
                ViewBag.TotalItems = Ord_details.Sum(x => x.Quantity);
                ViewBag.Discount = 0;
                ViewBag.TAmount = SumAmount - 0;
                ViewBag.Amount = SumAmount;
                return View(tuple);
            }
        public ActionResult NuvasOrdenes()
        {
            return View(db.Orders.OrderByDescending(x => x.OrderID).Where(x => x.DIspatched == false).Where(x => x.Shipped == false).Where(x => x.Deliver == false).ToList());

        }
        public ActionResult OrdenesEnviadas()
        {
            return View(db.Orders.OrderByDescending(x => x.OrderID).Where(x => x.DIspatched == true).Where(x => x.Shipped == true).Where(x => x.Deliver == false).ToList());

        }
        public ActionResult OrdenesEntregadas()
        {
            return View(db.Orders.OrderByDescending(x => x.OrderID).Where(x => x.DIspatched == true).Where(x => x.Shipped == true).Where(x => x.Deliver == true).ToList());

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {

            Order ord = db.Orders.Find(id);
            if (ord == null)
            {
                return HttpNotFound();
            }
            return View("Edit", ord);
        }

        [HttpPost]
        public ActionResult Edit(Order ord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("NuvasOrdenes", "OrdenVenta");
            }
            return View(ord);
        }

        [HttpGet]
        public ActionResult EditEntrega(int id)
        {
            Order ord = db.Orders.Find(id);
            if (ord == null)
            {
                return HttpNotFound();
            }
            return View("EditEntrega", ord);
        }

        [HttpPost]
        public ActionResult EditEntrega(Order ord)
        {
            if (ModelState.IsValid)
            {
                ord.DIspatched = true;
                ord.Shipped = true;
                ord.DispatchedDate = DateTime.Now;
                db.Entry(ord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("NuvasOrdenes", "OrdenVenta");
            }
            return View(ord);
        }

        [HttpGet]
        public ActionResult EditFechaEntrega(int id)
        {
            Order ord = db.Orders.Find(id);
            if (ord == null)
            {
                return HttpNotFound();
            }
            return View("EditFechaEntrega", ord);
        }

        [HttpPost]
        public ActionResult EditFechaEntrega(Order ord)
        {
            if (ModelState.IsValid)
            {
                ord.DIspatched = true;
                ord.Shipped = true;
                ord.Deliver = true;
                db.Entry(ord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("OrdenesEnviadas", "OrdenVenta");
            }
            return View(ord);
        }

    }
}