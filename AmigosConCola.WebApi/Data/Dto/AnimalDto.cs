using System.ComponentModel.DataAnnotations.Schema;
using AmigosConCola.Core.Models;
using ErrorOr;

namespace AmigosConCola.WebApi.Data.Dto;

[Table("animals")]
public class AnimalDto
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("age")]
    public int Age { get; set; }

    [Column("gender")]
    public string Gender { get; set; } = null!;

    [Column("image_url")]
    public string? ImageUrl { get; set; }

    [Column("adopted")]
    public bool Adopted { get; set; }

    [Column("species")]
    public string Species { get; set; } = null!;

    [Column("story")]
    public string? Story { get; set; }

    [Column("location")]
    public string? Location { get; set; }

    [Column("code")]
    public string? Code { get; set; }

    [Column("weight")]
    public decimal Weight { get; set; }

    public ICollection<VacunacionDto> Vacunaciones { get; } = null!;

    public ICollection<DesparasitacionDto> Desparasitaciones { get; } = null!;

    public ErrorOr<Animal> ToDomain()
    {
        AnimalGender gender;
        AnimalSpecies species;

        if (!Enum.TryParse(Gender, true, out gender))
        {
            return Error.Validation(description: "Invalid animal gender");
        }

        if (!Enum.TryParse(Species, true, out species))
        {
            return Error.Validation(description: "Invalid animal species");
        }

        return new Animal
        {
            Id = Id,
            Name = Name,
            Age = Age,
            Gender = gender,
            Species = species,
            ImageUrl = ImageUrl,
            Adopted = Adopted,
            Story = Story,
            Location = Location,
            Code = Code,
            Weight = Weight
        };
    }
}
