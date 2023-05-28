using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Domain.Requests.Dog;
using VeterinaryClinic.Domain.Responses.Dog;
using VeterinaryСlinic.Repositories.Contracts;

namespace VeterinaryСlinic.Handlers.Handlers.DogHandlers
{
    public class GetAllDogsHandler :
        IRequestHandler<GetAllDogsRequest, List<GetAllDogsResponse>>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public GetAllDogsHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<List<GetAllDogsResponse>> Handle(GetAllDogsRequest request,
            CancellationToken cancellationToken)
        {
            var courses = await _repositoryManager.DogRepository.GetAll().ToListAsync(cancellationToken);

            return _mapper.Map<List<GetAllDogsResponse>>(courses);
        }
    }
}
