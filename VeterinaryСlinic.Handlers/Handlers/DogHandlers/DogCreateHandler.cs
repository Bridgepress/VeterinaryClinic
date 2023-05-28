using AutoMapper;
using MediatR;
using VeterinaryClinic.Domain.Commands.Dog;
using VeterinaryClinic.Domain.Entities;
using VeterinaryClinic.Domain.Responses;
using VeterinaryСlinic.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Core.Exceptions.BadRequest400;

namespace VeterinaryСlinic.Handlers.Handlers.DogHandlers
{
    public class DogCreateHandler : IRequestHandler<CreateDogCommand, CreatedResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;

        public DogCreateHandler(IMapper mapper, IRepositoryManager repositoryManager)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }

        public async Task<CreatedResponse> Handle(CreateDogCommand request, CancellationToken cancellationToken)
        {
            await ValidateNameDuplicate(request, cancellationToken);
            ValidateTailLength(request, cancellationToken);
            var entity = _mapper.Map<Dog>(request);
            _repositoryManager.DogRepository.Create(entity);
            await _repositoryManager.SaveChangesAsync(cancellationToken);

            return new CreatedResponse(entity.Id);
        }

        private async Task ValidateNameDuplicate(CreateDogCommand request, CancellationToken cancellationToken)
        {
            if (await _repositoryManager.DogRepository.GetAll()
                    .AnyAsync(x => x.Name == request.Name, cancellationToken))
            {
                throw new DuplicatedNameException(request.Name);
            }
        }

        private void ValidateTailLength(CreateDogCommand request, CancellationToken cancellationToken)
        {
            if (request.TailLength < 0)
            {
                throw new TailLengthException();
            }
        }
    }
}
