using SistemaEventos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaEventos.Areas.Admin.Controllers
{
    [Autenticado]
    public class ServicioController : Controller
    {
        private Servicio _Servicio = new Servicio();
        private EventoServicio _EventoServicio= new EventoServicio();

        public ActionResult Index()
        {
            ViewBag.Evento = _EventoServicio.Listar();
            return View(_Servicio.Listar());
        }
    }
}