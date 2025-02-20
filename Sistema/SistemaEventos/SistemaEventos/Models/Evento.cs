namespace SistemaEventos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Evento")]
    public partial class Evento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Evento()
        {
            DetalleEvento = new HashSet<DetalleEvento>();
            EventoServicio = new HashSet<EventoServicio>();
            Publicidad = new HashSet<Publicidad>();
            Resena = new HashSet<Resena>();
            Reserva = new HashSet<Reserva>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        [StringLength(50)]
        public string Estado { get; set; }

        public int IdTipoEvento { get; set; }

        public decimal Costo { get; set; }

        public decimal? CostoPorInvitado { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleEvento> DetalleEvento { get; set; }

        public virtual TipoEvento TipoEvento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventoServicio> EventoServicio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Publicidad> Publicidad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Resena> Resena { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reserva> Reserva { get; set; }

        public List<Evento> Listar()
        {
            var query = new List<Evento>();
            try
            {
                using (var db = new ModeloSistema())
                {
                    query = db.Evento.Include("TipoEvento").
                        ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return query;
        }

        public Evento Obtener(int id)
        {
            var query = new Evento();
            try
            {
                using (var db = new ModeloSistema())
                {
                    query = db.Evento.Include("TipoEvento").
                        Where(x => x.Id == id).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return query;
        }

        public List<Evento> Buscar(string criterio)
        {
            var query = new List<Evento>();
            try
            {
                using (var db = new ModeloSistema())
                {
                    query = db.Evento.Include("TipoEvento").Include("Horario").
                        Where(x => x.Titulo.Contains(criterio)).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return query;
        }

        //metodo Guardar
        public void Guardar()
        {
            try
            {
                using (var db = new ModeloSistema())
                {
                    if (this.Id > 0)
                    {
                        db.Entry(this).State = EntityState.Modified;
                    }
                    else
                    {
                        db.Entry(this).State = EntityState.Added;
                        db.SaveChanges();
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        //metodo Eliminar
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
            catch (Exception)
            {
                throw;
            }
        }


    }
}
