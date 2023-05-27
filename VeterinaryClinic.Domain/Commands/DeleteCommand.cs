using MediatR;

namespace VeterinaryClinic.Domain.Commands
{
    public record DeleteCommand<TEntity>(Guid Id) : IRequest;
}
