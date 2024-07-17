using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.UseCases;
using FluentAssertions;
using Moq;

namespace AmigosConCola.UnitTests;

public class CreatePesoTest
{
    [Fact]
    public async void Test_CreatingAPeso_WithAnEmptyPesoActual_ReturnsAnError()
    {
        // Arrange
        var mock = new Mock<IPesosRepository>();
        var createPesoParams = new CreatePesoParams
        {
            IdAnimal = 1,
            PesoActual = 0,
            Fecha = DateOnly.FromDateTime(DateTime.Now)
        };

        var pesos = mock.Object;
        var createPeso = new CreatePesoUseCase(pesos);

        // Act
        var result = await createPeso.Invoke(createPesoParams);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be("PesoActual");
    }
}