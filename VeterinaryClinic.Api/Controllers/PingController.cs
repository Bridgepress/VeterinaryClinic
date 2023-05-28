using Microsoft.AspNetCore.Mvc;

namespace VeterinaryClinic.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken token)
        {
            return Ok(Messages.Ping);
        }
    }
}
