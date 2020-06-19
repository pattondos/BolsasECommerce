using ProyectoFinal_Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal_Ecommerce.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        public ActionResult ListarRolesIndex()
        {
            var enti = new Entities();
            return View(enti.Roles.ToList());
        }
    }
}