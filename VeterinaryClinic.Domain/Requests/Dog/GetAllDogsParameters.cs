using MediatR;
using VeterinaryClinic.Domain.Responses;
using VeterinaryClinic.Domain.Responses.Dog;

namespace VeterinaryClinic.Domain.Requests.Dog
{
    public class GetAllDogsParameters : QueryParameters, IRequest<PaginationResponse<GetAllDogsResponse>>
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public double? TailLength { get; set; }
        public double? Weight { get; set; }
    }
}
