using SistemaEventos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaEventos.Areas.Admin.Controllers
{
    [Autenticado]
    public class DetalleEventoController : Controller
    {
        private DetalleEvento _DetalleEvento = new DetalleEvento();
        private Evento _Evento = new Evento();
        private ModeloSistema db = new ModeloSistema();

        public ActionResult Index()
        {
            return View(_DetalleEvento.Listar());
        }
        

        public ActionResult AgregarEditar(int id = 0)
        {
            ViewBag.Evento = _Evento.Listar();

            if (id == 0)
            {
                return View(new DetalleEvento());
            }
            else
            {
                var detevento = db.DetalleEvento.Include("Evento").FirstOrDefault(e => e.Id == id);
                if (detevento == null)
                {
                    return HttpNotFound();
                }
                return View(detevento);
            }
        }

        public ActionResult Guardar(DetalleEvento _Evento)
        {
            if (ModelState.IsValid)
            {
                _Evento.Guardar();
                return RedirectToAction("Index", "DetalleEvento", new { area = "Admin" });
            }
            else
            {
                return RedirectToAction("AgregarEditar", "DetalleEvento", new { area = "Admin" });
            }
        }

    }
}