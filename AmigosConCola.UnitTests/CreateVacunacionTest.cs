using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.UseCases;
using AmigosConCola.UnitTests.FakeData;
using FluentAssertions;
using Moq;

namespace AmigosConCola.UnitTests;

public class CreateVacunacionTest
{
    [Fact]
    public async void Test_create_a_vacunacion_with_valid_data_returns_the_vacunacion()
    {
        // Arrange
        var mock = new Mock<IVacunacionRepository>();
        var createVacunacionParameters = CreateVacunacionParamsFake.Get();
        var expectedVacunacion = new Vacunacion
        {
            Id = 1,
            IdAnimal = createVacunacionParameters.IdAnimal,
            Date = createVacunacionParameters.Date,
            Name = createVacunacionParameters.Name
        };

        mock.Setup(x => x.Create(createVacunacionParameters))
            .ReturnsAsync(expectedVacunacion);

        var vacunaciones = mock.Object;
        var createVacunacion = new CreateVacunacionUseCase(vacunaciones);

        // Act
        var result = await createVacunacion.Invoke(createVacunacionParameters);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Date.Should().Be(expectedVacunacion.Date);
        result.Value.Name.Should().Be(expectedVacunacion.Name);
        result.Value.ExamenPrevio.Should().Be(expectedVacunacion.ExamenPrevio);
    }

    [Fact]
    public async void Test_creating_a_vacunacion_with_an_invalid_name_returns_an_error()
    {
        // Arrange
        var mock = new Mock<IVacunacionRepository>();
        var createVacunacionParameters = CreateVacunacionParamsFake.Get("");
        var vacunaciones = mock.Object;
        var createVacunacion = new CreateVacunacionUseCase(vacunaciones);

        // Act
        var result = await createVacunacion.Invoke(createVacunacionParameters);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be("Name");
    }
}