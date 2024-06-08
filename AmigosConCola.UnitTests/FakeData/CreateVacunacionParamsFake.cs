using AmigosConCola.Core.Repositories;
using Bogus;
using Bogus.DataSets;

namespace AmigosConCola.UnitTests.FakeData;

public class CreateVacunacionParamsFake
{
    public static CreateVacunacionParams Get(
        string? name = null)
    {
        var random = new Randomizer();
        var date = new Date();

        return new CreateVacunacionParams
        {
            IdAnimal = random.Number(),
            Date = date.FutureDateOnly(),
            Name = name ?? random.String2(10),
            ExamenPrevio = random.String2(10)
        };
    }
}