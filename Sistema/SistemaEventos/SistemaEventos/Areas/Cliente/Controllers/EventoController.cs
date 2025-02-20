using SistemaEventos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SistemaEventos.Areas.Cliente.Controllers
{
    public class EventoController : Controller
    {
        // GET: Cliente/Evento
        
        private TipoEvento objTipoE = new TipoEvento();
        private Evento objEvento = new Evento();
        private DetalleEvento objDetalle = new DetalleEvento();
        private Resena objComent = new Resena();
        private Reserva objReserva = new Reserva();

        public ActionResult Index()
        {
            ViewBag.DetalleEvento = objDetalle.Listar();
            return View(objEvento.Listar());
        }
        public ActionResult DetalleEvento(int id)
        {
            var detalleEvento = objDetalle.Obtener(id);
            ViewBag.Evento = objEvento.Listar();

           //ViewBag.Comentario1 = objComent.Listar().Where(r => r.IdEvento == id).ToList();
           ViewBag.Comentario2 = objComent.Listar().ToList();
           
            return View(detalleEvento);
        }


        public ActionResult Comentarios()
        {
            ViewBag.Comentario = objComent.Listar();
            return View();
        }
        [HttpPost]
        public ActionResult AgregarComentario(Resena objComent, int IdDetalleEvento)
        {
            objComent.Guardar();
            return RedirectToAction("DetalleEvento", new { id = IdDetalleEvento });
        }

    }
}