using SistemaEventos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaEventos.Areas.Admin.Controllers
{
    [Autenticado]
    public class TransaccionesController : Controller
    {
        private Transaccion _transation = new Transaccion();
        public ActionResult Index()
        {
            var transacciones = _transation.Listar();
            return View(transacciones);
        }
    }
}