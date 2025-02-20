namespace SistemaEventos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Web.Mvc;

    [Table("Reserva")]
    public partial class Reserva
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reserva()
        {
            Invitado = new HashSet<Invitado>();
            Pago = new HashSet<Pago>();
            Transaccion = new HashSet<Transaccion>();
        }

        public int Id { get; set; }

        public int IdUsuario { get; set; }

        public int IdEvento { get; set; }

        public int IdHorario { get; set; }

        public DateTime FechaReserva { get; set; }

        [StringLength(50)]
        public string Estado { get; set; }

        public int? NumeroInvitados { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        public virtual Evento Evento { get; set; }

        public virtual Horario Horario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invitado> Invitado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pago> Pago { get; set; }

        public virtual Usuario Usuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaccion> Transaccion { get; set; }

        public List<Reserva> Listar()
        {
            var query = new List<Reserva>();
            try
            {
                using (var db = new ModeloSistema())
                {
                    query = db.Reserva.Include("Evento").Include("Usuario")
                        .Include("Horario").
                        ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return query;
        }

        public Reserva Obtener(int id)
        {
            var query = new Reserva();
            try
            {
                using (var db = new ModeloSistema())
                {
                    query = db.Reserva.Include("Evento").Include("Usuario")
                        .Include("Horario").
                        Where(x => x.Id == id).SingleOrDefault();
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
