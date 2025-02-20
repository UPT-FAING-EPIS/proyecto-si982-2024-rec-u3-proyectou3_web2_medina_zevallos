using SistemaEventos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaEventos.Areas.Admin.Controllers
{
    [Autenticado]
    public class HomeController : Controller
    {
        private readonly ModeloSistema _context;

        public HomeController()
        {
            _context = new ModeloSistema();
        }

        public ActionResult Index()
        {
            var totalUsers = _context.Usuario.Count();
            var totalEmployees = _context.Usuario.Count(u => u.IdTipoU == 2);
            var totalClients = _context.Usuario.Count(u => u.IdTipoU == 3);
            var totalAdmins = _context.Usuario.Count(u => u.IdTipoU == 1);

            var totalEvents = _context.Evento.Count();

            var totalReservations = _context.Reserva.Count();
            var pendingReservations = _context.Reserva.Count(r => r.Estado == "pendiente");
            var confirmedReservations = _context.Reserva.Count(r => r.Estado == "realizado");

            var reservationsByMonth = _context.Reserva
            .GroupBy(r => new { Year = r.FechaReserva.Year, Month = r.FechaReserva.Month })
            .Select(g => new ReservationsByMonth
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                Count = g.Count()
            })
            .OrderBy(r => r.Year).ThenBy(r => r.Month)
            .ToList();



            var viewModel = new DashboardViewModel
            {
                TotalUsers = totalUsers,
                TotalEmployees = totalEmployees,
                TotalClients = totalClients,
                TotalAdmins = totalAdmins,
                TotalEvents = totalEvents,
                TotalReservations = totalReservations,
                PendingReservations = pendingReservations,
                ConfirmedReservations = confirmedReservations,

                ReservationsByMonth = reservationsByMonth
            };
            return View(viewModel);
        }
    }
}
