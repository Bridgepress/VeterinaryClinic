using MediatR;
using VeterinaryClinic.Domain.Responses.Dog;

namespace VeterinaryClinic.Domain.Requests.Dog
{
    public record GetAllDogsRequest : IRequest<List<GetAllDogsResponse>>;
}
