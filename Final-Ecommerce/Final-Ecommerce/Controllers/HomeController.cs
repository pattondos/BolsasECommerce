﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_Ecommerce.Repository;
using Final_Ecommerce.Models;
using Final_Ecommerce.Models.DAL;
using Microsoft.AspNet.Identity;

namespace Final_Ecommerce.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        public GenericUnitToWork _unitOfWork = new GenericUnitToWork();

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Contact(ContactoModel model)
        {
            if (ModelState.IsValid)
            {
                Contacto newContact = new Contacto();
                Contacto ide = _unitOfWork.GetRepositoryInstance<Contacto>().GetLastRecord();
                if (ide != null)
                {
                    newContact.id = ide.id + 1; 
                }
                newContact.nombre = model.nombre;
                newContact.telefono = model.telefono;
                newContact.mensaje = model.mensaje;
                newContact.email = model.email;
                _unitOfWork.GetRepositoryInstance<Contacto>().Add(newContact);

                Session["secc"] = "Se ha enviado correctamente su mensaje a los administradores.";

                return RedirectToAction("Contact");
            }
            return View(model);
        }
    }
}