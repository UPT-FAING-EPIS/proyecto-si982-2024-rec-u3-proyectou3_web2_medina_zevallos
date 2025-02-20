using SistemaEventos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaEventos.Areas.Admin.Controllers
{
    [Autenticado]
    public class ReservaController : Controller
    {
        private Reserva objReserva = new Reserva();

        public ActionResult Index()
        {
            return View(objReserva.Listar());
        }
        public ActionResult Ver(int id)
        {
            return View(objReserva.Obtener(id));
        }
        public ActionResult AgregarEditar(int id = 0)
        {
            return View(
                id == 0 ? new Reserva()
                : objReserva.Obtener(id));
        }
        public ActionResult Guardar(Reserva objEvento)
        {
            if (ModelState.IsValid)
            {
                objEvento.Guardar();
                //return Redirect("~/Reserva");
                return RedirectToAction("Index", "Reserva", new { area = "Admin" });

            }
            else
            {
                //return View("~/Views/Reserva/AgregarEditar.cshtml");
                return RedirectToAction("AgregarEditar", "Reserva", new { area = "Admin" });

            }
        }
        public ActionResult Eliminar(int id)
        {
            objReserva.Id = id;
            objReserva.Eliminar();
            //return Redirect("~/Reserva/Listar");
            return RedirectToAction("Index", "Reserva", new { area = "Admin" });
        }
    }
}