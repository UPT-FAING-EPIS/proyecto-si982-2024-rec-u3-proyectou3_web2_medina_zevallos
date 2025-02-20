namespace SistemaEventos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("TipoUsuario")]
    public partial class TipoUsuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoUsuario()
        {
            Usuario = new HashSet<Usuario>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nombre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuario> Usuario { get; set; }


        public List<TipoUsuario> Listar()
        {
            var query = new List<TipoUsuario>();
            try
            {
                using (var db = new ModeloSistema())
                {
                    query = db.TipoUsuario.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return query;
        }

        public TipoUsuario Obtener(int id)
        {
            var query = new TipoUsuario();
            try
            {
                using (var db = new ModeloSistema())
                {
                    query = db.TipoUsuario.
                        Where(x => x.Id == id).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return query;
        }

        public List<TipoUsuario> Buscar(string criterio)
        {
            var query = new List<TipoUsuario>();
            try
            {
                using (var db = new ModeloSistema())
                {
                    query = db.TipoUsuario.
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
