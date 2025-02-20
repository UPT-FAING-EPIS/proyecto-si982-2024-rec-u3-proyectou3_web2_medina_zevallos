namespace SistemaEventos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("DetalleEvento")]
    public partial class DetalleEvento
    {
        public int Id { get; set; }

        public int IdEvento { get; set; }

        [StringLength(255)]
        public string Ubicacion { get; set; }

        public int? CapacidadMaxima { get; set; }

        [StringLength(255)]
        public string ImagenUrl { get; set; }

        [StringLength(255)]
        public string VideoUrl { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        public virtual Evento Evento { get; set; }

        public List<DetalleEvento> Listar()
        {
            var query = new List<DetalleEvento>();
            try
            {
                using (var db = new ModeloSistema())
                {
                    query = db.DetalleEvento.Include("Evento").
                        ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return query;
        }

        public DetalleEvento Obtener(int id)
        {
            var query = new DetalleEvento();
            try
            {
                using (var db = new ModeloSistema())
                {
                    query = db.DetalleEvento.Include("Evento").
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


    }
}
