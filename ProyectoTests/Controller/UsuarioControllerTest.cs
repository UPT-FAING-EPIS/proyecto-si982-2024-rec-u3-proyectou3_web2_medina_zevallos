using Moq;
using SistemaEventos.Areas.Admin.Controllers;
using SistemaEventos.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;

namespace ProyectoTest.Controllers
{
    public class UsuarioControllerTest
    {
        private readonly UsuarioController _controller;
        private readonly Mock<Usuario> _mockUsuario;
        private readonly Mock<TipoUsuario> _mockTipoUsuario;

        public UsuarioControllerTest()
        {
            _mockUsuario = new Mock<Usuario>();
            _mockTipoUsuario = new Mock<TipoUsuario>();
            _controller = new UsuarioController();
        }

        [Fact]
        public void Index_ReturnsViewResult_WithAllUsuarios()
        {
            // Arrange
            var mockUsuarios = new List<Usuario>
            {
                new Usuario { Id = 1, Nombre = "Usuario 1" },
                new Usuario { Id = 2, Nombre = "Usuario 2" }
            };

            _mockUsuario.Setup(u => u.Listar()).Returns(mockUsuarios);

            // Act
            var result = _controller.Index(null) as ViewResult;
            var model = result?.Model as List<Usuario>;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(mockUsuarios.Count, model?.Count);
        }

        [Fact]
        public void Empleados_ReturnsViewResult_WithEmpleadoUsuarios()
        {
            // Arrange
            var mockEmpleados = new List<Usuario>
            {
                new Usuario { Id = 1, IdTipoU = 1, Nombre = "Empleado 1" },
                new Usuario { Id = 2, IdTipoU = 2, Nombre = "Empleado 2" }
            };

            _mockUsuario.Setup(u => u.Listar()).Returns(mockEmpleados);

            // Act
            var result = _controller.Empleados() as ViewResult;
            var model = result?.ViewData["Lista"] as List<Usuario>;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(mockEmpleados.Count, model?.Count);
        }

        [Fact]
        public void Clientes_ReturnsViewResult_WithClienteUsuarios()
        {
            // Arrange
            var mockClientes = new List<Usuario>
            {
                new Usuario { Id = 3, IdTipoU = 3, Nombre = "Cliente 1" },
                new Usuario { Id = 4, IdTipoU = 3, Nombre = "Cliente 2" }
            };

            _mockUsuario.Setup(u => u.Listar()).Returns(mockClientes);

            // Act
            var result = _controller.Clientes() as ViewResult;
            var model = result?.ViewData["Lista"] as List<Usuario>;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(mockClientes.Count, model?.Count);
        }

        [Fact]
        public void Ver_ReturnsViewResult_WithUsuarioDetails()
        {
            // Arrange
            int usuarioId = 1;
            var mockUsuario = new Usuario { Id = usuarioId, Nombre = "Usuario Test" };

            _mockUsuario.Setup(u => u.Obtener(usuarioId)).Returns(mockUsuario);

            // Act
            var result = _controller.Ver(usuarioId) as ViewResult;
            var model = result?.Model as Usuario;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(usuarioId, model?.Id);
        }

        [Fact]
        public void AgregarEditar_ReturnsViewResult_ForNewUsuario()
        {
            // Arrange
            _mockTipoUsuario.Setup(tu => tu.Listar()).Returns(new List<TipoUsuario>());

            // Act
            var result = _controller.AgregarEditar(0) as ViewResult;
            var model = result?.Model as Usuario;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(model);
        }

        [Fact]
        public void AgregarEditar_ReturnsViewResult_ForExistingUsuario()
        {
            // Arrange
            int usuarioId = 1;
            var mockUsuario = new Usuario { Id = usuarioId, Nombre = "Usuario Test" };
            _mockUsuario.Setup(u => u.Obtener(usuarioId)).Returns(mockUsuario);

            // Act
            var result = _controller.AgregarEditar(usuarioId) as ViewResult;
            var model = result?.Model as Usuario;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(usuarioId, model?.Id);
        }

        [Fact]
        public async Task Guardar_RedirectsToIndex_WhenModelStateIsValid()
        {
            // Arrange
            var mockUsuario = new Usuario { Id = 1, Nombre = "Usuario Test" };
            var mockFile = new Mock<HttpPostedFileBase>();
            _controller.ModelState.Clear();

            // Act
            var result = await _controller.Guardar(mockUsuario, mockFile.Object) as RedirectToRouteResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result?.RouteValues["action"]);
            Assert.Equal("Usuario", result?.RouteValues["controller"]);
            Assert.Equal("Admin", result?.RouteValues["area"]);
        }

        [Fact]
        public async Task Guardar_ReturnsToAgregarEditar_WhenModelStateIsInvalid()
        {
            // Arrange
            var mockUsuario = new Usuario { Id = 1, Nombre = "Usuario Test" };
            var mockFile = new Mock<HttpPostedFileBase>();
            _controller.ModelState.AddModelError("Nombre", "El nombre es obligatorio");

            // Act
            var result = await _controller.Guardar(mockUsuario, mockFile.Object) as RedirectToRouteResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("AgregarEditar", result?.RouteValues["action"]);
            Assert.Equal("Usuario", result?.RouteValues["controller"]);
            Assert.Equal("Admin", result?.RouteValues["area"]);
        }

        [Fact]
        public void Eliminar_RedirectsToIndex_AfterDeletingUsuario()
        {
            // Arrange
            int usuarioId = 1;
            _mockUsuario.Setup(u => u.Eliminar());

            // Act
            var result = _controller.Eliminar(usuarioId) as RedirectToRouteResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result?.RouteValues["action"]);
            Assert.Equal("Usuario", result?.RouteValues["controller"]);
            Assert.Equal("Admin", result?.RouteValues["area"]);
        }
    }
}
