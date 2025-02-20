using SistemaEventos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaEventos.Areas.Admin.Controllers
{
    [Autenticado]
    public class ResenaController : Controller
    {
        private Resena objResena = new Resena();
        private Evento objEvent = new Evento();
        

        public ActionResult Index()
        {
            return View(objResena.Listar());
        }
        public ActionResult Ver(int id)
        {
            return View(objResena.Obtener(id));

        }
        public ActionResult Buscar(string criterio)
        {
            return View(criterio == null || criterio == "" ? objResena.Listar() : objResena.Buscar(criterio));

        }
        public ActionResult AgregarEditar(int id = 0)
        {
            
            return View(
                id == 0 ? new Resena() //nuevo objeto
                : objResena.Obtener(id)); //devuelve el objeto encontrado

        }
        public ActionResult Guardar(Resena objEvento)
        {
            if (ModelState.IsValid)
            {
                objEvento.Guardar();
                return RedirectToAction("Index", "Resena", new { area = "Admin" });
            }
            else
            {
                return RedirectToAction("AgregarEditar", "Resena", new { area = "Admin" });
            }

        }

        public ActionResult Eliminar(int id)
        {
            objResena.Id = id;
            objResena.Eliminar();
            return RedirectToAction("Index", "Resena", new { area = "Admin" });
        }
    }
}