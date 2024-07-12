using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.UseCases;
using FluentAssertions;
using Moq;

namespace AmigosConCola.UnitTests;

public class CreateAseoTests
{
    [Fact]
    public async void Test_CreatingAnAseo_WithAnEmptyTipo_ReturnsAnError()
    {
        // Arrange
        var mock = new Mock<IAseosRepository>();
        var createAseoParams = new CreateAseoParams
        {
            IdAnimal = 1,
            Tipo = "",
            Fecha = DateOnly.FromDateTime(DateTime.Now)
        };

        var aseos = mock.Object;
        var createAseo = new CreateAseoUseCase(aseos);

        // Act
        var result = await createAseo.Invoke(createAseoParams);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be("Tipo");
    }
}