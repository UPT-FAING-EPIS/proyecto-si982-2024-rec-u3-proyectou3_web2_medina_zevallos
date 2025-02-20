namespace SistemaEventos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Pago")]
    public partial class Pago
    {
        public int Id { get; set; }

        public int IdUsuario { get; set; }

        public int IdReserva { get; set; }

        public DateTime FechaPago { get; set; }

        public DateTime? FechaCreacion { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? Monto { get; set; }

        public virtual Reserva Reserva { get; set; }

        public virtual Usuario Usuario { get; set; }


        public List<Pago> Listar()
        {
            var query = new List<Pago>();
            try
            {
                using (var db = new ModeloSistema())
                {
                    query = db.Pago.Include("Reserva").Include("Usuario").
                        ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return query;
        }

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

    }
}
