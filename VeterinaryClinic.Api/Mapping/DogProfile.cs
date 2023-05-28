using AutoMapper;
using VeterinaryClinic.Domain.Commands.Dog;
using VeterinaryClinic.Domain.Entities;
using VeterinaryClinic.Domain.Responses.Dog;

namespace VeterinaryClinic.Api.Mapping
{
    public class DogProfile : Profile
    {
        public DogProfile()
        {
            CreateMap<Dog, GetAllDogsResponse>();
            CreateMap<Dog, CreateDogCommand>().ReverseMap();
        }
    }
}
