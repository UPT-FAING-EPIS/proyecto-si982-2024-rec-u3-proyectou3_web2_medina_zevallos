using SistemaEventos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaEventos.Areas.Admin.Controllers
{
    [Autenticado]
    public class TipoUsuarioController : Controller
    {
        private TipoUsuario objTipoU = new TipoUsuario();
        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(objTipoU.Listar());
            }
            else
            {
                return View(objTipoU.Buscar(criterio));
            }
        }
        public ActionResult Ver(int id)
        {
            return View(objTipoU.Obtener(id));

        }
        public ActionResult Buscar(string criterio)
        {
            return View(criterio == null || criterio == "" ? objTipoU.Listar() : objTipoU.Buscar(criterio));

        }
        public ActionResult AgregarEditar(int id = 0)
        {
            return View(
                id == 0 ? new TipoUsuario() //nuevo objeto
                : objTipoU.Obtener(id)); //devuelve el objeto encontrado

        }
        public ActionResult Guardar(TipoUsuario objTipoE)
        {
            if (ModelState.IsValid)
            {
                objTipoE.Guardar();
                //return Redirect("~/TipoUsuario");
                return RedirectToAction("Index", "TipoUsuario", new { area = "Admin" });

            }
            else
            {
                //return View("~/Views/TipoUsuario/AgregarEditar.cshtml");
                return RedirectToAction("AgregarEditar", "TipoUsuario", new { area = "Admin" });

            }
        }
        public ActionResult Eliminar(int id)
        {
            objTipoU.Id = id;
            objTipoU.Eliminar();
            //return Redirect("~/TipoUsuario");
            return RedirectToAction("Index", "TipoUsuario", new { area = "Admin" });

        }
    }
}