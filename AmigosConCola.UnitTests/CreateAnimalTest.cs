using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.UseCases;
using AmigosConCola.UnitTests.FakeData;
using Moq;

namespace AmigosConCola.UnitTests;

public class CreateAnimalTest
{
    [Fact]
    public async void Test_creating_an_animal_with_valid_data_returns_the_animal()
    {
        // Arrange
        var mock = new Mock<IAnimalRepository>();
        var createAnimalParams = CreateAnimalParamsFake.Get();
        var expectedAnimal = AnimalFake.Get(
            createAnimalParams.Age,
            createAnimalParams.Name,
            createAnimalParams.ImageUrl);

        mock.Setup(x => x.Create(createAnimalParams))
            .ReturnsAsync(expectedAnimal);

        var animals = mock.Object;
        var createAnimal = new CreateAnimalUseCase(animals);

        // Act
        var result = await createAnimal.Invoke(createAnimalParams);

        // Assert
        Assert.True(!result.IsError);
        Assert.Equal(createAnimalParams.Name, result.Value.Name);
        Assert.Equal(createAnimalParams.Age, result.Value.Age);
        Assert.Equal(createAnimalParams.ImageUrl, result.Value.ImageUrl);
    }

    [Fact]
    public async void Test_creating_an_animal_with_an_empty_name_returns_an_error()
    {
        // Arrange
        var mock = new Mock<IAnimalRepository>();
        var createAnimalParams = CreateAnimalParamsFake.Get(name: "");
        var animals = mock.Object;
        var createAnimal = new CreateAnimalUseCase(animals);

        // Act
        var result = await createAnimal.Invoke(createAnimalParams);

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("Name", result.FirstError.Code);
    }

    [Fact]
    public async void Test_creating_an_animal_with_an_empty_image_url_returns_an_error()
    {
        // Arrange
        var mock = new Mock<IAnimalRepository>();
        var createAnimalParams = CreateAnimalParamsFake.Get(imageUrl: "");
        var animals = mock.Object;
        var createAnimal = new CreateAnimalUseCase(animals);

        // Act
        var result = await createAnimal.Invoke(createAnimalParams);

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("ImageUrl", result.FirstError.Code);
    }

    [Fact]
    public async void Test_creating_an_animal_with_a_negative_age_returns_an_error()
    {
        // Arrange
        var mock = new Mock<IAnimalRepository>();
        var createAnimalParams = CreateAnimalParamsFake.Get(-1);
        var animals = mock.Object;
        var createAnimal = new CreateAnimalUseCase(animals);

        // Act
        var result = await createAnimal.Invoke(createAnimalParams);

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("Age", result.FirstError.Code);
    }
}