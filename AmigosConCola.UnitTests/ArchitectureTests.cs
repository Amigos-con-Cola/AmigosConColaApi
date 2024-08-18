using AmigosConCola.Core.UseCases;
using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using Assembly = System.Reflection.Assembly;

namespace AmigosConCola.UnitTests;

using static ArchRuleDefinition;

public class ArchitectureTests
{
    private static readonly Assembly? Assembly = Assembly.GetAssembly(typeof(CreateAnimalUseCase));
    private static readonly Architecture Architecture = new ArchLoader().LoadAssembly(Assembly).Build();

    [Fact]
    public void UseCases_Should_HaveNameEndingWithUseCase()
    {
        Types()
            .That().ResideInNamespace("AmigosConCola.Core.UseCases")
            .Should().HaveNameEndingWith("UseCase")
            .Check(Architecture);
    }

    [Fact]
    public void UseCases_Should_OnlyDependOnRepositories()
    {
        Types()
            .That().ResideInNamespace("AmigosConCola.Core.UseCases")
            .Should().OnlyDependOn(Types().That().ResideInNamespace("AmigosConCola.Core.Repositories"));
    }
}