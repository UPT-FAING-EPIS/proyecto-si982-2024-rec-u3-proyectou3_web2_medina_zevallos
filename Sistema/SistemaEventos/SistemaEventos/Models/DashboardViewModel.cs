using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaEventos.Models
{
    public class DashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalEmployees { get; set; }
        public int TotalClients { get; set; }
        public int TotalAdmins { get; set; }

        public int TotalEvents { get; set; }
        public int UpcomingEvents { get; set; }
        public int PastEvents { get; set; }

        public int TotalReservations { get; set; }
        public int PendingReservations { get; set; }
        public int ConfirmedReservations { get; set; }
        public int CancelledReservations { get; set; }

        public List<ReservationsByMonth> ReservationsByMonth { get; set; } = new List<ReservationsByMonth>();
    }

    public class ReservationsByMonth
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Count { get; set; }
    }

    public class EventoServicioViewModel
    {
        public string NombreEvento { get; set; }
        public string DetallesServicios { get; set; }
        public decimal PrecioTotal { get; set; }
        public DateTime FechaCreacionInicial { get; set; }
        public DateTime UltimaFechaActualizacion { get; set; }
    }

    public class ReservaViewModel
    {
        public int ReservaId { get; set; }
        public string EstadoReserva { get; set; }
        public DateTime FechaReserva { get; set; }
        public int NumeroInvitados { get; set; }

        // Información del evento
        public string TituloEvento { get; set; }
        public string DescripcionEvento { get; set; }
        public string EstadoEvento { get; set; }
        public decimal Costo { get; set; }

        // Detalles del evento
        public string Ubicacion { get; set; }
        public int? CapacidadMaxima { get; set; }
        public string ImagenUrl { get; set; }
        public string VideoUrl { get; set; }

        // Información adicional
        public string TipoEvento { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
    }
    public class CorreoViewModel
    {
        public int IdReserva { get; set; }
        public string Asunto { get; set; }
        public string Cuerpo { get; set; }
    }
}