using SistemaEventos.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SistemaEventos.Areas.Admin.Controllers
{
    [Autenticado]
    public class InvitadoController : Controller
    {
        // GET: Invitado
        private Invitado objInvitado = new Invitado();
        private Reserva objReserva = new Reserva();
        private Evento objEvento = new Evento();
        private ModeloSistema db = new ModeloSistema();

        public ActionResult ReservaCorreo()
        {
            var reserva = db.Reserva.Where(u => u.Estado == "realizado");
            
            return View(reserva);
        }

        private EmailService emailService = new EmailService(
        "smtp.gmail.com", // Servidor SMTP de Gmail
        587,              // Puerto SMTP de Gmail
        "jt2018062232@virtual.upt.pe", // Tu dirección de correo electrónico
        "rlxjfsitihqezvgx" // Tu contraseña de correo electrónico
        );

        public ActionResult EditarCorreo(int id)
        {
            ViewBag.Invitado = db.Invitado.Where(u => u.IdReserva == id);

            var model = new CorreoViewModel
            {
                IdReserva = id,
                Asunto = "Tu invitación al evento", // Asunto predeterminado
                Cuerpo = "Hola [Nombre],Te invitamos a nuestro evento." // Cuerpo predeterminado
            };

            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> EnviarInvitaciones(CorreoViewModel model)
        {
            var invitados = db.Invitado.Where(u => u.IdReserva == model.IdReserva);
            ViewBag.Invitado = db.Invitado.Where(u => u.IdReserva == model.IdReserva);
            foreach (var invitado in invitados)
            {
                var body = model.Cuerpo.Replace("[Nombre]", invitado.Nombre); // Reemplazar marcador con el nombre del invitado
                try
                {
                    await emailService.EnviarCorreoAsync(invitado.Correo, model.Asunto, body);
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Error al enviar correo a " + invitado.Correo + ": " + ex.Message;
                    return View("EditarCorreo", model);
                }
            }
            ViewBag.Message = "Invitaciones enviadas exitosamente.";
            return View("EditarCorreo", model);
        }

        //public async Task<ActionResult> EnviarInvitaciones()
        //public async Task<ActionResult> EnviarInvitaciones(int id)
        //public async Task<ActionResult> EnviarInvitaciones(CorreoViewModel model)
        //{
        //    var invitados = db.Invitado.Where(u => u.IdReserva == model.IdReserva);
        //    foreach (var invitado in invitados)
        //    {
        //        var subject = "Tu invitación al evento";
        //        var body = $"Hola {invitado.Nombre},\n\nTe invitamos a nuestro evento.";
        //        try
        //        {
        //            await emailService.EnviarCorreoAsync(invitado.Correo, subject, body);
        //        }
        //        catch (Exception ex)
        //        {
        //            // Manejo del error
        //            ViewBag.ErrorMessage = "Error al enviar correo a " + invitado.Correo + ": " + ex.Message;
        //            return View();
        //        }
        //    }
        //    ViewBag.Message = "Invitaciones enviadas exitosamente.";
        //    return View();
        //}

        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(objInvitado.Listar());
            }
            else
            {
                return View(objInvitado.Buscar(criterio));
            }
        }
        public ActionResult Ver(int id)
        {
            return View(objInvitado.Obtener(id));

        }
        public ActionResult Buscar(string criterio)
        {
            return View(criterio == null || criterio == "" ? objInvitado.Listar() : objInvitado.Buscar(criterio));

        }
        public ActionResult AgregarEditar(int id = 0)
        {
            return View(
                id == 0 ? new Invitado() 
                : objInvitado.Obtener(id)); 

        }
        public ActionResult Guardar(Invitado objUsuario)
        {
            if (ModelState.IsValid)
            {
                objUsuario.Guardar();
                return RedirectToAction("Index", "Invitado", new { area = "Admin" });

            }
            else
            {
                return RedirectToAction("AgregarEditar", "Invitado", new { area = "Admin" });
            }

        }

        public ActionResult Eliminar(int id)
        {
            objInvitado.Id = id;
            objInvitado.Eliminar();
            return RedirectToAction("Index", "Invitado", new { area = "Admin" });

        }

        [HttpGet]
        public ActionResult Cargar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cargar(int eventoId, HttpPostedFileBase archivoInvitados)
        {
            if (archivoInvitados != null && archivoInvitados.ContentLength > 0)
            {
                var invitados = new List<Invitado>();
                using (var reader = new StreamReader(archivoInvitados.InputStream))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var values = line.Split(',');
                        var invitado = new Invitado
                        {
                            Nombre = values[0],
                            Correo = values[1],
                            IdReserva = eventoId
                        };
                        invitados.Add(invitado);
                    }
                }
                db.Invitado.AddRange(invitados);
                db.SaveChanges();
                ViewBag.Message = "Invitados cargados exitosamente.";
            }
            return View();
        }
    }
}