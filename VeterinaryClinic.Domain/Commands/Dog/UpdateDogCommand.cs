using MediatR;
using System.ComponentModel.DataAnnotations;
using VeterinaryClinic.Core.Validation;

namespace VeterinaryClinic.Domain.Commands.Dog
{
    public record UpdateDogCommand(
        Guid Id,
        [Required]
        [StringLength(FieldsValidation.Dog.DogNameMaxLength)]
        string Name,
        [Required]
        [StringLength(FieldsValidation.Dog.DogColorMaxLength)]
        string Color,
        double TailLength,
        double Weight
    ) : IRequest;
}
