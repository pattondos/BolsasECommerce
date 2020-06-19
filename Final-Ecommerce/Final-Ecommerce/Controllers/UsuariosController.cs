using Final_Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Final_Ecommerce.Repository;
using Final_Ecommerce.Models.DAL;
using System.Threading.Tasks;

namespace Final_Ecommerce.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        public GenericUnitToWork _unitOfWork = new GenericUnitToWork();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        // GET: Usuarios
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(UsuarioModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.correo, Email = model.correo };
                var result = await UserManager.CreateAsync(user, model.pass);
                if (result.Succeeded)
                {
                    Usuarios newUser = new Usuarios();
                    //List<Usuarios> lista = _unitOfWork.GetRepositoryInstance<Usuarios>().GetAllRecords().ToList();
                    List<Usuarios> u = _unitOfWork.GetRepositoryInstance<Usuarios>().GetAllRecords().ToList();
                    if (u.Count() != 0)
                    {
                        newUser.id = u.Last().id + 1;
                    }
                    newUser.correo = model.correo;
                    newUser.username = model.username;
                    newUser.role_id = 1;
                    newUser.nombre = model.nombre;
                    newUser.apellido_paterno = model.apellido_paterno;
                    newUser.apellido_materno = model.apellido_materno;

                    Session["usr"] = newUser;

                    newUser.telefono = model.telefono;
                    newUser.pass = model.pass;
                    newUser.status = 1;


                    newUser.fecha_nacimiento = model.fecha_nacimiento;
                    _unitOfWork.GetRepositoryInstance<Usuarios>().Add(newUser);

                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
                Session["usr"] = null;
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

        public ActionResult Perfil()
        {
            Usuarios current = (Usuarios)Session["usr"];
            Usuarios u = _unitOfWork.GetRepositoryInstance<Usuarios>().GetFirstOrDefaultByParameter(i => i.id == current.id);

            UsuarioModelEdit model = new UsuarioModelEdit()
            {
                id = u.id,
                apellido_materno = u.apellido_materno,
                apellido_paterno = u.apellido_paterno,
                nombre = u.nombre,
                username = u.username,
                telefono = u.telefono,
                fecha_nacimiento = u.fecha_nacimiento,
                correo = u.correo
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult EditarPerfil(UsuarioModelEdit model)
        {
            if (ModelState.IsValid)
            {
                Usuarios newUser = _unitOfWork.GetRepositoryInstance<Usuarios>().GetFirstOrDefaultByParameter(i => i.id == model.id);
                //newUser.correo = model.correo;
                newUser.username = model.username;
                newUser.nombre = model.nombre;
                newUser.apellido_paterno = model.apellido_paterno;
                newUser.apellido_materno = model.apellido_materno;

                Session["usr"] = newUser;

                newUser.telefono = model.telefono;
                newUser.fecha_nacimiento = model.fecha_nacimiento;
                _unitOfWork.GetRepositoryInstance<Usuarios>().Update(newUser);
                Session["secc"] = "Se han guardado correctamente los datos.";
            }
            return RedirectToAction("Perfil");
        }


    }
}