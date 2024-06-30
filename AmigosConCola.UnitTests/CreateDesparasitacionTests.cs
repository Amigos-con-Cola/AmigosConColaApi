using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.UseCases;
using FluentAssertions;
using Moq;

namespace AmigosConCola.UnitTests;

public class CreateDesparasitacionTests
{
    [Fact]
    public async void Test_creating_a_desparasitacion_with_correct_data_returns_the_desparasitacion()
    {
        var mock = new Mock<IDesparasitacionRepository>();
        var date = DateOnly.FromDateTime(DateTime.Now);
        var createDesparasitacionParams = new CreateDesparasitacionParams(2, "Interno", date, "Actyvil", 5, "Pastilla");
        var expectedDesparasitacion = new Desparasitacion
        {
            Id = 1,
            IdAnimal = 2,
            Tipo = "Interno",
            Producto = "Actyvil",
            Peso = 5,
            Formato = "Pastilla"
        };

        mock.Setup(x => x.Create(createDesparasitacionParams)).ReturnsAsync(expectedDesparasitacion);

        var desparasitaciones = mock.Object;
        var createDesparasitacion = new CreateDesparasitacionUseCase(desparasitaciones);

        // Act
        var result = await createDesparasitacion.Invoke(createDesparasitacionParams);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.IdAnimal.Should().Be(2);
        result.Value.Formato.Should().Be("Pastilla");
    }

    // TODO: Test for the error cases.
}
