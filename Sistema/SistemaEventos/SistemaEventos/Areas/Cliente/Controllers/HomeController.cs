using SistemaEventos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaEventos.Areas.Cliente.Controllers
{
    public class HomeController : Controller
    {
        // GET: Cliente/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}