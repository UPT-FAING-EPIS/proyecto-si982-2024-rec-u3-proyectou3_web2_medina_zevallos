using SistemaEventos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaEventos.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private Usuario objusuario = new Usuario();

        [NoLogin]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Validar(string Usuario, string Password)
        {
            var rm = objusuario.ValidarLogin(Usuario, Password);

            if (rm.response)
            {
                var user = objusuario.Listar().FirstOrDefault(x => x.Correo == Usuario);
                if (user != null)
                {
                    switch (user.IdTipoU)
                    {
                        case 1:
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                        case 2:
                            return RedirectToAction("Index", "Home", new { area = "Cliente" });
                    }
                }
            }
            return Redirect("~/Login");
        }

        public ActionResult Logout()
        {
            SessionHelper.DestroyUserSession();
            return Redirect("~/Login");
        }
    }
}