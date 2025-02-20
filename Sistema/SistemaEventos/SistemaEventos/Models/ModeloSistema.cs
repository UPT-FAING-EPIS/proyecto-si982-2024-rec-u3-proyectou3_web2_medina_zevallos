using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SistemaEventos.Models
{
    public partial class ModeloSistema : DbContext
    {
        public ModeloSistema()
            : base("name=ModeloSistema")
        {
        }

        public virtual DbSet<DetalleEvento> DetalleEvento { get; set; }
        public virtual DbSet<Evento> Evento { get; set; }
        public virtual DbSet<EventoServicio> EventoServicio { get; set; }
        public virtual DbSet<Horario> Horario { get; set; }
        public virtual DbSet<Invitado> Invitado { get; set; }
        public virtual DbSet<Pago> Pago { get; set; }
        public virtual DbSet<Publicidad> Publicidad { get; set; }
        public virtual DbSet<Resena> Resena { get; set; }
        public virtual DbSet<Reserva> Reserva { get; set; }
        public virtual DbSet<Servicio> Servicio { get; set; }
        public virtual DbSet<TipoEvento> TipoEvento { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }
        public virtual DbSet<Transaccion> Transaccion { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DetalleEvento>()
                .Property(e => e.Ubicacion)
                .IsUnicode(false);

            modelBuilder.Entity<DetalleEvento>()
                .Property(e => e.ImagenUrl)
                .IsUnicode(false);

            modelBuilder.Entity<DetalleEvento>()
                .Property(e => e.VideoUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Evento>()
                .Property(e => e.Titulo)
                .IsUnicode(false);

            modelBuilder.Entity<Evento>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Evento>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<Evento>()
                .Property(e => e.Costo)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Evento>()
                .Property(e => e.CostoPorInvitado)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Evento>()
                .HasMany(e => e.DetalleEvento)
                .WithRequired(e => e.Evento)
                .HasForeignKey(e => e.IdEvento)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Evento>()
                .HasMany(e => e.EventoServicio)
                .WithRequired(e => e.Evento)
                .HasForeignKey(e => e.IdEvento);

            modelBuilder.Entity<Evento>()
                .HasMany(e => e.Publicidad)
                .WithOptional(e => e.Evento)
                .HasForeignKey(e => e.IdEvento);

            modelBuilder.Entity<Evento>()
                .HasMany(e => e.Resena)
                .WithOptional(e => e.Evento)
                .HasForeignKey(e => e.IdEvento);

            modelBuilder.Entity<Evento>()
                .HasMany(e => e.Reserva)
                .WithRequired(e => e.Evento)
                .HasForeignKey(e => e.IdEvento)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EventoServicio>()
                .Property(e => e.Precio)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Horario>()
                .HasMany(e => e.Reserva)
                .WithRequired(e => e.Horario)
                .HasForeignKey(e => e.IdHorario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Invitado>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Invitado>()
                .Property(e => e.Correo)
                .IsUnicode(false);

            modelBuilder.Entity<Invitado>()
                .Property(e => e.Dni)
                .IsUnicode(false);

            modelBuilder.Entity<Pago>()
                .Property(e => e.Monto)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Publicidad>()
                .Property(e => e.Titulo)
                .IsUnicode(false);

            modelBuilder.Entity<Publicidad>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Publicidad>()
                .Property(e => e.ImagenUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Publicidad>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<Resena>()
                .Property(e => e.Comentario)
                .IsUnicode(false);

            modelBuilder.Entity<Reserva>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<Reserva>()
                .HasMany(e => e.Invitado)
                .WithRequired(e => e.Reserva)
                .HasForeignKey(e => e.IdReserva)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reserva>()
                .HasMany(e => e.Pago)
                .WithRequired(e => e.Reserva)
                .HasForeignKey(e => e.IdReserva);

            modelBuilder.Entity<Reserva>()
                .HasMany(e => e.Transaccion)
                .WithRequired(e => e.Reserva)
                .HasForeignKey(e => e.IdReserva)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Servicio>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Servicio>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Servicio>()
                .Property(e => e.Precio)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Servicio>()
                .HasMany(e => e.EventoServicio)
                .WithRequired(e => e.Servicio)
                .HasForeignKey(e => e.IdServicio);

            modelBuilder.Entity<TipoEvento>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<TipoEvento>()
                .HasMany(e => e.Evento)
                .WithRequired(e => e.TipoEvento)
                .HasForeignKey(e => e.IdTipoEvento)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoUsuario>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<TipoUsuario>()
                .HasMany(e => e.Usuario)
                .WithRequired(e => e.TipoUsuario)
                .HasForeignKey(e => e.IdTipoU)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Transaccion>()
                .Property(e => e.Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<Transaccion>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Transaccion>()
                .Property(e => e.Monto)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Apellido)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Celular)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Direccion)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Correo)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Contrasena)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Perfil)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Pago)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.IdUsuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Resena)
                .WithOptional(e => e.Usuario)
                .HasForeignKey(e => e.IdUsuario);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Reserva)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.IdUsuario)
                .WillCascadeOnDelete(false);
        }
    }
}
