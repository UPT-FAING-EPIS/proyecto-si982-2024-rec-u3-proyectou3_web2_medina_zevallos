using SistemaEventos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaEventos.Areas.Admin.Controllers
{
    [Autenticado]
    public class TipoEventoController : Controller
    {
        private TipoEvento objTipoE = new TipoEvento();
        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(objTipoE.Listar());
            }
            else
            {
                return View(objTipoE.Buscar(criterio));
            }
        }
        public ActionResult Ver(int id)
        {
            return View(objTipoE.Obtener(id));
        }
        public ActionResult Buscar(string criterio)
        {
            return View(criterio == null || criterio == "" ? objTipoE.Listar() : objTipoE.Buscar(criterio));
        }
        public ActionResult AgregarEditar(int id = 0)
        {
            return View(
                id == 0 ? new TipoEvento()
                : objTipoE.Obtener(id)); 
        }
        public ActionResult Guardar(TipoEvento objTipoE)
        {
            if (ModelState.IsValid)
            {
                objTipoE.Guardar();
                //return Redirect("~/TipoEvento");
                return RedirectToAction("Index", "TipoEvento", new { area = "Admin" });

            }
            else
            {
                //return View("~/Views/TipoEvento/AgregarEditar.cshtml");
                return RedirectToAction("AgregarEditar", "TipoEvento", new { area = "Admin" });

            }
        }
        public ActionResult Eliminar(int id)
        {
            objTipoE.Id = id;
            objTipoE.Eliminar();
            //return Redirect("~/TipoEvento");
            return RedirectToAction("Index", "TipoEvento", new { area = "Admin" });


        }
    }
}