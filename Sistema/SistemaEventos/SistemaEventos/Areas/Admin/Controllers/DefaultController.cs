using Firebase.Storage;
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
    public class DefaultController : Controller
    {
        // GET: Admin/Default
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    // Configura el FirebaseStorage, usando el bucket que obtienes de Firebase Console
                    var storage = new FirebaseStorage("sistemaeventos-bf03e.appspot.com");

                    // Obtener la extensión del archivo
                    var extension = System.IO.Path.GetExtension(file.FileName);

                    // Crear un nombre de archivo único para evitar sobreescrituras
                    var fileName = $"{Guid.NewGuid()}{extension}";

                    // Sube el archivo y obtiene la URL de descarga
                    var downloadUrl = await storage
                        .Child("images")
                        .Child(fileName)
                        .PutAsync(file.InputStream);

                    ViewBag.Message = "Archivo subido exitosamente!";
                    ViewBag.DownloadUrl = downloadUrl;
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Error subiendo el archivo: " + ex.Message;
                }
            }
            return View();
        }

    }
}