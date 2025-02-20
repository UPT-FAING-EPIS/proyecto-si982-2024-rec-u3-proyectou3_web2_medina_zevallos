namespace SistemaEventos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("TipoEvento")]
    public partial class TipoEvento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoEvento()
        {
            Evento = new HashSet<Evento>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nombre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Evento> Evento { get; set; }

        public List<TipoEvento> Listar()
        {
            var query = new List<TipoEvento>();
            try
            {
                using (var db = new ModeloSistema())
                {
                    query = db.TipoEvento.Include("Evento").
                        ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return query;
        }

        public TipoEvento Obtener(int id)
        {
            var query = new TipoEvento();
            try
            {
                using (var db = new ModeloSistema())
                {
                    query = db.TipoEvento.Include("Evento").
                        Where(x => x.Id == id).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return query;
        }

        public List<TipoEvento> Buscar(string criterio)
        {
            var query = new List<TipoEvento>();
            try
            {
                using (var db = new ModeloSistema())
                {
                    query = db.TipoEvento.Include("Evento").
                        Where(x => x.Nombre.Contains(criterio)).ToList();
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
