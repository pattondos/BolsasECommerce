using Final_Ecommerce.Models;
using Final_Ecommerce.Models.DAL;
using Final_Ecommerce.Repository;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Final_Ecommerce.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {

        public GenericUnitToWork _unitOfWork = new GenericUnitToWork();

        // GET: Pago
        public ActionResult PaymentWithPayPal(string Cancel = null)
        {
            if (Session["usr"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            //Se obtiene apiContext
            APIContext apiContext = PayPalConfig.GetAPIContext();
            try
            {
                string PayerId = Request.Params["PayerId"];
                if (string.IsNullOrEmpty(PayerId))
                {
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority
                        + "/Payment/PaymentWithPayPal?";
                    var guid = Convert.ToString((new Random()).Next(100000));
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);

                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;

                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    var guid = Request.Params["guid"];
                    var executePayment = ExecutePayment(apiContext, PayerId, Session[guid] as string);

                    if (executePayment.state.ToLower() != "approved")
                    {
                        ViewBag.ErrorMessage = executePayment.failure_reason;
                        return View("FailureView");
                    }
                }
            }
            catch (Exception e)
            {
                return View("FailureView");
            }
            RegistraVenta();
            return View("SuccessView");
        }

        private PayPal.Api.Payment payment;

        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            List<Item> itemsPayPal = new List<Item>();
            foreach (CarritoModel item in ((List<CarritoModel>)Session["carrito"]))
            {
                Item i = new Item();
                i.name = item.Nombre.ToString();
                i.currency = "MXN";
                i.price = item.Precio_venta.ToString();
                i.quantity = item.Cantidad.ToString();
                i.sku = item.Id_producto.ToString();
                itemsPayPal.Add(i);
            }

            List<CarritoModel> carrito = (List<CarritoModel>)Session["carrito"];
            double subtotal = (double)carrito.Sum(i => i.Cantidad * i.Precio_venta);
            //double shipping = subtotal * .15;

            var ItemList = new ItemList() { items = itemsPayPal };

            var payer = new Payer() { payment_method = "paypal" };

            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };

            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = subtotal.ToString()
            };

            var amount = new Amount()
            {
                currency = "MXN",
                total = subtotal.ToString(),
                details = details
            };

            var transactionList = new List<Transaction>();
            transactionList.Add(new Transaction()
            {
                description = "Transaction Description",
                invoice_number = "#" + Convert.ToString((new Random()).Next(100000)),
                amount = amount,
                item_list = ItemList
            });

            payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            return this.payment.Create(apiContext);
        }

        public ViewResult FailureView()
        {
            return View();
        }

        public ViewResult SuccessView()
        {
            return View();
        }

        public void RegistraVenta()
        {
            List<CarritoModel> carrito = (List<CarritoModel>)Session["carrito"];
            Ventas venta = new Ventas();
            //List< Detalle_Ventas> detalles = new List<Detalle_Ventas>();
            Detalle_Ventas detalle = new Detalle_Ventas();
            //List<Ventas> u = _unitOfWork.GetRepositoryInstance<Ventas>().GetAllRecords().ToList();
            Ventas lastVenta = _unitOfWork.GetRepositoryInstance<Ventas>().GetLastRecord();

            if (lastVenta != null)
            {
                venta.id = lastVenta.id + 1;
            }

            venta.fecha = DateTime.Today;
            venta.id_usuario = ((Usuarios)Session["usr"]).id;
            venta.iva = 0;
            venta.sub_total = carrito.Sum(i => i.Cantidad * i.Precio_venta);
            venta.total = venta.sub_total;

            _unitOfWork.GetRepositoryInstance<Ventas>().Add(venta);

            foreach (CarritoModel item in carrito)
            {
                Detalle_Ventas d = _unitOfWork.GetRepositoryInstance<Detalle_Ventas>().GetLastRecord();

                if (d != null)
                {
                    detalle.id = d.id + 1;
                }

                detalle.id_producto = item.Id_producto;
                detalle.precio_venta = item.Precio_venta;
                detalle.precio_compra = _unitOfWork.GetRepositoryInstance<Productos>().GetFirstOrDefaultByParameter(i => i.id == item.Id_producto).precio_compra;
                detalle.stat = 1;
                detalle.id_venta = venta.id;
                detalle.cantidad = item.Cantidad;

                _unitOfWork.GetRepositoryInstance<Detalle_Ventas>().Add(detalle);

                Productos p = _unitOfWork.GetRepositoryInstance<Productos>().GetFirstOrDefaultByParameter(i => i.id == item.Id_producto);
                int cantidad = p.cantidad - item.Cantidad;

                p.cantidad = cantidad;

                _unitOfWork.GetRepositoryInstance<Productos>().Update(p);

                detalle = new Detalle_Ventas();
            }

            Session["carrito"] = null;

        }
    }

}