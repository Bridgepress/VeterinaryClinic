using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinic.Api.Filters
{
    public record BadResponseObjectResult
    {
        public required string ExceptionMessage { get; init; }
        public required object ExceptionObject { get; init; }
    }
}
