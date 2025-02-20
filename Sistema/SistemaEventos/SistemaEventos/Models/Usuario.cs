namespace SistemaEventos.Models
{
    using Azure.Storage.Files.Shares;
    using Azure;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.IO;
    using System.Linq;
    using Azure.Storage.Files.Shares.Models;
    using Azure.Storage.Blobs;

    [Table("Usuario")]
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            Pago = new HashSet<Pago>();
            Resena = new HashSet<Resena>();
            Reserva = new HashSet<Reserva>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(255)]
        public string Apellido { get; set; }

        [StringLength(9)]
        public string Celular { get; set; }

        [StringLength(255)]
        public string Direccion { get; set; }

        [Required]
        [StringLength(255)]
        public string Correo { get; set; }

        [Required]
        [StringLength(255)]
        public string Contrasena { get; set; }

        [StringLength(255)]
        public string Perfil { get; set; }

        [StringLength(50)]
        public string Estado { get; set; }

        public int IdTipoU { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pago> Pago { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Resena> Resena { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reserva> Reserva { get; set; }

        public virtual TipoUsuario TipoUsuario { get; set; }

        //metodo listar
        public List<Usuario> Listar()
        {

            var objUsuario = new List<Usuario>();
            try
            {
                using (var db = new ModeloSistema())
                {
                    objUsuario = db.Usuario.Include("TipoUsuario").
                        ToList();
                }


            }
            catch (Exception)
            {
                throw;

            }
            return objUsuario;

        }


        //metodo listar
        public List<Usuario> Buscar(string criterio)
        {
            var objUsuario = new List<Usuario>();
            try
            {
                using (var db = new ModeloSistema())
                {
                    objUsuario = db.Usuario.Include("TipoUsuario").
                        Where(x => x.Nombre.Equals(criterio) ||
                        x.Apellido.Equals(criterio)).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return objUsuario;
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
        public Usuario Obtener(int id)
        {
            var objUsuario = new Usuario();
            try
            {
                using (var db = new ModeloSistema())
                {
                    objUsuario = db.Usuario.Include("TipoUsuario").
                        Where(x => x.Id == id).SingleOrDefault();
                }


            }
            catch (Exception)
            {
                throw;

            }
            return objUsuario;

        }

        public string SubirImagen(Stream imageStream, string fileName)
        {
            string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
            string containerName = "xddd";

            // Crear el cliente de blob
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            // Asegurarse de que el contenedor exista
            containerClient.CreateIfNotExists();

            // Subir el archivo
            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            blobClient.Upload(imageStream, overwrite: true);

            // Devolver la URL del blob
            return blobClient.Uri.ToString();
        }



        public ResponseModel ValidarLogin(string usuario, string password)
        {
            var rm = new ResponseModel();
            try
            {
                using (var db = new ModeloSistema())
                {

                    var user = db.Usuario.Where(x => x.Correo == usuario).
                    Where(x => x.Contrasena == password).SingleOrDefault();
                    if (user != null)
                    {
                        SessionHelper.AddUserToSession(user.Id.ToString());
                        rm.SetResponse(true);
                    }
                    else
                    {
                        rm.SetResponse(false, "Usuario y/o Password Incorrectos");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return rm;
        }

        public ResponseModel RegistrarUsuario()
        {
            var rm = new ResponseModel();
            try
            {
                using (var db = new ModeloSistema())
                {
                    // Verificar si el correo ya est� registrado
                    var existe = db.Usuario.Any(x => x.Correo == this.Correo);
                    if (existe)
                    {
                        rm.SetResponse(false, "El correo ya est� registrado.");
                    }
                    else
                    {
                        db.Entry(this).State = EntityState.Added;
                        db.SaveChanges();
                        rm.SetResponse(true);
                    }
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, ex.Message);
            }
            return rm;
        }
    }
}
