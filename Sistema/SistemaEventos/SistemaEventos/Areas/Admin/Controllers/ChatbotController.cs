using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaEventos.Areas.Admin.Controllers
{
    public class ChatbotController : Controller
    {
        // GET: Admin/Chatbot
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Busqueda()
        {
            return View();
        }
    }
}