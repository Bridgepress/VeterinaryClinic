using MediatR;

namespace VeterinaryClinic.Domain
{
    public interface IInterceptingSender
    {
        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request,
            CancellationToken cancellationToken = default);

        public Task Send(IRequest request, CancellationToken cancellationToken = default);
    }
}
