using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.UseCases;
using ErrorOr;
using Moq;

namespace AmigosConCola.UnitTests;

public class GetAllAnimalsTest
{
    [Fact]
    public async void Test_getting_all_animals_with_valid_parameters_return_a_list_of_animals()
    {
        // Arrange
        var mock = new Mock<IAnimalRepository>();
        var getAllAnimalsParams = new PaginationParams
        {
            PerPage = 10,
            Page = 1
        };
        var filters = new GetAllAnimalsFilters();

        mock.Setup(x => x.GetAll(getAllAnimalsParams, filters))
            .ReturnsAsync(Enumerable.Empty<Animal>().ToErrorOr());

        var animals = mock.Object;
        var getAllAnimals = new GetAllAnimalsUseCase(animals);

        // Act
        var result = await getAllAnimals.Invoke(getAllAnimalsParams, filters);

        // Assert
        Assert.True(!result.IsError);
        Assert.Empty(result.Value);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async void Test_getting_all_animals_with_an_invalid_page_number_returns_an_error(int page)
    {
        // Arrange
        var mock = new Mock<IAnimalRepository>();
        var animals = mock.Object;
        var parameters = new PaginationParams
        {
            Page = page,
            PerPage = 10
        };
        var filters = new GetAllAnimalsFilters();
        var getAllAnimals = new GetAllAnimalsUseCase(animals);

        // Act
        var result = await getAllAnimals.Invoke(parameters, filters);

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("Page", result.FirstError.Code);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(7)]
    public async void Test_getting_all_animals_with_an_invalid_per_page_number_returns_an_error(int perPage)
    {
        // Arrange
        var mock = new Mock<IAnimalRepository>();
        var animals = mock.Object;
        var parameters = new PaginationParams
        {
            Page = 1,
            PerPage = perPage
        };
        var filters = new GetAllAnimalsFilters();
        var getAllAnimals = new GetAllAnimalsUseCase(animals);

        // Act
        var result = await getAllAnimals.Invoke(parameters, filters);

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("PerPage", result.FirstError.Code);
    }
}