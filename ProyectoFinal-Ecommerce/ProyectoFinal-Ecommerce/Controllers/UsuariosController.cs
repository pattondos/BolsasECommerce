using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ProyectoFinal_Ecommerce.Models;
using ProyectoFinal_Ecommerce.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal_Ecommerce.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        public GenericUnitToWork _unitOfWork = new GenericUnitToWork();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ActionResult VistaPanelUsuarios()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Registro()
        {
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["mom"]).role_id != 2)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registro(UsuarioModel model)
        {
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["mom"]).role_id != 2)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                if (model.role_id == 0)
                {
                    Session["bot"] = "Elige un rol válido";
                    return View("Registro");
                }

                var user = new ApplicationUser { UserName = model.correo, Email = model.correo };
                var result = await UserManager.CreateAsync(user, model.pass);
                if (result.Succeeded)
                {
                    Usuarios newUser = new Usuarios();
                    List<Usuarios> u = _unitOfWork.GetRepositoryInstance<Usuarios>().GetAllRecords().ToList();
                    if (u.Count() > 0)
                    {
                        newUser.id = u.Last().id + 1;
                    }
                    newUser.correo = model.correo;
                    newUser.username = model.username;
                    newUser.role_id = model.role_id;
                    newUser.nombre = model.nombre;
                    newUser.apellido_paterno = model.apellido_paterno;
                    newUser.apellido_materno = model.apellido_materno;

                    // Session["usr"] = newUser;


                    newUser.pass = model.pass;
                    newUser.status = 1;


                    _unitOfWork.GetRepositoryInstance<Usuarios>().Add(newUser);

                    // await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
                // Session["usr"] = null;
            }
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ActionResult Perfil(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("VistaPanelUsuarios");
            }

            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["mom"]).role_id != 2)
            {
                return RedirectToAction("Index", "Home");
            }

            Usuarios u = _unitOfWork.GetRepositoryInstance<Usuarios>().GetFirstorDefaultByParameter(i => i.id == id);

            UsuarioModelEdit model = new UsuarioModelEdit()
            {
                id = u.id,
                apellido_materno = u.apellido_materno,
                apellido_paterno = u.apellido_paterno,
                nombre = u.nombre,
                username = u.username,
                role_id = u.role_id,
                correo = u.correo
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult EditarPerfil(UsuarioModelEdit model)
        {
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["mom"]).role_id != 2)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                Usuarios newUser = _unitOfWork.GetRepositoryInstance<Usuarios>().GetFirstorDefaultByParameter(i => i.id == model.id);

                newUser.username = model.username;
                newUser.nombre = model.nombre;
                newUser.apellido_paterno = model.apellido_paterno;
                newUser.apellido_materno = model.apellido_materno;
                newUser.role_id = model.role_id;

                _unitOfWork.GetRepositoryInstance<Usuarios>().Update(newUser);

                Usuarios us = (Usuarios)Session["mom"];

                if (us != null && us.id == model.id)
                {
                    Usuarios xd = _unitOfWork.GetRepositoryInstance<Usuarios>().GetFirstorDefaultByParameter(i => i.id == model.id);
                    xd.pass = null;

                    Session["mom"] = xd;
                }

                Session["secc"] = "Se han guardado correctamente los datos.";
            }
            return RedirectToAction("Perfil", new { model.id });
        }

        public ActionResult ListarEmpleados()
        {
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["mom"]).role_id != 2)
            {
                return RedirectToAction("Index", "Home");
            }
            var ent = new Entities();
            return View(ent.Usuarios.ToList());
        }

        public ActionResult EliminarUsuarios(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("VistaPanelUsuarios");
            }
            if (((ProyectoFinal_Ecommerce.Models.Usuarios)Session["mom"]).role_id != 2)
            {
                return RedirectToAction("Index", "Home");
            }
            Usuarios u = _unitOfWork.GetRepositoryInstance<Usuarios>().GetFirstorDefaultByParameter(i => i.id == id);

            AspNetUsers us = _unitOfWork.GetRepositoryInstance<AspNetUsers>().GetFirstorDefaultByParameter(i => i.Email == u.correo);

            _unitOfWork.GetRepositoryInstance<Usuarios>().Remove(u);
            _unitOfWork.GetRepositoryInstance<AspNetUsers>().Remove(us);

            return RedirectToAction("VistaPanelUsuarios");
        }

    }
}