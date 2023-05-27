using MediatR;
using System.ComponentModel.DataAnnotations;
using VeterinaryClinic.Core.Validation;
using VeterinaryClinic.Domain.Responses;

namespace VeterinaryClinic.Domain.Commands.Dog
{
    public record CreateDogCommand(
        [Required]
        [StringLength(FieldsValidation.Dog.DogNameMaxLength)]
        string Name,
        [Required]
        [StringLength(FieldsValidation.Dog.DogColorMaxLength)]
        string Color,
        double TailLength,
        double Weight
    ) : IRequest<CreatedResponse>;
}
