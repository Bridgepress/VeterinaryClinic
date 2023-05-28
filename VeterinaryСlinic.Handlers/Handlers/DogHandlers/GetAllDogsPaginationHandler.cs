using MediatR;
using VeterinaryClinic.Domain.Requests.Dog;
using VeterinaryClinic.Domain.Responses;
using VeterinaryClinic.Domain.Responses.Dog;
using VeterinaryСlinic.Repositories.Contracts;
using VeterinaryClinic.Core.Extensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace VeterinaryСlinic.Handlers.Handlers.DogHandlers
{
    public class GetAllDogsPaginationHandler :
        IRequestHandler<GetAllDogsParameters, PaginationResponse<GetAllDogsResponse>>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public GetAllDogsPaginationHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<GetAllDogsResponse>> Handle(
            GetAllDogsParameters request,
            CancellationToken cancellationToken)
        {
            var query = _repositoryManager.DogRepository.GetAll();
            var count = await query.CountAsync(cancellationToken);
            var pagedDogs = await query
                .Sort(request.OrderBy, request.SortOrder)
                .Skip(request.PageSize * request.PageNumber)
                .Take(request.PageSize)
                .Select(c => new GetAllDogsResponse
                {
                    Color = c.Color,
                    Id = c.Id,
                    Name = c.Name,    
                    TailLength = c.TailLength,
                    Weight = c.Weight
                })
                .ToListAsync(cancellationToken);

            return new PaginationResponse<GetAllDogsResponse>(count, pagedDogs);
        }
    }
}
