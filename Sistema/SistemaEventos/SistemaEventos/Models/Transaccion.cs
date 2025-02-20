namespace SistemaEventos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Transaccion")]
    public partial class Transaccion
    {
        public int Id { get; set; }

        public int IdReserva { get; set; }

        [Required]
        [StringLength(50)]
        public string Tipo { get; set; }

        [StringLength(255)]
        public string Descripcion { get; set; }

        public decimal Monto { get; set; }

        public DateTime FechaTransaccion { get; set; }

        public virtual Reserva Reserva { get; set; }


        public List<Transaccion> Listar()
        {

            var query = new List<Transaccion>();
            try
            {
                using (var db = new ModeloSistema())
                {
                    query = db.Transaccion.Include("Reserva").
                        ToList();
                }


            }
            catch (Exception)
            {
                throw;

            }
            return query;

        }








    }
}
