namespace SistemaEventos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("EventoServicio")]
    public partial class EventoServicio
    {
        public int Id { get; set; }

        public int IdEvento { get; set; }

        public int IdServicio { get; set; }

        public decimal Precio { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        //public List<int> idsServicios{ get; set; }
        public List<int> idsServicios { get; set; } = new List<int>();

        public virtual Evento Evento { get; set; }

        public virtual Servicio Servicio { get; set; }

        public List<EventoServicio> Listar()
        {
            var query = new List<EventoServicio>();
            try
            {
                using (var db = new ModeloSistema())
                {
                    query = db.EventoServicio.Include("Servicio").Include("Evento")
                        .ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return query;
        }

        public EventoServicio Obtener(int id)
        {
            EventoServicio objEventoServicio = null;
            try
            {
                using (var db = new ModeloSistema())
                {
                    objEventoServicio = db.EventoServicio.Include("Servicio").Include("Evento")
                        .SingleOrDefault(x => x.Id == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el EventoServicio", ex);
            }
            return objEventoServicio;
        }

        public void Guardar()
        {
            try
            {
                using (var db = new ModeloSistema())
                {
                    foreach (int idServicio in idsServicios)
                    {
                        var eventoServicio = new EventoServicio
                        {
                            IdEvento = this.IdEvento,
                            IdServicio = idServicio,
                            Precio = db.Servicio.Find(idServicio).Precio,
                            FechaCreacion = DateTime.Now,
                            FechaActualizacion = DateTime.Now
                        };
                        db.EventoServicio.Add(eventoServicio);
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar EventoServicio", ex);
            }
        }

        public void Eliminar()
        {
            try
            {
                using (var db = new ModeloSistema())
                {
                    db.Entry(this).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar EventoServicio", ex);
            }
        }


    }

}
