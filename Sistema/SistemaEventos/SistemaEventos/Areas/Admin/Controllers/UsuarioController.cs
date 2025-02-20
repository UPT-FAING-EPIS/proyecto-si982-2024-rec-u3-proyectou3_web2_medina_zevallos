using Firebase.Storage;
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
    public class UsuarioController : Controller
    {
        private Usuario objUsuario = new Usuario();
        private TipoUsuario objTipo = new TipoUsuario();

        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(objUsuario.Listar());
            }
            else
            {
                return View(objUsuario.Buscar(criterio));
            }
        }

        public ActionResult Empleados()
        {
            var ListaEmpleados = objUsuario.Listar().Where(r => r.IdTipoU == 1 || r.IdTipoU == 2);
            // Usuario Empleado 

            ViewBag.Lista = ListaEmpleados.ToList();

            return View();
        }

        public ActionResult Clientes()
        {
            var ListaEmpleados = objUsuario.Listar().Where(r => r.IdTipoU == 3); // Usuario Cliente 

            ViewBag.Lista = ListaEmpleados.ToList();

            return View();
        }



        public ActionResult Ver(int id)
        {
            return View(objUsuario.Obtener(id));

        }
        public ActionResult Buscar(string criterio)
        {
            return View(criterio == null || criterio == "" ? objUsuario.Listar() : objUsuario.Buscar(criterio));

        }
        public ActionResult AgregarEditar(int id = 0)
        {
            //ViewBag.Tipo = objTipo.Listar();

            var tipos = objTipo.Listar();
            ViewBag.Tipo = tipos.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Nombre
            }).ToList();


            return View(
                id == 0 ? new Usuario()
                : objUsuario.Obtener(id));

        }
        //[HttpPost]
        //public ActionResult Guardar(Usuario usuario, HttpPostedFileBase imagen)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (imagen != null && imagen.ContentLength > 0)
        //        {
        //            var fileName = Path.GetFileName(imagen.FileName);
        //            using (var reader = new BinaryReader(imagen.InputStream))
        //            {
        //                usuario.Perfil = usuario.SubirImagen(imagen.InputStream, fileName);
        //            }
        //        }
        //        usuario.Guardar();
        //        return RedirectToAction("Index", "Usuario", new { area = "Admin" });
        //    }
        //    else
        //    {
        //        return RedirectToAction("AgregarEditar", "Usuario", new { area = "Admin", id = usuario.Id });
        //    }
        //}

        [HttpPost]
        public async Task<ActionResult> Guardar(Usuario usuario, HttpPostedFileBase imagen)
        {
            if (ModelState.IsValid)
            {
                if (imagen != null && imagen.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(imagen.FileName);
                    var storage = new FirebaseStorage("sistemaeventos-bf03e.appspot.com");

                    try
                    {
                        // Sube la imagen y obtiene la URL de descarga
                        var imageUrl = await storage
                            .Child("usuario")
                            .Child(fileName)
                            .PutAsync(imagen.InputStream);

                        // Asignar la URL de la imagen al perfil del usuario
                        usuario.Perfil = imageUrl;
                    }
                    catch (Exception ex)
                    {
                        // Manejo de errores
                        ModelState.AddModelError("", "Error al subir la imagen: " + ex.Message);
                        return View("AgregarEditar", usuario); // Asegúrate de tener esta vista para manejar errores
                    }
                }

                // Guarda o actualiza los datos del usuario en tu base de datos
                usuario.Guardar();
                return RedirectToAction("Index", "Usuario", new { area = "Admin" });
            }
            else
            {
                return RedirectToAction("AgregarEditar", "Usuario", new { area = "Admin", id = usuario.Id });
            }
        }

        //public ActionResult Guardar(Usuario objUsuario)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        objUsuario.Guardar();
        //        //return Redirect("~/Usuario");
        //        return RedirectToAction("Index", "Usuario", new { area = "Admin" });

        //    }
        //    else
        //    {
        //        //return View("~/Views/Usuario/AgregarEditar.cshtml");
        //        return RedirectToAction("Index", "Usuario", new { area = "Admin" });

        //    }
        //}
        public ActionResult Eliminar(int id)
        {
            objUsuario.Id = id;
            objUsuario.Eliminar();
            //return Redirect("~/Usuario");
            return RedirectToAction("Index", "Usuario", new { area = "Admin" });

        }
    }
}