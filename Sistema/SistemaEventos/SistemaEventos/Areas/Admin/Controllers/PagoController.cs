using SistemaEventos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaEventos.Areas.Admin.Controllers
{
    [Autenticado]
    public class PagoController : Controller
    {
        private Pago _Pago = new Pago();
        private Evento _Event = new Evento();
        private Usuario _User = new Usuario();
        private ModeloSistema db = new ModeloSistema();

        public ActionResult Index()
        {
            var pagos = db.Pago.Include("Reserva").Include("Usuario").ToList();
            var totalCost = pagos.Sum(p => p.Monto ?? 0);

            ViewBag.TotalCost = totalCost;

            return View(_Pago.Listar());
        }
    }
}