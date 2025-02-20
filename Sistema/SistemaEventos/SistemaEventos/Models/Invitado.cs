namespace SistemaEventos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Invitado")]
    public partial class Invitado
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(255)]
        public string Correo { get; set; }

        [StringLength(8)]
        public string Dni { get; set; }

        public int IdReserva { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public virtual Reserva Reserva { get; set; }


        //metodo listar
        public List<Invitado> Listar()
        {

            var objInvitado = new List<Invitado>();
            try
            {
                using (var db = new ModeloSistema())
                {
                    objInvitado = db.Invitado.Include("Reserva").
                        ToList();
                }


            }
            catch (Exception)
            {
                throw;

            }
            return objInvitado;

        }


        //metodo listar
        public List<Invitado> Buscar(string criterio)
        {
            var objInvitado = new List<Invitado>();
            try
            {
                using (var db = new ModeloSistema())
                {
                    objInvitado = db.Invitado.Include("Invitado").Include("Reserva").
                        Where(x => x.Nombre.Contains(criterio)).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return objInvitado;
        }

        //metodo Guardar
        public void Guardar()
        {

            try
            {
                using (var db = new ModeloSistema())
                {
                    if (this.Id > 0) //cuando si sexiste objeto
                    {
                        db.Entry(this).State = EntityState.Modified;
                    }
                    else //cuando no  sexiste objeto a nivel bs
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
        public Invitado Obtener(int id)
        {
            var objInvitado = new Invitado();
            try
            {
                using (var db = new ModeloSistema())
                {
                    objInvitado = db.Invitado.Include("Evento").
                        Where(x => x.Id == id).SingleOrDefault();
                }


            }
            catch (Exception)
            {
                throw;

            }
            return objInvitado;

        }


    }
}
