using Moq;
using SistemaEventos.Areas.Admin.Controllers;
using SistemaEventos.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Xunit;

namespace ProyectoTest.Controllers
{
    public class EventoControllerTest
    {
        private readonly EventoController _controller;
        private readonly Mock<ModeloSistema> _mockDb;
        private readonly Mock<Evento> _mockEvento;
        private readonly Mock<TipoEvento> _mockTipoEvento;

        public EventoControllerTest()
        {
            // Crear mocks
            _mockDb = new Mock<ModeloSistema>();
            _mockEvento = new Mock<Evento>();
            _mockTipoEvento = new Mock<TipoEvento>();

            // Asegurarnos de que los métodos de listar y buscar están correctamente configurados
            _mockEvento.Setup(e => e.Listar()).Returns(new List<Evento>());
            _mockEvento.Setup(e => e.Buscar(It.IsAny<string>())).Returns(new List<Evento>());

            // Crear el controlador pasando los mocks
            _controller = new EventoController(_mockDb.Object, _mockEvento.Object, _mockTipoEvento.Object);
        }

        [Fact]
        public void Index_ReturnsViewResult_WithAllEventos()
        {
            // Arrange
            var mockEventos = new List<Evento>
            {
                new Evento { Id = 1, Nombre = "Evento 1" },
                new Evento { Id = 2, Nombre = "Evento 2" }
            };

            _mockEvento.Setup(e => e.Listar()).Returns(mockEventos);
            _controller.ModelState.Clear();

            // Act
            var result = _controller.Index(null) as ViewResult;
            var model = result?.Model as List<Evento>;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(mockEventos.Count, model?.Count);
        }

        [Fact]
        public void Buscar_ReturnsViewResult_WithFilteredEventos()
        {
            // Arrange
            string criterio = "Evento 1";
            var mockEventos = new List<Evento>
            {
                new Evento { Id = 1, Nombre = "Evento 1" },
                new Evento { Id = 2, Nombre = "Evento 2" }
            };

            _mockEvento.Setup(e => e.Buscar(criterio)).Returns(mockEventos.Where(x => x.Nombre.Contains(criterio)).ToList());

            _controller.ModelState.Clear();

            // Act
            var result = _controller.Buscar(criterio) as ViewResult;
            var model = result?.Model as List<Evento>;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, model?.Count);
            Assert.Equal("Evento 1", model?[0].Nombre);
        }

        [Fact]
        public void AgregarEditar_ReturnsViewResult_ForNewEvento()
        {
            // Arrange
            _mockDb.Setup(db => db.Servicio).Returns(new List<Servicio>());
            _mockDb.Setup(db => db.Evento).Returns(new List<Evento>());
            _mockTipoEvento.Setup(te => te.Listar()).Returns(new List<TipoEvento>());

            // Act
            var result = _controller.AgregarEditar(0) as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = result?.Model as Evento;
            Assert.NotNull(model);
        }

        [Fact]
        public void AgregarEditar_ReturnsViewResult_ForExistingEvento()
        {
            // Arrange
            int eventoId = 1;
            var mockEvento = new Evento { Id = eventoId, Nombre = "Evento Test" };
            _mockDb.Setup(db => db.Evento.FirstOrDefault(e => e.Id == eventoId)).Returns(mockEvento);

            // Act
            var result = _controller.AgregarEditar(eventoId) as ViewResult;
            var model = result?.Model as Evento;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(eventoId, model?.Id);
        }

        [Fact]
        public void Guardar_RedirectsToIndex_WhenModelStateIsValid()
        {
            // Arrange
            var mockEvento = new Evento { Id = 1, Nombre = "Evento Test" };
            _controller.ModelState.Clear();

            // Act
            var result = _controller.Guardar(mockEvento) as RedirectToRouteResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result?.RouteValues["action"]);
            Assert.Equal("Evento", result?.RouteValues["controller"]);
            Assert.Equal("Admin", result?.RouteValues["area"]);
        }

        [Fact]
        public void Guardar_ReturnsToAgregarEditar_WhenModelStateIsInvalid()
        {
            // Arrange
            var mockEvento = new Evento { Id = 1, Nombre = "Evento Test" };
            _controller.ModelState.AddModelError("Nombre", "El nombre es obligatorio");

            // Act
            var result = _controller.Guardar(mockEvento) as RedirectToRouteResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("AgregarEditar", result?.RouteValues["action"]);
            Assert.Equal("Evento", result?.RouteValues["controller"]);
            Assert.Equal("Admin", result?.RouteValues["area"]);
        }

        [Fact]
        public void Eliminar_RedirectsToIndex_AfterDeletingEvento()
        {
            // Arrange
            int eventoId = 1;
            _mockEvento.Setup(e => e.Eliminar());

            // Act
            var result = _controller.Eliminar(eventoId) as RedirectToRouteResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result?.RouteValues["action"]);
            Assert.Equal("Evento", result?.RouteValues["controller"]);
            Assert.Equal("Admin", result?.RouteValues["area"]);
        }
    }
}
