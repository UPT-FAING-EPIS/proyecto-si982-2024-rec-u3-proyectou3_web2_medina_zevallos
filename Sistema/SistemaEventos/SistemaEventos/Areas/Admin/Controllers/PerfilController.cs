using SistemaEventos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaEventos.Areas.Admin.Controllers
{
    [Autenticado]
    public class PerfilController : Controller
    {
        private Usuario _users = new Usuario();
        public ActionResult Index(int id)
        {
            return View(_users.Obtener(id));
        }

    }
}