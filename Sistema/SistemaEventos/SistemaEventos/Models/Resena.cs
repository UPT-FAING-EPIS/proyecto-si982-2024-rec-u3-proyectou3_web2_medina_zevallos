namespace SistemaEventos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Resena")]
    public partial class Resena
    {
        public int Id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Comentario { get; set; }

        public int? Calificacion { get; set; }

        public int? IdEvento { get; set; }

        public int? IdUsuario { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public virtual Evento Evento { get; set; }

        public virtual Usuario Usuario { get; set; }

        public List<Resena> Listar()
        {
            var query = new List<Resena>();
            try
            {
                using (var db = new ModeloSistema())
                {
                    query = db.Resena.Include("Evento").Include("Usuario").
                        ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return query;
        }

        public List<Resena> Buscar(string criterio)
        {
            var query = new List<Resena>();
            try
            {
                using (var db = new ModeloSistema())
                {
                    query = db.Resena.Include("Resena").
                        Where(x => x.Comentario.Contains(criterio)).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return query;
        }

        public Resena Obtener(int id)
        {
            var query = new Resena();
            try
            {
                using (var db = new ModeloSistema())
                {
                    query = db.Resena.Include("Evento").Include("Usuario").
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
