namespace SistemaEventos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Publicidad")]
    public partial class Publicidad
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        [StringLength(255)]
        public string ImagenUrl { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        [StringLength(50)]
        public string Estado { get; set; }

        public int? IdEvento { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        public virtual Evento Evento { get; set; }
    }
}
