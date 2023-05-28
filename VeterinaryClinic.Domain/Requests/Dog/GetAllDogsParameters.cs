using MediatR;
using VeterinaryClinic.Domain.Responses;
using VeterinaryClinic.Domain.Responses.Dog;

namespace VeterinaryClinic.Domain.Requests.Dog
{
    public class GetAllDogsParameters : QueryParameters, IRequest<PaginationResponse<GetAllDogsResponse>>
    {
    }
}
