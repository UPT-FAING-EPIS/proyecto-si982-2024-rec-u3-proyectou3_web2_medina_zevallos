using SistemaEventos.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaEventos.Areas.Cliente.Controllers
{
    [Autenticado]
    public class ReservaController : Controller
    {
        private Reserva objReserva = new Reserva();
        private Horario objHorario = new Horario();
        private Usuario objUsuario = new Usuario();
        private Evento objEvento= new Evento();
        private Pago objPago = new Pago();
        private ModeloSistema db = new ModeloSistema();

        public ActionResult Index(int id)
        {
            var horarios = db.Horario.ToList();
            ViewBag.Horarios = new SelectList(horarios, "Id", "HoraInicio");

            var model = new Reserva
            {
                IdEvento = id
            };
            return View(model);
        }


        [HttpPost]
        public ActionResult Guardar(Reserva objReserva)
        {
            if (ModelState.IsValid)
            {
                objReserva.Guardar();
                return RedirectToAction("Pago", "Reserva", new { idReserva = objReserva.Id , area = "Cliente" });
            }
            var horarios = db.Horario.ToList();
            ViewBag.Horarios = new SelectList(horarios, "Id", "HoraInicio");
            return View("Index", objReserva);
        }

        public ActionResult Pago(int idReserva)
        {
            ViewBag.Pago = objPago.Listar();
            ViewBag.Evento = objEvento.Listar();

            var horarios = db.Horario.ToList();
            var reserva = db.Reserva.Find(idReserva);
            var pago = new Pago
            {
                IdReserva = idReserva,
                IdUsuario = reserva.IdUsuario,
                //Monto = 1000,
                FechaPago = DateTime.Now
            };
            return View(pago);
        }


        //public ActionResult VerReserva(int id)
        //{
        //    var Reserva = objReserva.Obtener(id);
        //    ViewBag.InvitadosCount = db.Invitado.Count(i => i.IdReserva == id);
        //    return View(Reserva);
        //}
        public ActionResult VerReserva(int id)
        {
            var reserva = objReserva.Obtener(id);

            var idEvento = reserva.IdEvento;

            ViewBag.InvitadosCount = db.Invitado.
                Count(i => i.IdReserva == id);

            ViewBag.DetalleEvent = db.DetalleEvento.
                Where(i => i.IdEvento == idEvento).
                ToList();

            ViewBag.Servicios2 = db.Servicio.
                ToList();

            ViewBag.Servicios = db.EventoServicio.
                Where(i => i.IdEvento == idEvento).
                ToList();


            return View(reserva);
        }

        [HttpPost]
        public ActionResult VerReserva(int IdReserva, HttpPostedFileBase archivoInvitados, bool? confirmarEliminacion = null)
        {
            var reserva = objReserva.Obtener(IdReserva);

            ViewBag.DetalleEvent = db.DetalleEvento.
                Where(i => i.IdEvento == IdReserva).
                ToList();

            ViewBag.Servicios = db.EventoServicio.
                Where(i => i.IdEvento == IdReserva).
                ToList();

            ViewBag.Servicios2 = db.Servicio.
               ToList();

            

            if (confirmarEliminacion.HasValue && confirmarEliminacion.Value)
            {
                // Recuperar archivo de la sesión
                archivoInvitados = Session["archivoInvitados"] as HttpPostedFileBase;
                Session.Remove("archivoInvitados");
            }
            if (archivoInvitados != null && archivoInvitados.ContentLength > 0)
            {
                if (!confirmarEliminacion.HasValue || !confirmarEliminacion.Value)
                {
                    if (db.Invitado.Any(i => i.IdReserva == IdReserva))
                    {
                        Session["archivoInvitados"] = archivoInvitados;
                        ViewBag.ConfirmarEliminacion = true;
                        ViewBag.IdReserva = IdReserva;
                        return View(reserva);  // Pasar el modelo de reserva a la vista
                    }
                }
                // Eliminar los invitados existentes
                var invitadosExistentes = db.Invitado.Where(i => i.IdReserva == IdReserva).ToList();
                db.Invitado.RemoveRange(invitadosExistentes);
                db.SaveChanges();
                int invitadosAgregados = 0;
                var nuevosInvitados = new List<Invitado>();
                using (var reader = new StreamReader(archivoInvitados.InputStream))
                {
                    string line;
                    char delimiter = ',';
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(";"))
                        {
                            delimiter = ';';
                        }
                        var values = line.Split(delimiter);
                        var invitado = new Invitado
                        {
                            Nombre = values[0],
                            Correo = values[1],
                            Dni = values[2],
                            IdReserva = IdReserva,
                            FechaCreacion = DateTime.Now
                        };
                        nuevosInvitados.Add(invitado);
                    }
                }
                db.Invitado.AddRange(nuevosInvitados);
                db.SaveChanges();
                invitadosAgregados = nuevosInvitados.Count;
                TempData["SuccessMessage"] = $"Invitados cargados exitosamente. Total invitados: {invitadosAgregados}.";
                ViewBag.InvitadosCount = db.Invitado.Count(i => i.IdReserva == IdReserva);
            }
            return RedirectToAction("VerReserva", new { id = IdReserva });
        }


        public ActionResult VerInvitados(int idReserva)
        {
            var invitados = db.Invitado.Where(i => i.IdReserva == idReserva).ToList();
            ViewBag.IdReserva = idReserva;
            return View(invitados);
        }

        [HttpPost]
        public ActionResult Edit(Invitado invitado)
        {
            var existingInvitado = db.Invitado.FirstOrDefault(i => i.Id == invitado.Id);
            if (existingInvitado == null)
            {
                return HttpNotFound();
            }

            existingInvitado.Nombre = invitado.Nombre;
            existingInvitado.Dni = invitado.Dni;
            existingInvitado.Correo = invitado.Correo;
            existingInvitado.FechaCreacion = invitado.FechaCreacion;

            db.SaveChanges();

            return RedirectToAction("VerInvitados", new { idReserva = existingInvitado.IdReserva , area = "Cliente" });
        }


        public ActionResult ProcesarPago(Pago objPago)
        {
            objPago.Guardar();
            return RedirectToAction("Confirmacion", "Reserva", new { area = "Cliente" });
        }

        public ActionResult Confirmacion()
        {
            return View();
        }

        public ActionResult MisReservas()
        {
            ViewBag.Hora = objHorario.Listar();
            ViewBag.Usuario = objUsuario.Listar();

            return View(objReserva.Listar());
        }
    }
}