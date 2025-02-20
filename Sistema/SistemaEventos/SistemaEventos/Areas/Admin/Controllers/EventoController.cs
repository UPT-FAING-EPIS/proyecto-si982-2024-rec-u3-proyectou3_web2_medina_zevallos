using SistemaEventos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaEventos.Areas.Admin.Controllers
{
    [Autenticado]
    public class EventoController : Controller
    {
        private Evento objEvento = new Evento();
        private TipoEvento objTipoEvento = new TipoEvento();
        private ModeloSistema db = new ModeloSistema();

        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(objEvento.Listar());
            }
            else
            {
                return View(objEvento.Buscar(criterio));
            }
        }
        public ActionResult Ver(int id)
        {
            return View(objEvento.Obtener(id));

        }
        public ActionResult Buscar(string criterio)
        {
            return View(criterio == null || criterio == "" ? objEvento.Listar() : objEvento.Buscar(criterio));

        }
        public ActionResult AgregarEditar(int id = 0)
        {
            //return View(
            //    id == 0 ? new Evento() //nuevo objeto
            //    : objEvento.Obtener(id)); //devuelve el objeto encontrado
            ViewBag.ServicioId = new SelectList(db.Servicio, "Id", "Nombre");
            ViewBag.TipoEvento = objTipoEvento.Listar();
            if (id == 0)
            {
                return View(new Evento());
            }
            else
            {
                var evento = db.Evento.Include("EventoServicio.Servicio").FirstOrDefault(e => e.Id == id);
                if (evento == null)
                {
                    return HttpNotFound();
                }
                return View(evento);
            }

        }
        public ActionResult Guardar(Evento objEvento)
        {
            if (ModelState.IsValid)
            {
                objEvento.Guardar();
                //return Redirect("~/Evento");
                return RedirectToAction("Index", "Evento", new { area = "Admin" });

            }
            else
            {
                //return View("~/Views/Evento/AgregarEditar.cshtml");
                return RedirectToAction("AgregarEditar", "Evento", new { area = "Admin" });

            }
        }

        public ActionResult Eliminar(int id)
        {
            objEvento.Id = id;
            objEvento.Eliminar();
            //return Redirect("~/Evento");
            return RedirectToAction("Index", "Evento", new { area = "Admin" });

        }
    }
}