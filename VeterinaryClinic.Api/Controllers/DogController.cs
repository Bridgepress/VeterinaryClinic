using MediatR;
using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Domain.Commands.Dog;
using VeterinaryClinic.Domain.Requests.Dog;

namespace VeterinaryClinic.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DogController : ControllerBase
    {
        private readonly ISender _sender;

        public DogController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateDogCommand command, CancellationToken token)
        {
            var result = await _sender.Send(command, token);
            return Ok(result);
        }

        [HttpGet("dogs")]
        public async Task<IActionResult> GetDogs(CancellationToken token)
        {
            var result = await _sender.Send(new GetAllDogsRequest(), token);
            return Ok(result);
        }

        [HttpGet("GetAllDogs")]
        public async Task<IActionResult> GetAllDogs([FromQuery] GetAllDogsParameters parameters, CancellationToken token)
        {
            var result = await _sender.Send(parameters, token);
            return Ok(result);
        }
    }
}
