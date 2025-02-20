using SistemaEventos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SistemaEventos.Areas.Admin.Controllers
{
    [Autenticado]
    public class EventoServicioController : Controller
    {
        private EventoServicio _EventService = new EventoServicio();
        private Servicio _Servicios = new Servicio();
        private Evento _Eventos = new Evento();

        public ActionResult Index()
        {
            var context = new ModeloSistema(); // Asumiendo que ModeloSistema es tu contexto de Entity Framework

            var viewModel = context.EventoServicio
                .Include("Evento")
                .Include("Servicio")
                .ToList()
                .GroupBy(es => es.Evento.Titulo)
                .Select(group => new EventoServicioViewModel
                {
                    NombreEvento = group.Key,
                    DetallesServicios = string.Join(", ", group.Select(x => x.Servicio.Nombre + " ($" + x.Precio.ToString("N2") + ")")),
                    PrecioTotal = group.Sum(x => x.Precio),
                }).ToList();

            return View(viewModel);
        }



        public ActionResult AgregarEditar(int id = 0)
        {
            ViewBag.Servicios = _Servicios.Listar();
            ViewBag.Eventos = _Eventos.Listar();
            return View(
                id == 0 ? new EventoServicio() //nuevo objeto
                : _EventService.Obtener(id)); //devuelve el objeto encontrado

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Guardar(EventoServicio objEventS, int[] idsServicios)
        {
            if (ModelState.IsValid)
            {
                objEventS.idsServicios = idsServicios.ToList();
                objEventS.Guardar();
                return RedirectToAction("Index", "EventoServicio", new { area = "Admin" });
            }
            else
            {
                ViewBag.Servicios = _Servicios.Listar();
                ViewBag.Eventos = _Eventos.Listar();
                return View("AgregarEditar", objEventS);
            }
        }

    }
}